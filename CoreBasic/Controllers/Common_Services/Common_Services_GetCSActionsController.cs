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
    public class Common_Services_GetCSActionsController : ControllerBase_Std
    {
		[ServiceFilter(typeof(Filter.Filter_OperatorCheck))]
		[HttpGet]
		public IActionResult action()
		{
			MessageHelper messageHelper = new MessageHelper();
			messageHelper.CreateNewDoc();
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Set_NewDialog,"");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Get_DialogContent, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Set_SendMessage, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Set_DelDialog, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Get_DialogList, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Get_RelationsList, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Get_RelationsSearch, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Get_RelationsAcceptableList, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Set_NewFriend, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Set_AcceptFriend, "");
			messageHelper.AddMessage("action", Global.ActionsMap.Action_Get_BatchArrProfile, "");
			return Content(messageHelper.GetMessageDocString());
		}
    }
}