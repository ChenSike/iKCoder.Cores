using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace AppMain.Filter
{
    public class Filter_AccessVerify : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Controllers.BaseController.BaseController_AppMain Base_Controller = context.Controller as Controllers.BaseController.BaseController_AppMain;
            string package_name = Base_Controller._appLoader.GetQueryParam(context.HttpContext.Request, "package_name");
            string uname = Base_Controller.GetAccountInfoFromBasicController("name");
            Dictionary<string, string> paramsMap = new Dictionary<string, string>();
            paramsMap.Add("@uid", uname);
            DataTable dtData = Base_Controller._appLoader.ExecuteSelectWithConditionsReturnDT(AppMain.Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, AppMain.Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_coursepackage, paramsMap);
            DataRow[] rows = dtData.Select("course_name='" + package_name + "'");
            if(rows.Length>0)
            {
                return;
            }
            else
            {
                ContentResult resultObj = new ContentResult();
                resultObj.Content = MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Package_AccessDenied], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Package_AccessDenied]);
                context.Result = resultObj;
            }            
        }
    }
}
