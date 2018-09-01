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

namespace AppMain.Controllers.Exp
{
    [Route("api/Exp_Get_List")]
    [ApiController]
    public class Exp_Get_ListController : BaseController.BaseController_AppMain
    {
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action(string course_name)
		{
			Dictionary<string, string> paramsMap = new Dictionary<string, string>();
			paramsMap.Add("@course", course_name);
			DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_exp, paramsMap);
			return Content(Data_dbDataHelper.ActionConvertDTtoXMLString(dtData));			
		}
		
	}
}