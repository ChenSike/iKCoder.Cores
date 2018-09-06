using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;

namespace AppMain.Controllers.System
{
    [Route("api/system_get_host")]
    [ApiController]
    public class System_Get_HostController : ControllerBase
    {
		[HttpGet]
		public ContentResult Action()
		{
			string serverIP = HttpContext.Connection.LocalIpAddress.ToString();
			return Content(MessageHelper.ExecuteSucessful("HOST_IP", serverIP));
		}
    }
}