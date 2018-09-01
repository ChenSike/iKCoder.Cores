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
    [Route("api/Course_Set_LessonStatus")]
    [ApiController]
    public class Course_Set_LessonStatusController : BaseController.BaseController_AppMain
    {

		/*
		 * <root>
		 * <code>required</code>
		 * <step>optional</step>
		 * <type>optional</type>
		 * <doc>BASE64 DATA</doc>
		 * </root>
		 * 
		 */
		[ServiceFilter(typeof(AppMain.Filter.Filter_InitServices))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(AppMain.Filter.Filter_TokenVerify))]
		[HttpPost]
		public ContentResult Action()
		{
			string requestData = _appLoader.get_PostData(Request);
			if(string.IsNullOrEmpty(requestData))
			{
				return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Request_Invalidate], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Request_Invalidate]));
			}
			else
			{
				XmlDocument requestDoc = new XmlDocument();
				try
				{
					requestDoc.LoadXml(requestData);
				}
				catch
				{
					return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Request_Invalidate], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Request_Invalidate]));
				}
				XmlNode codeNode = requestDoc.SelectSingleNode("/root/code");
				XmlNode stepNode = requestDoc.SelectSingleNode("/root/step");
				XmlNode typeNode = requestDoc.SelectSingleNode("/root/type");
				XmlNode docNode = requestDoc.SelectSingleNode("/root/doc");
				string str_code = string.Empty;
				string str_step = string.Empty;
				string str_type = string.Empty;
				string str_doc = string.Empty;
				if(codeNode==null)
					return Content(MessageHelper.ExecuteFalse(Global.MsgMap.MsgCodeMap[Global.MsgKeyMap.MsgKey_Request_Invalidate], Global.MsgMap.MsgContentMap[Global.MsgKeyMap.MsgKey_Request_Invalidate]));
				str_code = Util_XmlOperHelper.GetNodeValue(codeNode);
				if(stepNode!=null)
					str_step = Util_XmlOperHelper.GetNodeValue(stepNode);
				if(typeNode!=null)
					str_type = Util_XmlOperHelper.GetNodeValue(typeNode);
				if(docNode!=null)
					str_doc = Util_XmlOperHelper.GetNodeValue(docNode);
				string uname = GetAccountInfoFromBasicController("name");
				Dictionary<string, string> paramsForBasic = new Dictionary<string, string>();
				paramsForBasic.Add("@uid", uname);
				paramsForBasic.Add("@lesson_code", str_code);
				DataTable dtData = _appLoader.ExecuteSelectWithMixedConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonstatus, paramsForBasic);
				paramsForBasic.Add("@current_step", str_step);
				paramsForBasic.Add("@current_statusdoc", str_doc);
				paramsForBasic.Add("@type", str_type);
				paramsForBasic.Add("@recorddt", DateTime.Now.ToString());
				if (dtData != null && dtData.Rows.Count == 1)
				{					
					if (_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonstatus, paramsForBasic))
					{
						return Content(MessageHelper.ExecuteSucessful());
					}
					else
					{
						return Content(MessageHelper.ExecuteFalse());
					}
				}
				else
				{
					if (_appLoader.ExecuteInsert(Global.GlobalDefines.DB_KEY_IKCODER_APPMAIN, Global.MapStoreProcedures.ikcoder_appmain.spa_operation_students_lessonstatus, paramsForBasic))
					{
						return Content(MessageHelper.ExecuteSucessful());
					}
					else
					{
						return Content(MessageHelper.ExecuteFalse());
					}
				}
			}
		}
		
    }
}