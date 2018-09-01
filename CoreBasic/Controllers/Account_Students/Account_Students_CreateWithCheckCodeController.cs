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
	[Route("api/Account_Students_CreateWithCheckCode")]
	public class Account_Students_CreateWithCheckCodeController : ControllerBase_Std
	{
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[HttpGet]
		public ContentResult actionResult(string uid, string pwd, string checkcode, string status = "0", string level = "0")
		{
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("name", uid);
				if (_appLoader.VerifyNotEmpty(activeParams))
				{
					string checkcodefromsession = string.Empty;
					if (HttpContext.Session.Keys.Contains("checkcode"))
					{
						checkcodefromsession = HttpContext.Session.GetString("checkcode");
					}
					else
					{
						return Content(MessageHelper.ExecuteFalse("400", "null checkcode"));
					}
					if (checkcodefromsession != checkcode)
					{
						return Content(MessageHelper.ExecuteFalse("400", "wrong checkcode"));
					}
					DataTable activeDataTable = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams);
					if (activeDataTable == null)
						return Content(MessageHelper.ExecuteFalse());
					else
					{
						if (activeDataTable.Rows.Count > 0)
						{
							return Content(MessageHelper.ExecuteFalse("400", "Account Existed"));
						}
					}
					activeParams.Add("password", pwd);
					activeParams.Add("status", status);
					activeParams.Add("type", level);
					if (_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams))
						return Content(MessageHelper.ExecuteSucessful());
					else
						return Content(MessageHelper.ExecuteFalse());
				}
				else
					return Content(MessageHelper.ExecuteFalse());
			}
			catch (Basic_Exceptions err)
			{
				return Content(MessageHelper.ExecuteFalse());
			}			
		}
	}
}