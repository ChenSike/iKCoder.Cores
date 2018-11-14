using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace AppMain.Controllers.Course
{
    [Route("api/Course_Get_PackageLessonDocController")]
    [ApiController]
    public class Course_Get_PackageLessonDocController : BaseController.BaseController_AppMain
    {
        [ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
        [ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
        [ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
        [ServiceFilter(typeof(AppMain.Filter.Filter_AccessVerify))]
        [HttpGet]
        public ContentResult Action(string package_name)
        {
            
        }
    }
}