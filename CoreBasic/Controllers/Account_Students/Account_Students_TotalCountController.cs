using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using iKCoderComps;
using iKCoderSDK;

namespace CoreBasic.Controllers.Account_Students
{
	[Produces("application/json")]
	[Route("api/Account_Students_TotalCount")]
	public class Account_Students_TotalCountController : ControllerBase_Std
	{
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[HttpGet]
		public ContentResult actionResult()
		{
			DataTable dataTable = _appLoader.ExecuteSQL(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, "select count(*) as total from account_students");
			return Content(Data_dbDataHelper.ActionConvertDTtoXMLString(dataTable));
		}
	}
}