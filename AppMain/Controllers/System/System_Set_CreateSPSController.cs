using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;

namespace AppMain.Controllers.System
{
    [Route("api/[controller]")]
    [ApiController]
    public class System_Set_CreateSPSController : BaseController.BaseController_AppMain
    {
		[ServiceFilter(typeof(AppMain.Filter.Filter_OperatorVerify))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[HttpGet]
		public ContentResult Action()
		{
			if (_appLoader.CreateSPS(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.GlobalDefines.DB_SPSMAP_FILE))
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