using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using iKCoderComps;

namespace CoreBasic.Controllers.Common_Services
{
    [Produces("application/json")]
    [Route("api/Common_Services_GetHost")]
    public class Common_Services_GetHostController : Controller
    {
		[HttpGet]
		public ContentResult Action()
		{
			string serverIP = HttpContext.Connection.LocalIpAddress.ToString();
			return Content(MessageHelper.ExecuteSucessful("HOST_IP", serverIP));
		}
    }
}