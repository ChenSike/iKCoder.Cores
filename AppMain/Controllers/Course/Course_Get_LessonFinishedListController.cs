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
    [Route("api/Course_Get_LessonFinishedList")]
    [ApiController]
    public class Course_Get_LessonFinishedListController : BaseController.BaseController_AppMain
    {
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpPost]
		public ContentResult Action()
		{
			Dictionary<string, string> paramsmap = new Dictionary<string, string>();
			string uname = GetAccountInfoFromBasicController("name");
			paramsmap.Add("@uid", uname);
			DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonfinished, paramsmap);
			return Content(Data_dbDataHelper.ActionConvertDTtoXMLString(dtData));
		}
	}
}