using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace AppMain.Controllers.Demo
{
    [Route("api/Demo_Get_RAWContent")]
    [ApiController]
    public class Demo_Get_RAWContentController : BaseController.BaseController_AppMain
    {
        [ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
        [ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
        [HttpGet]
        public ContentResult Action(string symbol)
        {
            Dictionary<string, string> paramsMap = new Dictionary<string, string>();
            paramsMap.Add("symbol",symbol);
            DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_store_demo_raw, paramsMap);
            if(dtData!=null && dtData.Rows.Count>0)
            {
                string content = string.Empty;
                Data_dbDataHelper.GetColumnData(dtData.Rows[0], "content", out content);
                return Content(content);
            }
            else
            {
                return Content(MessageHelper.ExecuteFalse());
            }
        }
    }
}