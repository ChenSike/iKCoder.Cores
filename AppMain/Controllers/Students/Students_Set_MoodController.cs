﻿using System;
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
    [Route("api/students_set_mood")]
    [ApiController]
    public class Students_Set_MoodController : BaseController.BaseController_AppMain
    {
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action(string mood = "1")
		{
			try
			{
				string uname = GetAccountInfoFromBasicController("name");
				Dictionary<string, string> paramsForBasic = new Dictionary<string, string>();
				paramsForBasic.Add("@uid", uname);
				DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_checkon, paramsForBasic);
				DataRow[] recordRows = dtData.Select("checkdate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
				if (recordRows.Length == 0)
				{
					paramsForBasic.Add("@mood", mood);
					paramsForBasic.Add("@checkdate", DateTime.Now.ToString("yyyy-MM-dd"));
					paramsForBasic.Add("@checktime", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
					_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_mood, paramsForBasic);
					return Content(MessageHelper.ExecuteSucessful());
				}
				else
				{
					return Content(MessageHelper.ExecuteFalse());
				}
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
			}			
		}
    }
}