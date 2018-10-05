using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;
using System.Xml;


namespace AppMain.Controllers.AppStudio
{
	[Route("api/AppStudio_Get_StoreItemsList")]
	[ApiController]
	public class AppStudio_Get_StoreItemsListController : BaseController.BaseController_AppMain
	{
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult action()
		{
			Dictionary<string, string> paramsmap = new Dictionary<string, string>();
			string uname = GetAccountInfoFromBasicController("name");
			paramsmap.Add("@uid", uname);
			class_data_MySqlDataReader mySqlDataReader = (class_data_MySqlDataReader)_appLoader.ExecuteSelectWithConditionsReturnDR(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_appstudio_store, paramsmap);
			XmlDocument doc = new XmlDocument();
			doc.LoadXml("<root></root>");
			while(mySqlDataReader.ActiveDataReader.Read())
			{
				XmlNode newItem = Util_XmlOperHelper.CreateNode(doc, "item","");
				doc.SelectSingleNode("/root").AppendChild(newItem);
				Util_XmlOperHelper.SetAttribute(newItem, "id", mySqlDataReader.ActiveDataReader.GetString("id"));
				Util_XmlOperHelper.SetAttribute(newItem, "pname", mySqlDataReader.ActiveDataReader.GetString("pname"));
				Util_XmlOperHelper.SetAttribute(newItem, "imdt", mySqlDataReader.ActiveDataReader.GetString("imdt"));
			}
			mySqlDataReader.ActiveDataReader.Close();
			return Content(doc.OuterXml);
		}
	}
}