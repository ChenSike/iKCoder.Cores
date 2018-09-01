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
    [Route("api/Account_Students_LoginWithCheckCode")]
    public class Account_Students_LoginWithCheckCodeController : ControllerBase_Std
	{
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[HttpGet]
		public ContentResult actionResult(string name, string pwd,string checkcode)
		{
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("name", name);
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
					DataTable dtUser = new DataTable();
					dtUser = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams);
					if (dtUser != null && dtUser.Rows.Count == 1)
					{
						string uid = string.Empty;
						string password = string.Empty;
						Data_dbDataHelper.GetColumnData(dtUser.Rows[0], "id", out uid);
						Data_dbDataHelper.GetColumnData(dtUser.Rows[0], "password", out password);
						if (password == pwd)
						{
							Global.ItemAccountStudents newItem = Global.ItemAccountStudents.CreateNewItem(uid, name, pwd, "");
							Global.LoginServices.Push(newItem);
							Response.Cookies.Append("student_token", newItem.token);
							return Content(MessageHelper.ExecuteSucessful());
						}
						else
						{
							return Content(MessageHelper.ExecuteFalse());
						}
					}
					else
						return Content(MessageHelper.ExecuteFalse());
				}
				else
					return Content(MessageHelper.ExecuteSucessful());
			}
			catch (Basic_Exceptions err)
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
	}
}