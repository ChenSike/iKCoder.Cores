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
    [Produces("application/json")]
    [Route("api/Common_Services_CreateSPS")]
    public class Common_Services_CreateSPSController : ControllerBase_Std
    {
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_OperatorCheck))]
		[HttpGet]
		public ContentResult Action()
		{
			if (_appLoader.CreateSPS(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.GlobalDefines.DB_SPSMAP_FILE))
			{
				return Content(MessageHelper.ExecuteSucessful());
			}
			else
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
    }
}