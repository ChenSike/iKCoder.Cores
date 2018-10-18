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

namespace AppMain.Controllers.Course
{
	[Route("api/Course_Get_Agrr_LessonList")]
	[ApiController]
	public class Course_Get_Agrr_LessonListController : BaseController.BaseController_AppMain
	{
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action(string course_name)
		{
			try
			{
				string uname = GetAccountInfoFromBasicController("name");
				Dictionary<string, string> paramsForBasic = new Dictionary<string, string>();
				paramsForBasic.Add("@course_name", course_name);
				List<string> lstCourses = new List<string>();
				DataTable dtData_lesson = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_basic, paramsForBasic);
				Dictionary<string, string> paramsmap = new Dictionary<string, string>();
				paramsmap.Add("@uid", uname);
				DataTable dtData_finished = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonfinished, paramsmap);
				paramsmap.Clear();
				paramsmap.Add("@uid", uname);
				DataTable dtData_Learning = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_learninrecord, paramsmap);
				XmlDocument returnDoc = new XmlDocument();
				returnDoc.LoadXml("<root></root>");
				foreach (DataRow row in dtData_lesson.Rows)
				{
					XmlNode itemNode = Util_XmlOperHelper.CreateNode(returnDoc, "item", "");
					returnDoc.SelectSingleNode("/root").AppendChild(itemNode);
					string lesson_title = string.Empty;
					string lesson_code = string.Empty;
					string lesson_steam = string.Empty;
					string lesson_udba = string.Empty;
					string lesson_steps = string.Empty;
					string lesson_order = string.Empty;
					Data_dbDataHelper.GetColumnData(row, "lesson_title", out lesson_title);
					Data_dbDataHelper.GetColumnData(row, "lesson_code", out lesson_code);
					Data_dbDataHelper.GetColumnData(row, "steam", out lesson_steam);
					Data_dbDataHelper.GetColumnData(row, "udba", out lesson_udba);
					Data_dbDataHelper.GetColumnData(row, "totalsteps", out lesson_steps);
					Data_dbDataHelper.GetColumnData(row, "lorder", out lesson_order);
					Util_XmlOperHelper.SetAttribute(itemNode, "lesson_title", lesson_title);
					Util_XmlOperHelper.SetAttribute(itemNode, "lesson_code", lesson_code);
					Util_XmlOperHelper.SetAttribute(itemNode, "steam", lesson_steam);
					Util_XmlOperHelper.SetAttribute(itemNode, "udba", lesson_udba);
					Util_XmlOperHelper.SetAttribute(itemNode, "totalsteps", lesson_steps);
					Util_XmlOperHelper.SetAttribute(itemNode, "order", lesson_order);
					Util_XmlOperHelper.SetAttribute(itemNode, "status", "0");
					if (dtData_Learning != null && dtData_Learning.Rows.Count > 0)
					{
						DataRow[] learningRows = dtData_Learning.Select("code='" + lesson_code + "' and actions='" + Global.LearningActionsMap.LessonAction_StartLearning + "'");
						if (learningRows.Length > 0)
							Util_XmlOperHelper.SetAttribute(itemNode, "status", "1");
					}
					if (dtData_finished != null && dtData_finished.Rows.Count > 0)
					{
						DataRow[] finishedRows = dtData_finished.Select("lesson_code='" + lesson_code + "'");
						if (finishedRows.Length > 0)
							Util_XmlOperHelper.SetAttribute(itemNode, "status", "2");
					}
				}
				return Content(returnDoc.OuterXml);
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse());
			}

		}
	}
}