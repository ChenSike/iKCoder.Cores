using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using System.Xml;

namespace CoreBasic.Controllers.Common_Services
{
    [Produces("application/json")]
    [Route("api/Common_Services_GetCSActions")]
    public class Common_Services_GetCSActionsController : Controller
    {
		[ServiceFilter(typeof(Filter.Filter_OperatorCheck))]
		public IActionResult action()
		{
			MessageHelper messageHelper = new MessageHelper();
			messageHelper.CreateNewDoc();
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Get_ActiveDialog,"");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Get_DialogContent, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Set_OpenDialog, "");
			return Content(messageHelper.GetMessageDocString());
		}
    }
}