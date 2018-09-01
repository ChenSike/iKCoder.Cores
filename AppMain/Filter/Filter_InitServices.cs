using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;

namespace AppMain.Filter
{
    public class Filter_InitServices: IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			Controllers.BaseController.BaseController_AppMain Base_Controller = context.Controller as Controllers.BaseController.BaseController_AppMain;
			Base_Controller._appLoader.InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
		}
	}
}
