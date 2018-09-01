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
    public class Filter_UserAuthrization : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			ControllerBase_Std Base_Controller = context.Controller as ControllerBase_Std;
			if (Global.LoginServices.verify_logined_token(Base_Controller._appLoader.get_ClientToken(context.HttpContext.Request, "student_token")))
			{
				return;
			}
			else
			{
				ContentResult resultObj = new ContentResult();
				resultObj.Content = MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]);
				context.Result = resultObj;
			}			
		}
	}
}
