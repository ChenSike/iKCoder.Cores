using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace AppMain.Controllers.Course
{
    [Route("api/Course_Set_LessonFinished")]
    [ApiController]
    public class Course_Set_LessonFinishedController : BaseController.BaseController_AppMain
    {
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action(string lesson_code)
		{
			try
			{
				if (string.IsNullOrEmpty(lesson_code))
					return Content(MessageHelper.ExecuteFalse());
				Dictionary<string, string> paramsmap = new Dictionary<string, string>();
				string uname = GetAccountInfoFromBasicController("name");
				paramsmap.Add("@uid", uname);
				paramsmap.Add("@lesson_code", lesson_code);
				DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonfinished, paramsmap);
				if (dtData != null && dtData.Rows.Count == 1)
				{
					string finished_id = string.Empty;
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "id", out finished_id);
					paramsmap.Clear();
					paramsmap.Add("@id", finished_id);
					paramsmap.Add("@rdt", DateTime.Now.ToString("yyyy-MM-dd"));
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonfinished, paramsmap);
				}
				else
				{
				paramsmap.Add("@rdt", DateTime.Now.ToString("yyyy-MM-dd"));
				_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonfinished, paramsmap);
				}
				return Content(MessageHelper.ExecuteSucessful());
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
			}
		}
	}
}