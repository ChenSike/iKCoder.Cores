using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Relations_Students
{
    [Produces("application/text")]
    [Route("api/Relations_Students_GetList")]
    public class Relations_Students_GetListController : ControllerBase_Std
    {
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpGet]
		public ContentResult Action()
		{
			string token = _appLoader.get_ClientToken(Request, "student_token");
			Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
			Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
			paramsMap_for_profle.Add("@puname", activeItem.name);
			paramsMap_for_profle.Add("@accetped", "1");
			DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle);
			return Content(MessageHelper.TransDatatableToXML(dtData));
		}
    }
}