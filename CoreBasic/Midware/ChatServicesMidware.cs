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
using System.Security.Cryptography;

namespace CoreBasic.Midware
{

	public class ChatServicesMidware
    {
		private static ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> _sockets = new ConcurrentDictionary<string, System.Net.WebSockets.WebSocket>();
		private static ConcurrentDictionary<string, string> _accountTokenMap = new ConcurrentDictionary<string, string>();

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


		public string BuildReturnDoc(string optionalDoc,string faction , bool isPassive)
		{
			string activeAction = string.Empty;
			StringBuilder returnResult = new StringBuilder();
			if(!isPassive)
				returnResult.Append("<ret>");
			else
				returnResult.Append("<ret passive='false'>");
			returnResult.Append("<faction>");
			returnResult.Append(faction);
			returnResult.Append("</faction>");
			returnResult.Append("<doc>");
			returnResult.Append(optionalDoc);
			returnResult.Append("</doc>");
			returnResult.Append("</ret>");
			return returnResult.ToString();
		}


		public async Task Invoke(HttpContext context)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				await _next.Invoke(context);
				return;
			}

			/*string strSecWebSocketKeyValue = context.Request.Headers["Sec-WebSocket-Key"].ToString();
			SHA1 sha = SHA1.Create();
			string strSecWebSocketKeyValue_ForSha = strSecWebSocketKeyValue + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
			byte[] hash_buffer = Encoding.Default.GetBytes(strSecWebSocketKeyValue_ForSha);
			byte[] hash_result = sha.ComputeHash(hash_buffer);
			string strResult = Encoding.Default.GetString(hash_result);
			string strResultBase64 = Util_Common.Encoder_Base64(strResult);
			if (!context.Response.Headers.ContainsKey("Sec-WebSocket-Accept"))
				context.Response.Headers.Add("Sec-WebSocket-Accept", strResultBase64);
			else
				context.Response.Headers["Sec-WebSocket-Accept"] = strResultBase64;
			context.Response.Headers["Sec-WebSocket-Protocol"] = "chat";
			*/

