﻿using System;
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
    [Route("api/Account_Students_Create")]
    public class Account_Students_CreateController : ControllerBase_Std
	{
        private string system_suname = "99999999999";

		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[HttpGet]
        public ContentResult actionResult(string uid, string pwd, string status = "0", string level = "0",string type="0")
        {
			try
			{
				Dictionary<string, string> activeParams = new Dictionary<string, string>();
				activeParams.Add("@name", uid);
				if (_appLoader.VerifyNotEmpty(activeParams))
				{					
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
					activeParams.Add("@password", pwd);
					activeParams.Add("@status", status);
					activeParams.Add("@type", type);
					Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
					paramsMap_for_profle.Add("@uid", uid);
					paramsMap_for_profle.Add("@sex", "1");
					paramsMap_for_profle.Add("@nickname", uid);
					paramsMap_for_profle.Add("@birthday", "2000-01-01");
					paramsMap_for_profle.Add("@country", "china");
					if (_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle))
					{
						if (_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_account_students, activeParams))
						{
                            paramsMap_for_profle.Clear();
                            paramsMap_for_profle.Add("@puname", uid);
                            paramsMap_for_profle.Add("@isacc", "1");
                            paramsMap_for_profle.Add("@suname", system_suname);
                            paramsMap_for_profle.Add("@message", "");
                            _appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle);
                            return Content(MessageHelper.ExecuteSucessful());
						}
						else
						{
							return Content(MessageHelper.ExecuteFalse());
						}
					}
					else
					{
						return Content(MessageHelper.ExecuteFalse());
					}
				}
				else
					return Content(MessageHelper.ExecuteFalse());
			}
			catch(Basic_Exceptions err)
			{
				return Content(MessageHelper.ExecuteFalse());
			}			
        }
    }
}