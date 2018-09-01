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
    [Route("api/Relations_Students_GetDoSearch")]
    public class Relations_Students_GetDoSearchController : ControllerBase_Std
    {
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpGet]
		public ContentResult Action(string keyvalue)
		{
			Dictionary<string, string> paramsMap = new Dictionary<string, string>();
			paramsMap.Add("@uid", keyvalue);
			paramsMap.Add("@nickname", keyvalue);
			DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap);
			return Content(MessageHelper.TransDatatableToXML(dtData));
		}
    }
}