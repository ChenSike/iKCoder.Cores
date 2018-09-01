using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Net.Http.Headers;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Profiles_Students
{
    [Produces("application/text")]
    [Route("api/Profiles_Students_GetTextInfo")]
    public class Profiles_Students_GetTextInfoController : ControllerBase_Std
	{
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpGet]
		public ContentResult actionResult()
		{
			try
			{
				string token = _appLoader.get_ClientToken(Request, "student_token");
				Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
				Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@uid", activeItem.name);
				DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
				if (dtData != null && dtData.Rows.Count > 0)
				{
					string nickName = string.Empty;
					string birthday = string.Empty;
					string country = string.Empty;
					string state = string.Empty;
					string city = string.Empty;
					string realname = string.Empty;
					Dictionary<string, string> resturnMap = new Dictionary<string, string>();
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "nickname", out nickName);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "birthday", out birthday);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "country", out country);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "state", out state);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "city", out city);
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "realname", out realname);
					if (!string.IsNullOrEmpty(nickName))
						resturnMap.Add("nickname", nickName);
					if (!string.IsNullOrEmpty(birthday))
						resturnMap.Add("birthday", birthday);
					if (!string.IsNullOrEmpty(country))
						resturnMap.Add("country", country);
					if (!string.IsNullOrEmpty(state))
						resturnMap.Add("state", state);
					if (!string.IsNullOrEmpty(city))
						resturnMap.Add("city", city);
					if (!string.IsNullOrEmpty(realname))
						resturnMap.Add("realname", city);
					return Content(MessageHelper.ExecuteSucessfulDoc(resturnMap));
				}
				else
				{
					return Content(MessageHelper.ExecuteFalse());
				}
			}
			catch (Basic_Exceptions err)
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
    }
}