using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Xml;
using iKCoderSDK;

namespace iKCoderComps
{

	/*
	public class PoolOfWebSockets
	{
		private static Dictionary<string, WebSocket> _sockets = new Dictionary<string, WebSocket>();

		public static void Push(string tokenID, WebSocket webSocket)
		{
			if (!_sockets.ContainsKey(tokenID))
				_sockets.Add(tokenID, webSocket);
			else
				_sockets[tokenID] = webSocket;
		}

		public static WebSocket Pull(string tokenID)
		{
			if (_sockets.ContainsKey(tokenID))
				return _sockets[tokenID];
			else
				return null;
		}

	}

	public class PoolOfExecutedMethod
	{
		private static Dictionary<string, Action<string, string>> map_methods = new Dictionary<string, Action<string, string>>();

		public static void AddMethod(string keyname,Action<string,string> action)
		{
			if (!map_methods.ContainsKey(keyname))
				map_methods.Add(keyname, action);
			else
				map_methods[keyname] = action;
		}
		

		
	}

	
	public class WebSocketMidware
	{
		private RequestDelegate _next;

		public const string symbol_tokenid = "tokenid";
				
		public WebSocketMidware(RequestDelegate next)
		{
			_next = next;
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

		
		public bool DoProcess(string response)
		{
			try
			{
				XmlDocument messageDoc = new XmlDocument();
				messageDoc.LoadXml(response);
				XmlNode fromNode = messageDoc.SelectSingleNode("/root/from");
				if (fromNode == null)
					return false;
				XmlNode commandNode = messageDoc.SelectSingleNode("/root/commnad");
				if (commandNode == null)
					return false;
				XmlNode contentNode = messageDoc.SelectSingleNode("/root/commnad");

				string command = Util_XmlOperHelper.GetNodeValue(commandNode);
				string from = Util_XmlOperHelper.GetNodeValue(commandNode);
				string content = contentNode != null ? Util_XmlOperHelper.GetNodeValue(contentNode) : string.Empty;


				
			}
			catch
			{
				return false;
			}
		}

		public async Task Invoke(HttpContext context)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				await _next.Invoke(context);
				return;
			}
			System.Net.WebSockets.WebSocket dummy;

			CancellationToken ct = context.RequestAborted;
			var currentSocket = await context.WebSockets.AcceptWebSocketAsync();
			string newSocketID = Guid.NewGuid().ToString();
			//string socketId = context.Request.Query[symbol_tokenid].ToString();
			PoolOfWebSockets.Push(newSocketID, currentSocket);

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
			}

			//_sockets.TryRemove(socketId, out dummy);

			await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
			currentSocket.Dispose();
		}

	}
	*/
}
