using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;

namespace CoreBasic.Controllers.Common_Services
{
    [Produces("application/text")]
    [Route("api/Common_Services_VerifyCheckCode")]
    public class Common_Services_VerifyCheckCodeController : ControllerBase_Std
    {
		[HttpGet]
		public ContentResult actionResult(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				return Content(MessageHelper.ExecuteFalse());
			}
			else
			{
				if(HttpContext.Session.Keys.Contains("checkcode"))
				{
					if(HttpContext.Session.GetString("checkcode")==code)
					{
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
		}
    }
}