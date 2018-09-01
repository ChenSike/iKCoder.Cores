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
	public class Filter_ConnectDB : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{
			Controllers.BaseController.BaseController_AppMain Base_Controller = context.Controller as Controllers.BaseController.BaseController_AppMain;
			Base_Controller._appLoader.CloseDB();
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			Controllers.BaseController.BaseController_AppMain Base_Controller = context.Controller as Controllers.BaseController.BaseController_AppMain;
			Base_Controller._appLoader.ConnectDB(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN);
			Base_Controller._appLoader.LoadSPS(Global.GlobalDefines.DB_SPSMAP_FILE);
			
		}
	}
}
