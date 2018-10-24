using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Profiles_Students
{
	[Produces("application/text")]
	[Route("api/Profiles_Students_SetTextInfo")]
	public class Profiles_Students_SetTextInfoController : ControllerBase_Std
	{
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpGet]
		public ContentResult actionResult(string sex, string nickname, string birthday, string state, string city, string realname,string country = "China")
		{
			try
			{
				string token = _appLoader.get_ClientToken(Request, "student_token");
				Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
				Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@uid", activeItem.name);
				DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
				string id = string.Empty;
				Data_dbDataHelper.GetColumnData(dtData.Rows[0], "id", out id);
				paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@id", id);
				if (!string.IsNullOrEmpty(nickname))
				{
					paramsMap_for_profle.Add("@nickname", nickname);
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					paramsMap_for_profle.Remove("@nickname");
				}
				if (!string.IsNullOrEmpty(birthday))
				{
					paramsMap_for_profle.Add("@birthday", birthday);
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					paramsMap_for_profle.Remove("@birthday");
				}
				if (!string.IsNullOrEmpty(country))
				{
					paramsMap_for_profle.Add("@country", country);
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					paramsMap_for_profle.Remove("@country");
				}
				if (!string.IsNullOrEmpty(state))
				{
					paramsMap_for_profle.Add("@state", state);
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					paramsMap_for_profle.Remove("@state");

				}
				if (!string.IsNullOrEmpty(city))
				{
					paramsMap_for_profle.Add("@city", city);
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					paramsMap_for_profle.Remove("@city");

				}
				if (!string.IsNullOrEmpty(sex))
				{
					paramsMap_for_profle.Add("@sex", sex);
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					paramsMap_for_profle.Remove("@sex");

				}
				if (!string.IsNullOrEmpty(realname))
				{
					paramsMap_for_profle.Add("@realname", realname);
					_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					paramsMap_for_profle.Remove("@realname");

				}
				return Content(MessageHelper.ExecuteSucessful());
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse());
			}

		}
	}
}