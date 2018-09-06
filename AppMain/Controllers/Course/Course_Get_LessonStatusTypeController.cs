using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AppMain.Controllers.Course
{
    [Route("api/Course_Get_LessonStatusType")]
    [ApiController]
    public class Course_Get_LessonStatusTypeController : ControllerBase
    {
		[HttpGet]
		public ContentResult Action()
		{
			StringBuilder resultDoc = new StringBuilder();
			resultDoc.Append("<root>");
			resultDoc.Append("<item value='" + Global.GlobalDefines.lessonstatus_type.process.ToString() + "' title='process'></item>");
			resultDoc.Append("<item value='" + Global.GlobalDefines.lessonstatus_type.finished.ToString() + "' title='finished'></item>");
			resultDoc.Append("</root>");
			return Content(resultDoc.ToString());
		}
	}
}