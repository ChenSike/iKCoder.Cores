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
		public ContentResult actionResult(string sex, string nickname, string birthday, string state, string city, string realname, string country = "China")
		{
			Global.ItemAccountStudents activeItem = _appLoader.get_SessionObject(HttpContext.Session, "student_item") as Global.ItemAccountStudents;
			Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
			paramsMap_for_profle.Add("@uid", activeItem.name);
			DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
			string id = string.Empty;
			Data_dbDataHelper.GetColumnData(dtData.Rows[0], "id", out id);
			paramsMap_for_profle = new Dictionary<string, string>();
			paramsMap_for_profle.Add("@id", id);
			if (!string.IsNullOrEmpty(nickname))
				paramsMap_for_profle.Add("@nickname", nickname);
			if (!string.IsNullOrEmpty(birthday))
				paramsMap_for_profle.Add("@birthday", birthday);
			if (!string.IsNullOrEmpty(country))
				paramsMap_for_profle.Add("@country", country);
			if (!string.IsNullOrEmpty(state))
				paramsMap_for_profle.Add("@state", state);
			if (!string.IsNullOrEmpty(city))
				paramsMap_for_profle.Add("@city", city);
			if (!string.IsNullOrEmpty(realname))
				paramsMap_for_profle.Add("@realname", realname);
			if (_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle))
			{
				return Content(MessageHelper.ExecuteSucessful());
			}
			else
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
	}
}