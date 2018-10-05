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
    [Route("api/AppStudio_Set_UpdateStoreItem")]
    [ApiController]
    public class AppStudio_Set_UpdateStoreItemController : BaseController.BaseController_AppMain
    {
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult action(string projectname)
		{
			try
			{
				string postData = _appLoader.get_PostData(HttpContext.Request);
				string base64PostData = Util_Common.Encoder_Base64(postData);
				Dictionary<string, string> paramsmap = new Dictionary<string, string>();
				string uname = GetAccountInfoFromBasicController("name");
				paramsmap.Add("@uid", uname);
				paramsmap.Add("@pname", projectname);
				DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_appstudio_store, paramsmap);
				bool isUpdated = false;
				if (dtData != null && dtData.Rows.Count == 1)
				{
					isUpdated = true;
					string id = string.Empty;
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "id", out id);
					paramsmap.Add("@id", id);
				}
				paramsmap.Add("@contentdoc", base64PostData);
				paramsmap.Add("@imdt", DateTime.Now.ToString());
				if (isUpdated)
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_appstudio_store, paramsmap);
				else
					_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_appstudio_store, paramsmap);
				return Content(MessageHelper.ExecuteSucessful());
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
    }
}