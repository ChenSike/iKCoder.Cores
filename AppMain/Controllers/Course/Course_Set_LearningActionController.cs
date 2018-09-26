using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Xml;
using System.Data;

namespace AppMain.Controllers.Course
{
	[Route("api/Course_Set_LearningAction")]
	[ApiController]
	public class Course_Set_LearningActionController : BaseController.BaseController_AppMain
	{
		/*
		 *<root>
		 *<type>
		 * </type>
		 * <action>
		 * </action>
		 * <code>
		 * </code>
		 *</root>
		 */
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpPost]
		public ContentResult Action()
		{
			string strRequest = _appLoader.get_PostData(HttpContext.Request);
			try
			{
				if (string.IsNullOrEmpty(strRequest))
					return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Request_Invalidate], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Request_Invalidate]));
				XmlDocument requestDoc = new XmlDocument();
				requestDoc.LoadXml(strRequest);
				XmlNode typeNode = requestDoc.SelectSingleNode("/root/type");
				XmlNode actionNode = requestDoc.SelectSingleNode("/root/action");
				XmlNode codeNode = requestDoc.SelectSingleNode("/root/code");
				string str_code = Util_XmlOperHelper.GetNodeValue(typeNode);
				string str_action = Util_XmlOperHelper.GetNodeValue(actionNode);
				string str_type = Util_XmlOperHelper.GetNodeValue(actionNode);
				Dictionary<string, string> paramsmap = new Dictionary<string, string>();
				string uname = GetAccountInfoFromBasicController("name");
				paramsmap.Add("@uid", uname);
				paramsmap.Add("@rdt", DateTime.Now.ToString("yyyy-MM-dd"));
				paramsmap.Add("@actions", str_action);
				paramsmap.Add("@code", str_code);
				DataTable dtLearning = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_learninrecord, paramsmap);
				if (dtLearning != null)
				{
					if(dtLearning.Rows.Count>=1)
					{
						int times = Data_dbDataHelper.GetColumnIntData(dtLearning.Rows[0], "times");
						string id = string.Empty;
						Data_dbDataHelper.GetColumnData(dtLearning.Rows[0], "id", out id);
						paramsmap.Add("@times", (times++).ToString());
						paramsmap.Add("@id", id);
						paramsmap.Add("@rtime", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
						_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_learninrecord, paramsmap);
						return Content(MessageHelper.ExecuteSucessful());
					}
				}
				paramsmap.Add("@times","1");
				paramsmap.Add("@rtime", DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
				_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_learninrecord, paramsmap);
				return Content(MessageHelper.ExecuteSucessful());
			}
			catch
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Fetch_Error], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Fetch_Error]));
			}
		}
	}
}