﻿using System;
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
	[Route("api/course_get_coursepackage")]
	[ApiController]
	public class Course_Get_CoursesPackageController : BaseController.BaseController_AppMain
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
				Dictionary<string, string> paramsMap = new Dictionary<string, string>();
				paramsMap.Add("@uid", uname);
				DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_coursepackage, paramsMap);
				List<string> lstCoursesFromPackage = new List<string>();
				foreach (DataRow activeDR in dtData.Rows)
				{
					string courseid = string.Empty;
					Data_dbDataHelper.GetColumnData(activeDR, "course_name", out courseid);
					string overdate = string.Empty;
					Data_dbDataHelper.GetColumnData(activeDR, "overdate", out overdate);
					DateTime dtOverdate = DateTime.Now;
					DateTime.TryParse(overdate, out dtOverdate);
					if (dtOverdate <= DateTime.Now)
					{
						lstCoursesFromPackage.Add(courseid);
					}
				}
				XmlDocument returnDoc = new XmlDocument();
				returnDoc.LoadXml("<root></root>");
				dtData = _appLoader.ExecuteSelect(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_main);
				foreach (DataRow activeRow in dtData.Rows)
				{
					string course_name = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "name", out course_name);
					string course_id = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "id", out course_id);
					string course_title = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "title", out course_title);
					string course_isfree = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "isfree", out course_isfree);
					string course_discount = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "discount", out course_discount);
					string course_price = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "price", out course_price);
					string course_enable = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "enabled", out course_enable);
					string course_diff = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "diff", out course_diff);
					string course_udma = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "udma", out course_udma);
					string course_des = string.Empty;
					Data_dbDataHelper.GetColumnData(activeRow, "des", out course_des);
					XmlNode newItemNode = Util_XmlOperHelper.CreateNode(returnDoc, "item", "");
					Util_XmlOperHelper.SetAttribute(newItemNode, "name", course_name);
					Util_XmlOperHelper.SetAttribute(newItemNode, "id", course_id);
					Util_XmlOperHelper.SetAttribute(newItemNode, "title", course_title);
					Util_XmlOperHelper.SetAttribute(newItemNode, "price", course_price);
					Util_XmlOperHelper.SetAttribute(newItemNode, "discount", course_discount);
					Util_XmlOperHelper.SetAttribute(newItemNode, "isfress", course_isfree);
					Util_XmlOperHelper.SetAttribute(newItemNode, "diff", course_diff);
					Util_XmlOperHelper.SetAttribute(newItemNode, "udma", course_udma);
					Util_XmlOperHelper.SetAttribute(newItemNode, "des", course_des);
					if (lstCoursesFromPackage.Contains(course_name) || course_isfree == "1")
					{
						Util_XmlOperHelper.SetAttribute(newItemNode, "access", "1");
					}
					else
					{
						Util_XmlOperHelper.SetAttribute(newItemNode, "access", "0");
					}
					Util_XmlOperHelper.SetAttribute(newItemNode, "enable", course_enable);
					returnDoc.SelectSingleNode("/root").AppendChild(newItemNode);
				}
				return Content(returnDoc.OuterXml);
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
			}			
		}
	}
}