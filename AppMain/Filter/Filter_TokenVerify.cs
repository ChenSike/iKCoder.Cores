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
	public class Filter_TokenVerify : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{			
			
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			Controllers.BaseController.BaseController_AppMain Base_Controller = context.Controller as Controllers.BaseController.BaseController_AppMain;
			/*
			if (Base_Controller.VerifyToken())
			{
				return;
			}
			else
			{
				Global.MsgReturn_TokenVerify msgObj = new Global.MsgReturn_TokenVerify();
				ContentResult resultObj = new ContentResult();
				resultObj.Content = MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]);
				context.Result = resultObj;
			}
			*/
		}
	}
}
