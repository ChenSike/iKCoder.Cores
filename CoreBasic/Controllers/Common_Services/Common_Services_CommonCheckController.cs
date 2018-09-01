using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Xml;

namespace CoreBasic.Controllers.Common_Services
{
	[Produces("application/text")]
	[Route("api/Common_Services_CommonCheck")]
	public class Common_Services_CommonCheckController : ControllerBase_Std
	{
		[HttpGet]
		public ContentResult Action()
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
				_appLoader.ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
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
			finally
			{
				_appLoader.CloseDB();
			}
			return Content(messageDoc.OuterXml);
		}
	}
}