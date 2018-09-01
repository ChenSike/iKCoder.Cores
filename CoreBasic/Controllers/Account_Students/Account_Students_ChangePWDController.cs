using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Account_Students
{
    [Produces("application/text")]
    [Route("api/Account_Students_ChangePWD")]
    public class Account_Students_ChangePWDController : ControllerBase_Std
	{
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpPost]
		public ContentResult actionResult(string oldpwd, string newpwd)
		{
			Global.ItemAccountStudents activeItem = _appLoader.get_SessionObject(HttpContext.Session, "student_item") as Global.ItemAccountStudents;
			Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
			paramsMap_for_profle.Add("@id", activeItem.id);
			paramsMap_for_profle.Add("@password", oldpwd);
			DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
			if (dtData != null && dtData.Rows.Count > 0)
			{
				paramsMap_for_profle["@password"] = newpwd;
				_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
				return Content(MessageHelper.ExecuteSucessful());
			}
			else
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
    }
}