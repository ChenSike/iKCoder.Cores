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
    public class Filter_InitServices : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			ControllerBase_Std Base_Controller = context.Controller as ControllerBase_Std;
			Base_Controller._appLoader.InitApiConfigs(Global.GlobalDefines.SY_CONFIG_FILE);
		}
	}
}
