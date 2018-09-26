using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using System.Xml;
using iKCoderSDK;
using System.Text;

namespace AppMain.Controllers.Course
{
    [Route("api/Course_Get_LearningActions")]
    [ApiController]
    public class Course_Get_LearningActionsController : BaseController.BaseController_AppMain
    {
		[HttpGet]
		public ContentResult Action()
		{
			StringBuilder docReturn = new StringBuilder();
			docReturn.Append("<root>");
			docReturn.Append("<action value='" + Global.LearningActionsMap.LessonAction_StartLearning + "'");
			docReturn.Append("<action value='" + Global.LearningActionsMap.LessonAction_EndLearning + "'");
			docReturn.Append("</root>");
			return Content(docReturn.ToString());
		}
	}
}