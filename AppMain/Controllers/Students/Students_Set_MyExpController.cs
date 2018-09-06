using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace AppMain.Controllers.Students
{
    [Route("api/students_set_myexp")]
    [ApiController]
    public class Students_Set_MyExpController : BaseController.BaseController_AppMain
    {
		
		/*[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action(string symbol)
		{
			if(!string.IsNullOrEmpty(symbol))
			{
				string uname = GetAccountInfoFromBasicController("name");
				Dictionary<string, string> paramsForBasic = new Dictionary<string, string>();
				paramsForBasic.Add("@uid", uname);
				paramsForBasic.Add("@symbol", symbol);
				DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_checkon, paramsForBasic);
				if(dtData.Rows.Count>0)
				{
					return Content(MessageHelper.ExecuteFalse());
				}
				else
				{
					paramsForBasic.Add("@uid", uname);
					paramsForBasic.Add("@symbol", symbol);
					paramsForBasic.Add("@uid", uname);
					paramsForBasic.Add("@symbol", symbol);
				}
			}
		}*/
		
	}
}