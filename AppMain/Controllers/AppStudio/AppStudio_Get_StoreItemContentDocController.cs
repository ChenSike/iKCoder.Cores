using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace AppMain.Controllers.AppStudio
{
	[Route("api/AppStudio_Get_StoreItemContentDoc")]
	[ApiController]
	public class AppStudio_Get_StoreItemContentDocController : BaseController.BaseController_AppMain
	{
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpPost]
		public ContentResult Action(string id)
		{
			Dictionary<string, string> paramsmap = new Dictionary<string, string>();
			string uname = GetAccountInfoFromBasicController("name");
			paramsmap.Add("@id", id);
			DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_appstudio_store, paramsmap);
			if (dtData != null && dtData.Rows.Count == 1)
			{
				string base64StrContentDoc = string.Empty;
				string StrContentDoc = string.Empty;
				Data_dbDataHelper.GetColumnData(dtData.Rows[0], "contentdoc", out base64StrContentDoc);
				StrContentDoc = Util_Common.Decoder_Base64(base64StrContentDoc);
				return Content(StrContentDoc);
			}
			else
				return Content(MessageHelper.ExecuteFalse());
		}
	}
}