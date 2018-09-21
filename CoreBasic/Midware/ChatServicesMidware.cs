using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Xml;
using iKCoderSDK;
using iKCoderComps;
using System.Data;

namespace CoreBasic.Midware
{

	public class MessageItem
	{
		public string SenderID { get; set; }
		public string ReceiverID { get; set; }
		public string MessageType { get; set; }
		public string Content { get; set; }
	}

	public class WebSocket_socketItem
	{
		public DateTime regTime;
		public DateTime recentTime;
		public WebSocket _sockets;
	}

	public class ChatServicesMidware
    {
		private static ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> _sockets = new ConcurrentDictionary<string, System.Net.WebSockets.WebSocket>();
		
		private readonly RequestDelegate _next;

		public ChatServicesMidware(RequestDelegate next)
		{
			_next = next;
		}
		
		
		public string get_ClientToken(HttpRequest httpRequest, string tokenname)
		{
			string tokenFromClient = string.Empty;
			if (GetQueryParam(httpRequest, tokenname) != string.Empty)
			{
				tokenFromClient = GetQueryParam(httpRequest, tokenname);
			}
			else if (httpRequest.Cookies.ContainsKey(tokenname))
			{
				tokenFromClient = httpRequest.Cookies[tokenname];
			}
			return tokenFromClient;
		}

		public string GetQueryParam(HttpRequest httpRequest, string paramname)
		{
			if (!string.IsNullOrEmpty(paramname))
			{
				if (httpRequest.Query.ContainsKey(paramname))
				{
					return httpRequest.Query[paramname].ToString();
				}
				else
				{
					return string.Empty;
				}
			}
			else
				return string.Empty;
		}



		public async Task Invoke(HttpContext context)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				await _next.Invoke(context);
				return;
			}