			string token = get_ClientToken(context.Request, "student_token");
			if (Global.LoginServices.verify_logined_token(token))
			{
				CancellationToken ct = context.RequestAborted;
				var currentSocket = await context.WebSockets.AcceptWebSocketAsync();
				AppLoader newApploader = new AppLoader();
				string uname = Global.LoginServices.Pool_Logined[token].name;
				_accountTokenMap.TryAdd(uname, token);
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
					try
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
						string activeAction = string.Empty;
						string returnContent = ProcessProtocal(token, response, newApploader, out activeAction);
						await SendStringAsync(currentSocket, BuildReturnDoc(returnContent, activeAction, false), ct);
					}
					catch
					{
						continue;
					}
				}
				WebSocket dummy_socket;
				_sockets.TryRemove(socketId, out dummy_socket);
				string dummy_token;
				_accountTokenMap.TryRemove(uname, out dummy_token);
				newApploader.CloseDB();
				await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
				currentSocket.Dispose();
			}
		}

		private string ProcessProtocal(string token, string message, AppLoader existedLoader,out string refAction)
		{
			refAction = "";
			XmlDocument protocalMessageDoc = new XmlDocument();
			protocalMessageDoc.LoadXml(message);
			XmlNode fromNode = protocalMessageDoc.SelectSingleNode("/root/from");
			if (fromNode == null)
				return "<root type='error'><errmsg>nofrom</errmsg></root>";
			string from = Util_XmlOperHelper.GetNodeValue(fromNode);
			/*if(from != token)
				return "<root type='error'><errmsg>invalidated token</errmsg></root>";*/
			XmlNode actionNode = protocalMessageDoc.SelectSingleNode("/root/action");
			if (actionNode == null)
				return "<root type='error'><errmsg>noaction</errmsg></root>";
			string action = Util_XmlOperHelper.GetNodeValue(actionNode);
			XmlNode paramsNode = protocalMessageDoc.SelectSingleNode("/root/params");
			refAction = action;
			Global.ItemAccountStudents activeStuentItem = Global.LoginServices.Pull(token);
            switch (action)
            {
                case Global.ActionsMap.Action_Get_DialogList:
                    /*
					 * <root>
					 * <from>
					 * token
					 * <from>
					 * <action>
					 * Action_Get_DialogList
					 * </action>
					 * </root>
					 * 					 
					 */
                    return Action_Get_DialogList(activeStuentItem.name, existedLoader);
                case Global.ActionsMap.Action_Get_DialogContent:
                    /*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <symbol>
					 * </symbol>
					 * <action>
					 * Action_Get_DialogContent
					 * </action>
					 * </root>
					 * 
					 */
                    string message_symbol = string.Empty;
                    XmlNode messageSymbolNode = protocalMessageDoc.SelectSingleNode("/root/symbol");
                    if (messageSymbolNode == null)
                        return "<root type='error'><errmsg>nosymbol</errmsg></root>";
                    message_symbol = Util_XmlOperHelper.GetNodeValue(messageSymbolNode);
                    if (string.IsNullOrEmpty(message_symbol))
                        return "<root type='error'><errmsg>nosymbol</errmsg></root>";
                    return Action_Get_DialogContent(message_symbol, existedLoader);
                case Global.ActionsMap.Action_Set_NewDialog:
                    /*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <target>
					 * <item>
					 * u1
					 * </item>
					 * <item>
					 * u2
					 * </item>
					 * </target>
					 * <action>
					 * Action_Set_NewDialog
					 * </action>
					 * </root>
					 * 
					 */
                    XmlNodeList targetUserNodes = protocalMessageDoc.SelectNodes("/root/target/item");
                    List<string> lstTargetItems = new List<string>();
                    if (targetUserNodes != null && targetUserNodes.Count > 0)
                    {
                        foreach (XmlNode itemNode in targetUserNodes)
                        {
                            string value = Util_XmlOperHelper.GetNodeValue(itemNode);
                            lstTargetItems.Add(value);
                        }
                    }
                    else
                    {
                        return "<root type='error'><errmsg>notargets</errmsg></root>";
                    }
                    Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
                    lstTargetItems.Add(activeItem.name);
                    return Action_Set_NewDialog(token, lstTargetItems, existedLoader);
                case Global.ActionsMap.Action_Get_RelationsList:
                    /*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <action>
					 * Action_Get_RelationsList
					 * </action>
					 * </root>
					 */
                    return Action_Get_RelationsList(from, existedLoader);
                case Global.ActionsMap.Action_Get_RelationsSearch:
                    /*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <action>
					 * Action_Get_RelationsSearch
					 * </action>
					 * <value>
					 * </value>
					 * </root>
					 */
                    string keyvalue = string.Empty;
                    XmlNode valueNode = protocalMessageDoc.SelectSingleNode("/root/value");
                    if (valueNode != null)
                        keyvalue = Util_XmlOperHelper.GetNodeValue(valueNode);
                    return Action_Get_RelationsSearch(keyvalue, existedLoader);
                case Global.ActionsMap.Action_Get_RelationsAcceptableList:
                    return Action_Get_RelationsAcceptableList(token, existedLoader);
                case Global.ActionsMap.Action_Set_NewFriend:
                    /*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <action>
					 * Action_Set_NewFriend
					 * </action>
					 * <msg>
					 * </msg>
					 * <suname>
					 * </suname>
					 * </root>
					 */
                    string suname = string.Empty;
                    XmlNode sunameNode = protocalMessageDoc.SelectSingleNode("/root/suname");
                    string msg = string.Empty;
                    XmlNode msgNode = protocalMessageDoc.SelectSingleNode("/root/msg");
                    if (sunameNode == null)
                    {
                        return "<root type='error'><errmsg>nosuname</errmsg></root>";
                    }
                    else
                    {
                        suname = Util_XmlOperHelper.GetNodeValue(sunameNode);
                        return Action_Set_NewFriend(from, suname, msg, existedLoader);
                    }
                case Global.ActionsMap.Action_Set_AcceptFriend:
                    /*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <action>
					 * Action_Set_AcceptFriend
					 * </action>
					 * <id>
					 * </id>
					 * </root>
					 */
					string id = string.Empty;
                    XmlNode id_node = protocalMessageDoc.SelectSingleNode("/root/id");
                    if (id_node == null)
                    {
                        return "<root type='error'><errmsg>no puname</errmsg></root>";
                    }
                    else
                        id = Util_XmlOperHelper.GetNodeValue(id_node);
					return Action_Set_AcceptFriend(id, existedLoader);
				case Global.ActionsMap.Action_Get_BatchArrProfile:
                    /*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <action>
					 * Action_Get_BatchArrProfile
					 * </action>
					 * <params>
					 * <item>
					 * </item>
					 * </params>
					 * </root>
					 */
                    List<string> lstID = new List<string>();
                    XmlNodeList items = paramsNode.SelectNodes("item");
                    foreach (XmlNode item in items)
                    {
                        string itemValue = Util_XmlOperHelper.GetNodeValue(item);
                        lstID.Add(itemValue);
                    }
                    return Action_Get_BatchArrProfile(lstID, existedLoader);
                case Global.ActionsMap.Action_Set_SendMessage:
                    /*
					 * <root>
					 * <from>
					 * token
					 * </from>
					 * <symbol>
					 * symbol for message
					 * </symbol>
					 * <action>
					 * Action_Set_OpenDialog
					 * </action>
					 * <target>
					 * <item>
					 * u1
					 * </item>
					 * <item>
					 * u2
					 * </item>
					 * </target>
					 * <message>
					 * </message>
					 * </root>
					 * 
					 */
                    XmlNode symbolNode = protocalMessageDoc.SelectSingleNode("/root/symbol");
                    if (symbolNode == null)
                    {
                        return "<root type='error'><errmsg>nosymbol</errmsg></root>";
                    }
                    string symbolValue = Util_XmlOperHelper.GetNodeValue(symbolNode);
                    XmlNode targetNode = protocalMessageDoc.SelectSingleNode("/root/target");
                    if (targetNode == null)
                    {
                        return "<root type='error'><errmsg>notarget</errmsg></root>";
                    }
                    string tagetValue = Util_XmlOperHelper.GetNodeValue(targetNode);
                    XmlNodeList targetItemNodes = protocalMessageDoc.SelectNodes("/root/target/item");
                    List<string> lstOwners = new List<string>();
                    foreach (XmlNode itemNode in targetItemNodes)
                    {
                        string value = Util_XmlOperHelper.GetNodeValue(itemNode);
                        lstOwners.Add(value);
                    }
                    XmlNode messageNode = protocalMessageDoc.SelectSingleNode("/root/message");
                    string messageValue = Util_XmlOperHelper.GetNodeValue(messageNode);
					return Action_Set_SendMessage(activeStuentItem.name, symbolValue, messageValue, lstOwners, existedLoader);
            }
			return "";
		}

		public string Action_Get_BatchArrProfile(List<string> lstID, AppLoader existedLoader)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("select * from profile_students where uid in (");
			for (int i =1; i <= lstID.Count; i++)
			{
				if (i == lstID.Count)
					sql.Append("'" + lstID[i-1] + "'");
				else
					sql.Append("'" + lstID[i-1] + "',");
			}
			sql.Append(")");
			DataTable dtData = existedLoader.ExecuteSQL(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, sql.ToString());
			return MessageHelper.TransDatatableToXML(dtData);
		}

		public string Action_Get_RelationsAcceptableList(string token, AppLoader existedLoader)
		{
			try
			{
				Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
				string sql = "select * from relations_students where suname='" + activeItem.name + "' and isacc='0'";
				DataTable dtData = existedLoader.ExecuteSQL(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, sql);
				return Data_dbDataHelper.ActionConvertDTtoXMLString(dtData);
			}
			catch (Exception err)
			{
				return err.Message + "|" + err.StackTrace;
			}
		}
                
		public string Action_Set_NewFriend(string token,string suname, string msg,AppLoader existedLoader)
		{
			Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
			Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
			paramsMap_for_profle.Add("@puname", activeItem.name);
			DataTable dtData = existedLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle);
			if (dtData != null && dtData.Rows.Count > 0)
			{
                DataRow[] rows = dtData.Select("suname='" + suname + "'");
				if (rows.Length > 0)
				{
					return MessageHelper.ExecuteSucessful();
				}
			}
			paramsMap_for_profle.Add("@isacc", "0");
			paramsMap_for_profle.Add("@suname", suname);
			paramsMap_for_profle.Add("@message", msg);
			if (existedLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle))
			{
				if (_accountTokenMap.ContainsKey(suname))
				{
					string owner_token = _accountTokenMap[suname];
					WebSocket owner_socket = null;
					if (_sockets.ContainsKey(owner_token))
					{
						owner_socket = _sockets[owner_token];
						StringBuilder message = new StringBuilder();
						message.Append("<root type='passive'>");
						message.Append("<action>" + Global.ActionsMap.Action_Get_RelationsAcceptableList + "</action>");
						message.Append("</root>");
						SendStringAsync(owner_socket, BuildReturnDoc(message.ToString(), Global.ActionsMap.Action_Get_RelationsAcceptableList, true));
					}
				}
				return MessageHelper.ExecuteSucessful();
			}
			else
			{
				return MessageHelper.ExecuteFalse();
			}
		}

		public string Action_Set_AcceptFriend(string id, AppLoader existedLoader)
		{			
			Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
			paramsMap_for_profle.Add("@id",id);
			DataTable dtData = existedLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle);
			if (dtData != null && dtData.Rows.Count ==1 )
			{
				paramsMap_for_profle.Add("@isacc", "1");
				if (existedLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle))
				{
					string puname = string.Empty;
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "puname", out puname);
					if (_accountTokenMap.ContainsKey(puname))
					{
						string owner_token = _accountTokenMap[puname];
						WebSocket owner_socket = null;
						if (_sockets.ContainsKey(owner_token))
						{
							owner_socket = _sockets[owner_token];
							StringBuilder message = new StringBuilder();
							message.Append("<root type='passive'>");
							message.Append("<action>" + Global.ActionsMap.Action_Get_RelationsList + "</action>");
							message.Append("</root>");
							SendStringAsync(owner_socket, BuildReturnDoc(message.ToString(), Global.ActionsMap.Action_Get_RelationsList, false));
						}
					}
					return MessageHelper.ExecuteSucessful();
				}
				else
				{
					return MessageHelper.ExecuteFalse();
				}
			}
			else
			{
				return MessageHelper.ExecuteFalse();
			}
		}



		public string Action_Get_RelationsSearch(string value, AppLoader existedLoader)
		{
			string sql = "select * from profile_students where uid = '" + value + "' or nickname like '%" + value + "%'";
			DataTable dtData = existedLoader.ExecuteSQL(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, sql);
			return MessageHelper.TransDatatableToXML(dtData);
		}

		public string Action_Get_RelationsList(string token, AppLoader existedLoader)
		{			
			Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
			Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
			paramsMap_for_profle.Add("@puname", activeItem.name);
			paramsMap_for_profle.Add("@isacc", "1");
			DataTable dtData = existedLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle);
			return MessageHelper.TransDatatableToXML(dtData);
		}

		

		public string Action_Set_SendMessage(string uid,string messageSymbol, string message, List<string> lstOwners, AppLoader existedLoader)
		{
			foreach(string owner in lstOwners)
			{
				if (_accountTokenMap.ContainsKey(owner))
				{
					string owner_token = _accountTokenMap[owner];
					WebSocket owner_socket = null;
					if (_sockets.ContainsKey(owner_token))
					{
						owner_socket = _sockets[owner_token];
						StringBuilder sent_message = new StringBuilder();
						sent_message.Append("<root>");
						sent_message.Append("<from>" + uid);
						sent_message.Append("</from>");
						sent_message.Append("<group>");
						sent_message.Append("</group>");
						sent_message.Append("<action>");
						sent_message.Append(Global.ActionsMap.Passive_Get_ReceivedMessage);
						sent_message.Append("</action>");
						sent_message.Append("<msg>");
						sent_message.Append(message);
						sent_message.Append("</msg>");
						sent_message.Append("</root>");
						SendStringAsync(owner_socket, message);
					}
				}
			}
			Dictionary<string, string> activeParams = new Dictionary<string, string>();
			activeParams.Add("symbol", messageSymbol);
			DataTable activeDataTable = existedLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams);
			if (activeDataTable == null)
				return "<root type='error'><errmsg>lostdata</errmsg></root>";
			else
			{
				string base64MsgContent = string.Empty;
                string id = string.Empty;
                Data_dbDataHelper.GetArrByteColumnDataToString(activeDataTable.Rows[0], "content", out base64MsgContent);
                Data_dbDataHelper.GetColumnData(activeDataTable.Rows[0], "id", out id);
                string MsgContent = Util_Common.Decoder_Base64(base64MsgContent);
				XmlDocument contentDoc = new XmlDocument();
				contentDoc.LoadXml(MsgContent);
				XmlNode newItem = Util_XmlOperHelper.CreateNode(contentDoc,"item", message);
				Util_XmlOperHelper.SetAttribute(newItem, "date", DateTime.Now.ToString("yyyy-MM-dd"));
				Util_XmlOperHelper.SetAttribute(newItem, "time", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
				Util_XmlOperHelper.SetAttribute(newItem, "dt", DateTime.Now.ToString());
				contentDoc.SelectSingleNode("/root").AppendChild(newItem);
				string MsgBase64Conetent = Util_Common.Encoder_Base64(contentDoc.OuterXml);
                activeParams.Clear();
                activeParams.Add("id", id);
                activeParams.Add("content", MsgBase64Conetent);
                existedLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams);
				return "<root><msg>sent</msg></root>";
			}
		}

		public string Action_Get_DialogList(string uid, AppLoader existedLoader)
		{
			string query_sql = "SELECT * FROM ikcoder_basic.messagesindex_students where symbol in (select symbol from ikcoder_basic.messagesindex_students where uid = '" + uid + "')";
			DataTable activeDataTable = existedLoader.ExecuteSQL(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, query_sql);
			if (activeDataTable == null)
				return "<root type='error'><errmsg>nodata</errmsg></root>";
			else
			{
				XmlDocument returnDoc = new XmlDocument();
				returnDoc.LoadXml("<root></root>");
				XmlNode rootNode = returnDoc.SelectSingleNode("/root");
				int uid_index = 1;
				foreach (DataRow activeRow in activeDataTable.Rows)
				{
					string strSymbol = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "symbol", out strSymbol);
					string uid_fromdb = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "uid", out uid_fromdb);
					if (uid == uid_fromdb)
						continue;
					XmlNode itemNode = returnDoc.SelectSingleNode("/root/item[@symbol='" + strSymbol + "']");
					if (itemNode == null)
					{
						itemNode = Util_XmlOperHelper.CreateNode(returnDoc, "item", "");
						rootNode.AppendChild(itemNode);
					}					
					Util_XmlOperHelper.SetAttribute(itemNode, "uid", uid_fromdb);
					Util_XmlOperHelper.SetAttribute(itemNode, "symbol", strSymbol);
					Util_XmlOperHelper.SetAttribute(itemNode, "index", uid_index.ToString());
					uid_index++;
				}
				return returnDoc.OuterXml.ToString();
			}
		}

		public string Action_Set_NewDialog(string token, List<string> lstOwners, AppLoader existedLoader)
		{
			string symbol_dialog = Guid.NewGuid().ToString();
			Dictionary<string, string> activeParams = new Dictionary<string, string>();
			activeParams.Add("symbol", symbol_dialog);
            DataTable dtMessageIndex = existedLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messagesindex_students, activeParams);
            List<string> existedIndexLst = new List<string>();
            foreach(DataRow dr in dtMessageIndex.Rows)
            {
                string existedUID = string.Empty;
                Data_dbDataHelper.GetColumnData(dr, "uid", out existedUID);
                if(!existedIndexLst.Contains(existedUID))
                    existedIndexLst.Add(existedUID);
            }
            foreach (string owner in lstOwners)
			{
                if (!existedIndexLst.Contains(owner))
                {
                    if (activeParams.ContainsKey("uid"))
                        activeParams.Remove("uid");
                    activeParams.Add("uid", owner);
                    existedLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messagesindex_students, activeParams);
                }
			}
			activeParams.Clear();
			activeParams.Add("symbol", symbol_dialog);
			string newMsgContent = "<msg></msg>";
			string base64Content = Util_Common.Encoder_Base64(newMsgContent);
			activeParams.Add("content", base64Content);
			existedLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams);
			StringBuilder strReturnDoc = new StringBuilder();
			strReturnDoc.Append("<root type='passive'>");
			strReturnDoc.Append("<action>" + Global.ActionsMap.Action_Get_DialogList + "</action>");
			strReturnDoc.Append("</root>");
			return strReturnDoc.ToString();
		}

		
		public string Action_Get_DialogContent(string symbolMessage, AppLoader existedLoader)
		{
			Dictionary<string, string> activeParams = new Dictionary<string, string>();
			activeParams.Add("symbol", symbolMessage);
			DataTable activeDataTable = existedLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams);
			if (activeDataTable == null || activeDataTable.Rows.Count==0)
			{
				StringBuilder strReturnDoc = new StringBuilder();
				strReturnDoc.Append("<root type='passive'>");
				strReturnDoc.Append("<action>" + Global.ActionsMap.Action_Get_RelationsList + "</action>");
				strReturnDoc.Append("</root>");
				return strReturnDoc.ToString();
			}
			else
			{
				StringBuilder resultStr = new StringBuilder();
				string base64MsgContent = string.Empty;
				Data_dbDataHelper.GetArrByteColumnDataToString(activeDataTable.Rows[0], "content", out base64MsgContent);
				string MsgContent = Util_Common.Decoder_Base64(base64MsgContent);
                string dialogID = string.Empty;
                Data_dbDataHelper.GetArrByteColumnDataToString(activeDataTable.Rows[0], "id", out dialogID);
                resultStr.Append("<did>");
                resultStr.Append(dialogID);
                resultStr.Append("</did>");
                resultStr.Append(MsgContent);
				return resultStr.ToString();
			}
		}

		public string Action_Set_DelDialog(string symbolMessage, AppLoader existedLoader)
		{
			Dictionary<string, string> activeParams = new Dictionary<string, string>();
			activeParams.Add("symbol", symbolMessage);
			if(existedLoader.ExecuteDeleteWithConditions(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_messages_students, activeParams))
			{
				StringBuilder strReturnDoc = new StringBuilder();
				strReturnDoc.Append("<root type='passive'>");
				strReturnDoc.Append("<action>" + Global.ActionsMap.Action_Get_DialogList + "</action>");
				strReturnDoc.Append("</root>");
				return strReturnDoc.ToString();
			}
			else
			{
				return "<root type='error'><errmsg>false</errmsg></root>";
			}			
		}

        

		private static Task SendStringAsync(System.Net.WebSockets.WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
		{
			var buffer = Encoding.UTF8.GetBytes(data);
			var segment = new ArraySegment<byte>(buffer, 0, buffer.Length);
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
