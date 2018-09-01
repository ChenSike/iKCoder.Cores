using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;

namespace CoreBasic.Filter
{
    public class Filter_ConnectDB : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{
			ControllerBase_Std Base_Controller = context.Controller as ControllerBase_Std;
			Base_Controller._appLoader.CloseDB();
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			ControllerBase_Std Base_Controller = context.Controller as ControllerBase_Std;
			Base_Controller._appLoader.ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_BASIC);
			Base_Controller._appLoader.LoadSPS(Global.GlobalDefines.DB_SPSMAP_FILE);
		}
	}
}
