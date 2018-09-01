using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Messages_Students
{
    [Produces("application/text")]
    [Route("api/Messages_Students_GetDialogue")]
    public class Messages_Students_GetDialogueController : ControllerBase_Std
    {
		[HttpGet]
		public ContentResult Action(string suname)
		{
			if (Global.LoginServices.verify_logined_token(_appLoader.get_ClientToken(Request, "student_token")))
			{
				string token = _appLoader.get_ClientToken(Request, "student_token");
				Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
				Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@puname", activeItem.name);
				paramsMap_for_profle.Add("@accetped", "0");
				DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_relations_students, paramsMap_for_profle);
				return Content(MessageHelper.TransDatatableToXML(dtData));
			}
			else
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Login_Needed], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Login_Needed]));
			}
		}
    }
}