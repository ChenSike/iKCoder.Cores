using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using iKCoderComps;
using iKCoderSDK;

namespace AppMain.Controllers.Demo
{
    [Route("api/Demo_Set_RAWContent")]
    [ApiController]
    public class Demo_Set_RAWContentController : BaseController.BaseController_AppMain
    {
        [ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
        [ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
        [HttpPost]
        public ContentResult Action()
        {
            string data = _appLoader.get_PostData(HttpContext.Request);
            Dictionary<string, string> paramsMap = new Dictionary<string, string>();
            DataTable dtData = _appLoader.ExecuteSelect(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_store_demo_raw);
            foreach(DataRow dr in dtData.Rows)
            {
                string rdt = string.Empty;
                Data_dbDataHelper.GetColumnData(dr, "rdt", out rdt);
                DateTime tmpRDT = DateTime.Parse(rdt);
                if((DateTime.Now - tmpRDT).Days>=7)
                {
                    string id = string.Empty;
                    Data_dbDataHelper.GetColumnData(dr, "id", out id);
                    paramsMap.Clear();
                    paramsMap.Add("id", id);
                    _appLoader.ExecuteDeleteWithID(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_store_demo_raw, paramsMap);
                }
            }
            paramsMap.Clear();
            paramsMap.Add("rdt", DateTime.Now.ToString("yyyy-MM-dd"));
			string symbol = Guid.NewGuid().ToString();
			paramsMap.Add("symbol", symbol);
			paramsMap.Add("content", data);
			_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_store_demo_raw, paramsMap);
			return Content(MessageHelper.ExecuteSucessful("symbol", symbol));

		}
        
    }
}