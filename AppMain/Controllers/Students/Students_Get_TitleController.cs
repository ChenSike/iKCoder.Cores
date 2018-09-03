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
    [Route("api/students_get_title")]
    [ApiController]
    public class Students_Get_TitleController : BaseController.BaseController_AppMain
    {
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action()
		{
			try
			{
				string uname = GetAccountInfoFromBasicController("name");
				Dictionary<string, string> paramsForBasic = new Dictionary<string, string>();
				paramsForBasic.Add("@uid", uname);
				DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_exp, paramsForBasic);
				if(dtData!=null && dtData.Rows.Count>0)
				{
					DataRow currentRow = null;
					Data_dbDataHelper.GetActiveRow(dtData, 0, out currentRow);
					if (currentRow != null)
					{
						int exp_value = Data_dbDataHelper.GetColumnIntData(currentRow, "exp");
						DataTable dtTitle = _appLoader.ExecuteSelect(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_titles_defined);
						
					}
				}
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
			}
		}
    }
}