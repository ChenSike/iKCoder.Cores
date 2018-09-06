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
	public class Filter_OperatorCheck : IActionFilter
	{

		private string operatorKey = "ikcoder_operator";

		public void OnActionExecuted(ActionExecutedContext context)
		{
			
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.HttpContext.Request.Query.ContainsKey("operator"))
			{
				if (!(context.HttpContext.Request.Query["operator"].ToString() == operatorKey))
				{
					ContentResult resultObj = new ContentResult();
					resultObj.Content = MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Invalidated_OperatorKey], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Invalidated_OperatorKey]);
					context.Result = resultObj;
				}
			}
		}
	}
}
