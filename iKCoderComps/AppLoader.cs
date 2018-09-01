using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderSDK;
using System.Xml;
using System.Data;
using MySql.Data;
using Microsoft.AspNetCore.Cors;
using System.IO;
using System.Text;


namespace iKCoderComps
{
    public class AppLoader
    {
		public static void LoadConfiguration_AllowCros(ref IServiceCollection services)
		{
			services.AddCors(options =>
			options.AddPolicy("AllowSameDomain", builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
		}

		protected Dictionary<string, Dictionary<string, string>> Map_ApiConfigs = new Dictionary<string, Dictionary<string, string>>();
		protected Dictionary<String, class_Data_SqlSPEntry> Map_SPS = new Dictionary<string, class_Data_SqlSPEntry>();

		protected string Path_Api = string.Empty;

		protected class_Data_SqlConnectionHelper db_objectConnectionHelper = new class_Data_SqlConnectionHelper();
		protected Data_dbSqlHelper db_objectSqlHelper = new Data_dbSqlHelper();


		public void InitApiConfigs(string configFile)
		{

			Path_Api = AppContext.BaseDirectory;
			Basic_Config objApiConfig = new Basic_Config();
			objApiConfig.DoOpen(Path_Api + "\\config\\" + configFile);
			XmlNodeList sessionNodes = objApiConfig.GetSessionNodes();
			foreach (XmlNode activeSessionNode in sessionNodes)
			{
				string name = objApiConfig.GetAttrValue(activeSessionNode, "name");
				Dictionary<string, string> mapAttrs = new Dictionary<string, string>();
				List<XmlAttribute> sessionAttrs = objApiConfig.GetSessionAttrs(name);
				objApiConfig.SwitchToAESModeON();
				foreach (XmlAttribute activeAttr in sessionAttrs)
				{
					if (activeAttr.Name != "name")
						mapAttrs.Add(activeAttr.Name, objApiConfig.GetAttrValue(activeSessionNode, activeAttr.Name));
				}
				Map_ApiConfigs.Add(name, mapAttrs);
			}
		}

		


		public bool ExecuteInsert(string connectionkey, string spname, Dictionary<string, string> mapparams)
		{
			if (Map_SPS.ContainsKey(spname))
			{
				class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
				objSPEntry.ClearAllParamsValues();
				foreach (string columnname in mapparams.Keys)
				{
					objSPEntry.ModifyParameterValue(columnname, mapparams[columnname]);
				}
				return db_objectSqlHelper.ExecuteInsertSP(objSPEntry, db_objectConnectionHelper, connectionkey);
			}
			else
			{
				return false;
			}
		}

		public bool ExecuteUpdate(string connectionkey, string spname, Dictionary<string, string> mapparams)
		{
			if (Map_SPS.ContainsKey(spname))
			{
				class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
				objSPEntry.ClearAllParamsValues();
				foreach (string columnname in mapparams.Keys)
				{
					objSPEntry.ModifyParameterValue(columnname, mapparams[columnname]);
				}
				return db_objectSqlHelper.ExecuteUpdateSP(objSPEntry, db_objectConnectionHelper, connectionkey);
			}
			else
			{
				return false;
			}
		}

		public DataTable ExecuteSelectWithMixedConditionsReturnDT(string connectionkey, string spname, Dictionary<string, string> mapparams)
		{
			if (Map_SPS.ContainsKey(spname))
			{
				class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
				objSPEntry.ClearAllParamsValues();
				foreach (string columnname in mapparams.Keys)
				{
					objSPEntry.ModifyParameterValue(columnname, mapparams[columnname]);
				}
				return db_objectSqlHelper.ExecuteSelectSPMixedConditionsForDT(objSPEntry, db_objectConnectionHelper, connectionkey);
			}
			else
			{
				return null;
			}
		}

		public DataTable ExecuteSelectWithConditionsReturnDT(string connectionkey, string spname, Dictionary<string, string> mapparams)
		{
			if (Map_SPS.ContainsKey(spname))
			{
				class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
				objSPEntry.ClearAllParamsValues();
				foreach (string columnname in mapparams.Keys)
				{
					objSPEntry.ModifyParameterValue(columnname, mapparams[columnname]);
				}
				return db_objectSqlHelper.ExecuteSelectSPConditionForDT(objSPEntry, db_objectConnectionHelper, connectionkey);
			}
			else
			{
				return null;
			}
		}

		public DataTable ExecuteSelect(string connectionkey, string spname)
		{
			if (Map_SPS.ContainsKey(spname))
			{
				class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
				objSPEntry.ClearAllParamsValues();
				return db_objectSqlHelper.ExecuteSelectSPForDT(objSPEntry, db_objectConnectionHelper, connectionkey);
			}
			else
			{
				return null;
			}
		}

		public class_data_PlatformDBDataReader ExecuteSelectWithMixedConditionsReturnDR(string connectionkey, string spname, Dictionary<string, string> mapparams)
		{
			if (Map_SPS.ContainsKey(spname))
			{
				class_data_MySqlSPEntry objSPEntry = (class_data_MySqlSPEntry)Map_SPS[spname];
				objSPEntry.ClearAllParamsValues();
				foreach (string columnname in mapparams.Keys)
				{
					objSPEntry.ModifyParameterValue(columnname, mapparams[columnname]);
				}
				return db_objectSqlHelper.ExecuteSelectSPConditionForDR(objSPEntry, db_objectConnectionHelper, connectionkey);
			}
			else
			{
				return null;
			}
		}

		public void ConnectDB(string keyname)
		{
			string server = Map_ApiConfigs[keyname]["server"];
			string uid = Map_ApiConfigs[keyname]["uid"];
			string pwd = Map_ApiConfigs[keyname]["pwd"];
			string db = Map_ApiConfigs[keyname]["db"];
			db_objectConnectionHelper.Set_NewConnectionItem(keyname, server, uid, pwd, db, enum_DatabaseType.MySql);
		}

		public void LoadSPS(string spsmapfile)
		{
			XmlDocument spsmapdoc = new XmlDocument();
			spsmapdoc.Load(Path_Api + "\\config\\" + spsmapfile);
			Map_SPS = db_objectSqlHelper.ActionAutoLoadingAllSPSFromMap(spsmapdoc);
		}

		public void CloseDB()
		{
			db_objectConnectionHelper.Action_CloseAllActionConnection();
		}


		public bool VerifyNotEmpty(List<string> list)
        {
            foreach (string item in list)
            {
                if (string.IsNullOrEmpty(item))
                    return false;
            }
            return true;
        }

		public bool VerifyNotEmpty(Dictionary<string, string> list)
		{
			foreach (string key in list.Keys)
			{
				if (string.IsNullOrEmpty(list[key]))
					return false;
			}
			return true;
		}

		public string GetQueryParam(HttpRequest httpRequest, string paramname)
		{
			if (!string.IsNullOrEmpty(paramname))
			{
				if (httpRequest.Query.ContainsKey(paramname))
				{
					return httpRequest.Query[paramname].ToString();
				}
				else
				{
					return string.Empty;
				}
			}
			else
				return string.Empty;
		}

		public string GetCookieValue(HttpRequest httpRequest, string paramname)
		{
			if (httpRequest.Cookies.ContainsKey(paramname))
				return httpRequest.Cookies[paramname].ToString();
			else
				return string.Empty;
		}


		public object get_SessionObject(ISession session, string sessionName)
		{
			if (session.Keys.Contains(sessionName))
			{
				byte[] bufferData = session.Get(sessionName);
				return Util_Common.Bytes2Object(bufferData);
			}
			else
				return null;
		}


		public void clear_logined_token(HttpRequest httpRequest, HttpResponse httpResponse , ISession session , string tokenname)
		{
			if (httpRequest.Cookies.ContainsKey(tokenname))
			{
				httpResponse.Cookies.Delete(tokenname);
				session.Clear();
			}
		}


		public string get_ClientToken(HttpRequest httpRequest, string tokenname)
		{
			string tokenFromClient = string.Empty;
			if (httpRequest.Cookies.ContainsKey(tokenname))
			{
				tokenFromClient = httpRequest.Cookies[tokenname];
			}
			else if (GetQueryParam(httpRequest,tokenname) != string.Empty)
			{
				tokenFromClient = GetQueryParam(httpRequest, tokenname);
			}
			return tokenFromClient;
		}

		public string get_PostData(HttpRequest httpRequest)
		{
			Stream stream = httpRequest.Body;
			byte[] buffer = new byte[httpRequest.ContentLength.Value];
			stream.Read(buffer, 0, buffer.Length);
			string result = Encoding.UTF8.GetString(buffer);
			return result;
		}

	}
}