			string token = get_ClientToken(context.Request, "student_token");
			if (Global.LoginServices.verify_logined_token(token))
			{

				CancellationToken ct = context.RequestAborted;
				var currentSocket = await context.WebSockets.AcceptWebSocketAsync();
				AppLoader newApploader = new AppLoader();
				string socketId = token;
				if (!_sockets.ContainsKey(socketId))
				{
					_sockets.TryAdd(socketId, currentSocket);
					newApploader.InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
					newApploader.ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
					newApploader.LoadSPS(Global.GlobalDefines.DB_SPSMAP_FILE);
				}

				while (true)
				{
					if (ct.IsCancellationRequested)
					{
						break;
					}

					string response = await ReceiveStringAsync(currentSocket, ct);
					if (string.IsNullOrEmpty(response))
					{
						if (currentSocket.State != WebSocketState.Open)
						{
							break;
						}
						continue;
					}
					string returnContent = ProcessProtocal(token, response, newApploader);
					await SendStringAsync(currentSocket, returnContent, ct);

					/*
					foreach (var socket in _sockets)
					{
						if (socket.Value.State != WebSocketState.Open)
						{
							continue;
						}
						if (socket.Key == msg.ReceiverID || socket.Key == socketId)
						{
							await SendStringAsync(socket.Value, JsonConvert.SerializeObject(msg), ct);
						}
					}
					*/
					WebSocket dummy;
					_sockets.TryRemove(socketId, out dummy);
					newApploader.CloseDB();
					await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
					currentSocket.Dispose();
				}
			}
		}

		private string ProcessProtocal(string token, string message, AppLoader existedLoader)
		{
			XmlDocument protocalMessageDoc = new XmlDocument();
			protocalMessageDoc.LoadXml(message);
			XmlNode fromNode = protocalMessageDoc.SelectSingleNode("/root/from");
			if (fromNode == null)
				return "<root><errmsg>nofrom</errmsg></root>";
			string from = Util_XmlOperHelper.GetNodeValue(fromNode);
			if(from != token)
				return "<root><errmsg>invalidated token</errmsg></root>";
			XmlNode actionNode = protocalMessageDoc.SelectSingleNode("/root/action");
			if (actionNode == null)
				return "<root><errmsg>noaction</errmsg></root>";
			string action = Util_XmlOperHelper.GetNodeValue(actionNode);
			switch (action)
			{
				case Global.ActionsMap.Action_Get_ActiveDialog:
					/*
					 * <root>
					 * <from>
					 * token
					 * <from>
					 * <action>
					 * Action_Get_ActiveDialog
					 * </action>
					 * </root>
					 * 					 
					 */
					return Action_Get_ActiveDialog(token,existedLoader);
				case Global.ActionsMap.Action_Get_DialogContent:
					/*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <action>
					 * Action_Get_DialogContent
					 * </action>
					 * <params>
					 * <target>
					 * XXXX
					 * </target>
					 * </params>
					 * </root>
					 * 
					 */
					return "";
				case Global.ActionsMap.Action_Set_OpenDialog:
					/*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <action>
					 * Action_Set_OpenDialog
					 * </action>
					 * <params>
					 * <target>
					 * XXXX
					 * </target>
					 * </params>
					 * </root>
					 * 
					 */
					XmlNode paramsNode = protocalMessageDoc.SelectSingleNode("/root/params");
					if(paramsNode==null)
					{
						return "<root><errmsg>noparams</errmsg></root>";
					}
					else
					{
						XmlNode targetNode = paramsNode.SelectSingleNode("target");
						if(targetNode==null)
						{
							return "<root><errmsg>notarget</errmsg></root>";
						}
						string tagetValue = Util_XmlOperHelper.GetNodeValue(targetNode);
						return Action_Set_OpenDialog(token, tagetValue, existedLoader);
					}
					
			}
			return "";
		}

		public string Action_Set_OpenDialog(string token,string target, AppLoader existedLoader)
		{
			string uid = Global.LoginServices.Pull(token).name;
			Dictionary<string, string> activeParams = new Dictionary<string, string>();
			activeParams.Add("source", uid);
			activeParams.Add("target", target);
			DataTable activeDataTable = existedLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams);
			if (activeDataTable == null)
				return "<root><errmsg>nodata</errmsg></root>";
			else
			{
				StringBuilder resultStr = new StringBuilder();
				if (activeDataTable.Rows.Count==0)
				{
					string newMsgContent = "<msg></msg>";
					string base64Content = Util_Common.Encoder_Base64(newMsgContent);
					activeParams.Add("content", base64Content);
					existedLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams);
					resultStr.Append("<root>");
					resultStr.Append(newMsgContent);
					resultStr.Append("</root>");
				}
				else
				{
					string base64MsgContent = string.Empty;
					Data_dbDataHelper.GetColumnData(activeDataTable.Rows[0],"content", out base64MsgContent);
					string MsgContent = Util_Common.Decoder_Base64(base64MsgContent);
					resultStr.Append("<root>");
					resultStr.Append(base64MsgContent);
					resultStr.Append("</root>");
				}
				return resultStr.ToString();
			}
		}

		public string Action_Get_DialogContent(string token, string msgid,AppLoader existedLoader)
		{
			string uid = Global.LoginServices.Pull(token).name;
			Dictionary<string, string> activeParams = new Dictionary<string, string>();
			activeParams.Add("source", uid);
			DataTable activeDataTable = existedLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams);
			if (activeDataTable == null)
				return "<root><errmsg>nodata</errmsg></root>";
			else
			{
				return Data_dbDataHelper.ActionConvertDTtoXMLString(activeDataTable);
			}
		}

		public string Action_Get_ActiveDialog(string token,AppLoader existedLoader)
		{
			string uid = Global.LoginServices.Pull(token).name;
			Dictionary<string, string> activeParams = new Dictionary<string, string>();
			activeParams.Add("source", uid);
			DataTable activeDataTable = existedLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams);
			if (activeDataTable == null)
				return "<root><errmsg>nodata</errmsg></root>";
			else
			{
				return Data_dbDataHelper.ActionConvertDTtoXMLString(activeDataTable);
			}
		}
		

		private static Task SendStringAsync(System.Net.WebSockets.WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
		{
			var buffer = Encoding.UTF8.GetBytes(data);
			var segment = new ArraySegment<byte>(buffer);
			return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
		}

		private static async Task<string> ReceiveStringAsync(System.Net.WebSockets.WebSocket socket, CancellationToken ct = default(CancellationToken))
		{
			var buffer = new ArraySegment<byte>(new byte[8192]);
			using (var ms = new MemoryStream())
			{
				WebSocketReceiveResult result;
				do
				{
					ct.ThrowIfCancellationRequested();

					result = await socket.ReceiveAsync(buffer, ct);
					ms.Write(buffer.Array, buffer.Offset, result.Count);
				}
				while (!result.EndOfMessage);

				ms.Seek(0, SeekOrigin.Begin);
				if (result.MessageType != WebSocketMessageType.Text)
				{
					return null;
				}

				using (var reader = new StreamReader(ms, Encoding.UTF8))
				{
					return await reader.ReadToEndAsync();
				}
			}
		}
	}
}
