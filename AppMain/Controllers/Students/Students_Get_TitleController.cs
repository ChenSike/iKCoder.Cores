using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;
using System.Xml;

namespace AppMain.Controllers.Students
{
    [Route("api/students_get_title")]
    [ApiController]
    public class Students_Get_TitleController : BaseController.BaseController_AppMain
    {
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpGet]
		public ContentResult Action()
		{
			try
			{
				XmlDocument resultDoc = new XmlDocument();
				resultDoc.LoadXml("<root></root>");
				string uname = GetAccountInfoFromBasicController("name");
				Dictionary<string, string> paramsForBasic = new Dictionary<string, string>();
				paramsForBasic.Add("@uid", uname);
				DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_course_exp, paramsForBasic);
				if(dtData!=null && dtData.Rows.Count>0)
				{
					DataRow currentRow = null;
					Data_dbDataHelper.GetActiveRow(dtData, 0, out currentRow);
					if (currentRow != null)
					{
						int exp_value = Data_dbDataHelper.GetColumnIntData(currentRow, "exp");
						DataTable dtTitle = _appLoader.ExecuteSelect(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_titles_defined);
						if(dtData!=null && dtData.Rows.Count>0)
						{
							foreach(DataRow activeRow in dtData.Rows)
							{
								string title_name = string.Empty;
								string title_titles = string.Empty;
								int title_exp_min = 0;
								int title_exp_max = 0;
								Data_dbDataHelper.GetColumnData(activeRow, "name", out title_name);
								Data_dbDataHelper.GetColumnData(activeRow, "titles", out title_titles);
								title_exp_min = Data_dbDataHelper.GetColumnIntData(activeRow, "exp_min");
								title_exp_max = Data_dbDataHelper.GetColumnIntData(activeRow, "exp_max");
								if (exp_value >= title_exp_max)
								{
									XmlNode itemNode = Util_XmlOperHelper.CreateNode(resultDoc, "item", "");
									resultDoc.SelectSingleNode("/root").AppendChild(itemNode);
									Util_XmlOperHelper.SetAttribute(itemNode, "name", title_name);
									Util_XmlOperHelper.SetAttribute(itemNode, "title", title_titles);
									Util_XmlOperHelper.SetAttribute(itemNode, "isget", "1");
								}
								else
								{
									XmlNode itemNode = Util_XmlOperHelper.CreateNode(resultDoc, "item", "");
									resultDoc.SelectSingleNode("/root").AppendChild(itemNode);
									Util_XmlOperHelper.SetAttribute(itemNode, "name", title_name);
									Util_XmlOperHelper.SetAttribute(itemNode, "title", title_titles);
									Util_XmlOperHelper.SetAttribute(itemNode, "expvalue", exp_value.ToString());
									Util_XmlOperHelper.SetAttribute(itemNode, "isget", "0");
								}
								
							}
						}
					}
				}
				return Content(resultDoc.OuterXml);
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
			}
		}
    }
}