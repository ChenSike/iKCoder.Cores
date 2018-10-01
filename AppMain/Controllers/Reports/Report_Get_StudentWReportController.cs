using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace AppMain.Controllers.Reports
{
	[Route("api/report_get_studentwreport")]
	[ApiController]
	public class Report_Get_StudentWReportController : BaseController.BaseController_AppMain
	{
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action()
		{
			XmlDocument doc_Result = new XmlDocument();
			XmlDocument doc_AccountTotal = new XmlDocument();
			doc_Result.LoadXml("<root></root>");
			XmlNode rootNode = doc_Result.SelectSingleNode("/root");
			Util_XmlOperHelper.SetAttribute(rootNode, "gdate", DateTime.Now.ToString("yyyy-MM-dd"));
			string uname = GetAccountInfoFromBasicController("name");

			//Get total count
			doc_AccountTotal = GetAPIFromCoreBasic("Account_Students_TotalCount");
			XmlNode rowNode = doc_AccountTotal.SelectSingleNode("/root/row[@index='1']");
			string strTotalValue = Util_XmlOperHelper.GetAttrValue(rowNode, "total");
			int iTotalValue = 1;
			int.TryParse(strTotalValue, out iTotalValue);
			if (iTotalValue == 0)
				iTotalValue = 1;


			//Get exp
			Dictionary<string, string> paramsMap = new Dictionary<string, string>();
			paramsMap.Add("@uid", uname);
			DataTable dtData_Exp = _appLoader.ExecuteSelectWithConditionsReturnDT(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_exp, paramsMap);
			int totalExpValue = 0;
			foreach (DataRow activeRow in dtData_Exp.Rows)
			{
				string strExpValueFromDB = string.Empty;
				Data_dbDataHelper.GetColumnData(activeRow, "exp", out strExpValueFromDB);
				int iTmpValue = 0;
				int.TryParse(strExpValueFromDB, out iTmpValue);
				totalExpValue = totalExpValue + iTmpValue;
			}

			//Get exp postion for all
			string sql = "select (@pos:=@pos+1) as pos,tmpResult.* from (SELECT sum(exp) as rexp,uid FROM ikcoder_appmain.students_exp group by uid order by rexp desc) tmpResult,(select @pos:=0) r";
			DataTable dtData_Position = _appLoader.ExecuteSQL(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, sql);
			string position = string.Empty;
			if (dtData_Position != null && dtData_Position.Rows.Count > 0)
			{
				DataRow[] row = dtData_Position.Select("uid='" + uname + "'");

				if (row.Length == 1)
				{
					Data_dbDataHelper.GetColumnData(row[0], "pos", out position);
				}
				else
				{
					position = "1";
				}
			}
			int iPosition = 1;
			int.TryParse(position, out iPosition);

			//Get finished lessons
			paramsMap.Clear();
			paramsMap.Add("@uid", uname);
			DataTable dtData_FinishedLesson = _appLoader.ExecuteSelectWithConditionsReturnDT(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonfinished, paramsMap);
			int iFinishedLessons = dtData_FinishedLesson.Rows.Count;
			List<string> lstLessonsFinished_Code = new List<string>();
			List<string> lstLessonsFinished_Name = new List<string>();
			foreach(DataRow activeDR in dtData_FinishedLesson.Rows)
			{
				string lesson_code = string.Empty;
				Data_dbDataHelper.GetColumnData(activeDR, "lesson_code", out lesson_code);
				lstLessonsFinished_Code.Add(lesson_code);
			}

			//Get achieved defined
			DataTable dtData_Achieved = _appLoader.ExecuteSelect(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_achieved_defined);
			
			//Get Lessons Basic
			DataTable dtData_Basic = _appLoader.ExecuteSelect(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_basic);

			//Get Learning Status
			DataTable dtData_LearningStatus = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_learninrecord, paramsMap);

			//Build Sumary
			XmlNode sumaryNode = Util_XmlOperHelper.CreateNode(doc_Result, "sumary", "");
			rootNode.AppendChild(sumaryNode);
			Util_XmlOperHelper.SetAttribute(sumaryNode, "exp", totalExpValue.ToString());
			if (iTotalValue == 1)
				Util_XmlOperHelper.SetAttribute(sumaryNode, "over", "100");
			else
			{
				double dOver = (1.00 - (double)((double)iPosition / (double)iTotalValue)) * 100;
				Util_XmlOperHelper.SetAttribute(sumaryNode, "over", Math.Round(dOver, 2).ToString());
			}
			Util_XmlOperHelper.SetAttribute(sumaryNode, "finished", iFinishedLessons.ToString());
			
			//Build Achieved
			XmlNode achievedNode = Util_XmlOperHelper.CreateNode(doc_Result, "achieved", "");
			rootNode.AppendChild(achievedNode);
			foreach(string lesson_code in lstLessonsFinished_Code)
			{
				DataRow[] achievedRows = dtData_Achieved.Select("lesson_code='" + lesson_code + "'");
				foreach(DataRow achievedRow in achievedRows)
				{
					string archieved_title = string.Empty;
					Data_dbDataHelper.GetColumnData(achievedRow, "title", out archieved_title);
					string archieved_content = string.Empty;
					Data_dbDataHelper.GetColumnData(achievedRow, "content", out archieved_content);
					XmlNode newAchievedItem = Util_XmlOperHelper.CreateNode(doc_Result, "item", "");
					Util_XmlOperHelper.SetAttribute(newAchievedItem, "title", archieved_title);
					Util_XmlOperHelper.SetAttribute(newAchievedItem, "content", archieved_content);
					achievedNode.AppendChild(newAchievedItem);
				}
			}

			//Build STEML
			XmlNode abilityNode = Util_XmlOperHelper.CreateNode(doc_Result, "ability", "");
			XmlNode lessonsLstNode = Util_XmlOperHelper.CreateNode(doc_Result, "lstlessons", "");
			abilityNode.AppendChild(lessonsLstNode);
			XmlNode steamNode = Util_XmlOperHelper.CreateNode(doc_Result, "steam","");
			abilityNode.AppendChild(steamNode);
			Dictionary<char, int> steamMapForLessons = new Dictionary<char, int>();
			foreach (string lesson_code in lstLessonsFinished_Code)
			{
				DataRow[] finishedLessonRows = dtData_Basic.Select("lesson_code='" + lesson_code + "'");
				if (finishedLessonRows.Length > 0)
				{
					XmlNode finishedLessonRowsItem = Util_XmlOperHelper.CreateNode(doc_Result, "item", "");
					string lesson_title = string.Empty;
					string lesson_steam = string.Empty;
					Data_dbDataHelper.GetColumnData(finishedLessonRows[0], "lesson_title", out lesson_title);
					Data_dbDataHelper.GetColumnData(finishedLessonRows[0], "steam", out lesson_steam);
					Util_XmlOperHelper.SetAttribute(finishedLessonRowsItem, "lesson_title", lesson_title);
					lessonsLstNode.AppendChild(finishedLessonRowsItem);
					char[] steam_chars = lesson_steam.ToCharArray();
					foreach(char steam_char in steam_chars)
					{
						if(steamMapForLessons.ContainsKey(steam_char))
						{
							steamMapForLessons[steam_char] = steamMapForLessons[steam_char] + 1;
						}
						else
						{
							steamMapForLessons.Add(steam_char, 1);
						}
					}
				}
			}
			foreach(char steam_char in steamMapForLessons.Keys)
			{
				XmlNode newSteamNode = Util_XmlOperHelper.CreateNode(doc_Result, steam_char.ToString(), (steamMapForLessons[steam_char] * 100).ToString());
				steamNode.AppendChild(newSteamNode);
			}

			//Build Time Line
			XmlNode timelineNode = Util_XmlOperHelper.CreateNode(doc_Result, "timeline", "");
			rootNode.AppendChild(timelineNode);
			if (dtData_LearningStatus != null && dtData_LearningStatus.Rows.Count > 0)
			{
				DataRow[] start_rows = dtData_LearningStatus.Select("actions='" + Global.LearningActionsMap.LessonAction_StartLearning + "'");
				DataRow[] end_rows = dtData_LearningStatus.Select("actions='" + Global.LearningActionsMap.LessonAction_EndLearning + "'");
				foreach(DataRow start_row in start_rows)
				{
					string str_rdt = string.Empty;
					DateTime dt_rdt = new DateTime();
					Data_dbDataHelper.GetColumnData(start_row, "rdt", out str_rdt);
					DateTime.TryParse(str_rdt, out dt_rdt);
					//str_rdt str_rtime = string.Empty;
					//DateTime dt_rtime = new DateTime();

				}
			}

			return Content(doc_Result.OuterXml);
		}
	}
}