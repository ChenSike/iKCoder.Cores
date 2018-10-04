using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;

namespace AppMain.Controllers.AppStudio
{
    [Route("api/AppStudio_Set_UpdateStoreItem")]
    [ApiController]
    public class AppStudio_Set_UpdateStoreItemController : BaseController.BaseController_AppMain
    {
		/*[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult action(string projectname)
		{
			string postData = _appLoader.get_PostData(HttpContext.Request);
			string base64PostData = Util_Common.Encoder_Base64(postData);
			Dictionary<string, string> paramsmap = new Dictionary<string, string>();
			string uname = GetAccountInfoFromBasicController("name");
			paramsmap.Add("@uid", uname);
			paramsmap.Add("@pname", projectname);
			paramsmap.Add("@contentdoc", base64PostData);
			paramsmap.Add("@imdt", str_code);
		}*/
    }
}