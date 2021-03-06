﻿using System;
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
	[Route("api/Report_Get_StudentWReport")]
	[ApiController]
	public class Report_Get_StudentWReportController : BaseController.BaseController_AppMain
	{
        private class CourseMainInfoItem
        {
            public string name
            {
                set;
                get;
            }

            public string title
            {
                set;
                get;
            }

            public int totallessons
            {
                set;
                get;
            }

            public int finishedlessons
            {
                set;
                get;
            }

        }

		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action()
		{
			try
			{
				XmlDocument doc_Result = new XmlDocument();
				XmlDocument doc_AccountTotal = new XmlDocument();
				doc_Result.LoadXml("<root></root>");
				XmlNode rootNode = doc_Result.SelectSingleNode("/root");
				Util_XmlOperHelper.SetAttribute(rootNode, "gdate", DateTime.Now.ToString("yyyy-MM-dd"));
				string uname = GetAccountInfoFromBasicController("name");
                string uid = GetAccountInfoFromBasicController("id");

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
				List<string> lstLessonsFinished_Code = new List<string>();
				List<string> lstLessonsFinished_Name = new List<string>();
				DataTable dtData_FinishedLesson = _appLoader.ExecuteSelectWithConditionsReturnDT(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonfinished, paramsMap);
				int iFinishedLessons = 0;
				if (dtData_FinishedLesson != null && dtData_FinishedLesson.Rows.Count > 0)
				{
					iFinishedLessons = dtData_FinishedLesson.Rows.Count;
					foreach (DataRow activeDR in dtData_FinishedLesson.Rows)
					{
						string lesson_code = string.Empty;
						Data_dbDataHelper.GetColumnData(activeDR, "lesson_code", out lesson_code);
						lstLessonsFinished_Code.Add(lesson_code);
					}
				}

				//Get achieved defined
				DataTable dtData_Achieved = _appLoader.ExecuteSelect(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_achieved_defined);

				//Get Lessons Basic
				DataTable dtData_Basic = _appLoader.ExecuteSelect(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_basic);

				//Get Learning Status
				DataTable dtData_LearningStatus = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_learninrecord, paramsMap);

                //Get Course Main
                DataTable dtData_CourseMain = _appLoader.ExecuteSelect(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_main);

                //Get Lessons Total
                sql = "SELECT count(*) as total,course_name FROM ikcoder_appmain.course_basic group by course_name";
                DataTable dtData_LessonsTotal = _appLoader.ExecuteSQL(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, sql);

                //Get Finished Lessons Total
                sql = "SELECT count(*) as total,course_name FROM ikcoder_appmain.students_lessonfinished group by course_name";
                DataTable dtData_FinishedLessonsTotal = _appLoader.ExecuteSQL(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, sql);

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
				foreach (string lesson_code in lstLessonsFinished_Code)
				{
					DataRow[] achievedRows = dtData_Achieved.Select("lesson_code='" + lesson_code + "'");
					foreach (DataRow achievedRow in achievedRows)
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
				XmlNode steamNode = Util_XmlOperHelper.CreateNode(doc_Result, "steam", "");
				abilityNode.AppendChild(steamNode);
				Dictionary<char, int> steamMapForLessons = new Dictionary<char, int>();
				foreach (string lesson_code in lstLessonsFinished_Code)
				{
					if (dtData_Basic != null && dtData_Basic.Rows.Count > 0)
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
							foreach (char steam_char in steam_chars)
							{
								if (steamMapForLessons.ContainsKey(steam_char))
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
				}
				foreach (char steam_char in steamMapForLessons.Keys)
				{
					XmlNode newSteamNode = Util_XmlOperHelper.CreateNode(doc_Result, steam_char.ToString(), (steamMapForLessons[steam_char] * 100).ToString());
					steamNode.AppendChild(newSteamNode);
				}
                rootNode.AppendChild(abilityNode);

                //Build Course Finsished Map
                XmlNode courseFinishedMapNode = Util_XmlOperHelper.CreateNode(doc_Result, "coursefinished", "");
                rootNode.AppendChild(courseFinishedMapNode);
                Dictionary<string, CourseMainInfoItem> totalfinished_courses = new Dictionary<string, CourseMainInfoItem>();
                foreach(DataRow courseRow in dtData_CourseMain.Rows)
                {
                    XmlNode newItemNode = Util_XmlOperHelper.CreateNode(doc_Result, "item", "");
                    courseFinishedMapNode.AppendChild(newItemNode);
                    string course_name = string.Empty;
                    Data_dbDataHelper.GetColumnData(courseRow, "name", out course_name);
                    Util_XmlOperHelper.SetAttribute(newItemNode, "name", course_name);
                    string course_title = string.Empty;
                    Data_dbDataHelper.GetColumnData(courseRow, "title", out course_title);
                    Util_XmlOperHelper.SetAttribute(newItemNode, "title", course_title);
                    DataRow[] rows_finishedLesson = dtData_FinishedLesson.Select("course_name='" + course_name + "'");
                    string lessonFinished_Total = string.Empty;
                    int i_lessonFinished_Total = 0;
                    if (rows_finishedLesson.Length>0)
                    {
                        Data_dbDataHelper.GetColumnData(rows_finishedLesson[0], "total", out lessonFinished_Total);
                        int.TryParse(lessonFinished_Total, out i_lessonFinished_Total);
                        Util_XmlOperHelper.SetAttribute(newItemNode, "count_finished", lessonFinished_Total);
                    }
                    else
                    {
                        Util_XmlOperHelper.SetAttribute(newItemNode,"count_finished", "0");
                    }
                    DataRow[] rows_total = dtData_LessonsTotal.Select("course_name='" + course_name + "'");
                    string lessons_Total = string.Empty;
                    int i_lessons_Total = 1;
                    if (rows_total.Length>0)
                    {
                        Data_dbDataHelper.GetColumnData(rows_total[0], "total", out lessons_Total);
                        int.TryParse(lessons_Total, out i_lessons_Total);
                        Util_XmlOperHelper.SetAttribute(newItemNode, "count_total", lessons_Total);
                    }
                    else
                    {
                        Util_XmlOperHelper.SetAttribute(newItemNode, "count_total", "1");
                    }
                    Util_XmlOperHelper.SetAttribute(newItemNode, "rate", ((i_lessonFinished_Total / i_lessons_Total) * 100).ToString());
                }
                

                //Build Time Line
                XmlNode timelineNode = Util_XmlOperHelper.CreateNode(doc_Result, "timeline", "");
				rootNode.AppendChild(timelineNode);
				if (dtData_LearningStatus != null && dtData_LearningStatus.Rows.Count > 0)
				{
					DataRow[] start_rows = dtData_LearningStatus.Select("actions='" + Global.LearningActionsMap.LessonAction_StartLearning+"'");
                    
                    foreach (DataRow start_row in start_rows)
					{
						string str_start_rdt = string.Empty;
						DateTime dt_start_rdt = new DateTime();
						Data_dbDataHelper.GetColumnData(start_row, "rfultime", out str_start_rdt);
						DateTime.TryParse(str_start_rdt, out dt_start_rdt);
						int i_times = Data_dbDataHelper.GetColumnIntData(start_row, "times");
						string str_code = string.Empty;
						Data_dbDataHelper.GetColumnData(start_row, "code", out str_code);
						DataRow[] end_rows = dtData_LearningStatus.Select("actions='" + Global.LearningActionsMap.LessonAction_EndLearning + "' and code='" + str_code + "'");
						TimeSpan timeSpan = new TimeSpan();
						bool isEnded = false;
                        string end_dt = string.Empty;
						if (end_rows.Length > 0)
						{
							string str_end_rdt = string.Empty;
							Data_dbDataHelper.GetColumnData(end_rows[0], "rfultime", out str_end_rdt);
							DateTime dt_end_rdt = new DateTime();
							DateTime.TryParse(str_end_rdt, out dt_end_rdt);
							if (dt_end_rdt.Year == dt_start_rdt.Year && dt_end_rdt.Month == dt_start_rdt.Month && dt_end_rdt.Day == dt_start_rdt.Day)
							{
								isEnded = true;
								timeSpan = dt_end_rdt - dt_start_rdt;
                                end_dt = dt_end_rdt.Year + "-" + dt_end_rdt.Month + "-" + dt_end_rdt.Day;
							}
						}
						XmlNode timeItemNode = Util_XmlOperHelper.CreateNode(doc_Result, "item", "");
                        Util_XmlOperHelper.SetAttribute(timeItemNode, "hours", timeSpan.Hours.ToString());
                        Util_XmlOperHelper.SetAttribute(timeItemNode, "minutes", timeSpan.Minutes.ToString());
                        Util_XmlOperHelper.SetAttribute(timeItemNode, "dt", end_dt != string.Empty ? end_dt : DateTime.Now.ToString("yyyy-MM-dd"));
                        timelineNode.AppendChild(timeItemNode);
					}
				}

				return Content(doc_Result.OuterXml);
			}
			catch(Exception err)
			{
				return Content(err.Message + "|" + err.StackTrace);
			}
		}
	}
}