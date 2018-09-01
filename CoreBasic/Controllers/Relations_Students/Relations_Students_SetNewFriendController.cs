using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using iKCoderComps;
using iKCoderSDK;

namespace CoreBasic.Controllers.Relations_Students
{
    [Produces("application/text")]
    [Route("api/Relations_Students_SetNewFriend")]
    public class Relations_Students_SetNewFriendController : ControllerBase_Std
    {
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpGet]
		public ContentResult Action(string suname)
		{

			string token = _appLoader.get_ClientToken(Request, "student_token");
			Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
			Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
			paramsMap_for_profle.Add("@puname", activeItem.name);
			DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle);
			if (dtData != null && dtData.Rows.Count > 0)
			{
				DataRow[] rows = dtData.Select("sname='" + suname + "' and accetped='1'");
				if (rows.Length > 0)
				{
					return Content(MessageHelper.ExecuteFalse());
				}
			}
			paramsMap_for_profle.Add("@sumnae", suname);
			paramsMap_for_profle.Add("@accetped", "0");
			if (_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle))
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