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

namespace AppMain.Controllers.BaseController
{
	public class BaseController_AppMain : ControllerBase_Std
	{
		//public static string ROOT_SERVER = "http://wwww.ikcoder.com/corebasic/api/";
		public static string ROOT_SERVER = "http://127.0.0.1/corebasic/api/";
		public static string SYBOL_TOKEN_BASIC = "student_token";

		public string RequestForString(string Url,bool takeToken = false)
		{
			if (takeToken)
			{
				string token = _appLoader.get_ClientToken(Request,SYBOL_TOKEN_BASIC);
				Url = Url + "?" + SYBOL_TOKEN_BASIC + "=" + token;
				Util_NetServices netObj = new Util_NetServices();
				string result = netObj.RequestWithGet(Url).ToString();
				return result;
			}
			else
				return string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// 1.name
		/// 2.regtime
		/// 3.id
		/// 4.lastvisited
		/// </returns>
		public string GetAccountInfoFromBasicController(string attrname)
		{
			if (string.IsNullOrEmpty(attrname))
				return string.Empty;
			else
			{
				string token = _appLoader.get_ClientToken(Request, SYBOL_TOKEN_BASIC);
				string Url = ROOT_SERVER + "Account_Students_GetCurrentAccountInfo?" + SYBOL_TOKEN_BASIC + "=" + token;
				string result = RequestForString(Url, true);
				XmlDocument resultDoc = new XmlDocument();
				resultDoc.LoadXml(result);
				XmlNode rootNode = resultDoc.SelectSingleNode("/root");
				string returnResult = Util_XmlOperHelper.GetAttrValue(rootNode, attrname);
				return returnResult;
			}
		}

		public bool VerifyToken()
		{
			string token = _appLoader.get_ClientToken(Request,SYBOL_TOKEN_BASIC);
			if(token==string.Empty)
			{
				return false;
			}
			else
			{
				try
				{
					Util_NetServices netObj = new Util_NetServices();
					string result = netObj.RequestWithGet(ROOT_SERVER + "Account_Students_SignStatus?" + SYBOL_TOKEN_BASIC + "=" + token).ToString();
					XmlDocument resultDoc = new XmlDocument();
					resultDoc.LoadXml(result);
					XmlNode executedNode = resultDoc.SelectSingleNode("/root/executed");
					if (executedNode != null)
					{
						if (Util_XmlOperHelper.GetNodeValue(executedNode) == "true")
						{
							return true;
						}
						else
							return false;
					}
					else
						return false;
				}
				catch
				{
					return false;
				}
			}
		}

	}
}
