using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Xml;

namespace AppMain.Controllers.System
{
    [Route("api/system_get_check")]
    [ApiController]
    public class System_Get_CheckController : BaseController.BaseController_AppMain
    {
		[HttpGet]
		public ContentResult Action()
		{
			try
			{
				XmlDocument messageDoc = new XmlDocument();
				messageDoc.LoadXml("<root></root>");
				XmlNode rootNode = messageDoc.SelectSingleNode("/root");
				XmlNode itemNode = Util_XmlOperHelper.CreateNode(messageDoc, "item", "");
				Util_XmlOperHelper.SetAttribute(itemNode, "MainService", "true");
				rootNode.AppendChild(itemNode);
				try
				{
					_appLoader.InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
					itemNode = Util_XmlOperHelper.CreateNode(messageDoc, "item", "");
					Util_XmlOperHelper.SetAttribute(itemNode, "InitService", "true");
					rootNode.AppendChild(itemNode);
				}
				catch
				{
					itemNode = Util_XmlOperHelper.CreateNode(messageDoc, "item", "");
					Util_XmlOperHelper.SetAttribute(itemNode, "InitService", "false");
					rootNode.AppendChild(itemNode);
				}
				try
				{
					_appLoader.ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN);
					itemNode = Util_XmlOperHelper.CreateNode(messageDoc, "item", "");
					Util_XmlOperHelper.SetAttribute(itemNode, "ConnectDBService", "true");
					rootNode.AppendChild(itemNode);
				}
				catch
				{
					itemNode = Util_XmlOperHelper.CreateNode(messageDoc, "item", "");
					Util_XmlOperHelper.SetAttribute(itemNode, "ConnectDBService", "false");
					rootNode.AppendChild(itemNode);
				}
				return Content(messageDoc.OuterXml);
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse());
			}
			finally
			{
				_appLoader.CloseDB();
			}
		}
    }
}