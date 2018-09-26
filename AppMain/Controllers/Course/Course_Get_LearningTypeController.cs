using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using iKCoderComps;
using iKCoderSDK;
using System.Text;

namespace AppMain.Controllers.Course
{
    [Route("api/Course_Get_LearningType")]
    [ApiController]
    public class Course_Get_LearningTypeController : BaseController.BaseController_AppMain
    {
		public ContentResult Action()
		{
			StringBuilder docReturn = new StringBuilder();
			docReturn.Append("<root>");
			docReturn.Append("<type value='" + Global.LearningTypeMap.Type_Exp + "'");
			docReturn.Append("<type value='" + Global.LearningTypeMap.Type_Lesson + "'");
			docReturn.Append("</root>");
			return Content(docReturn.ToString());
		}
    }
}