using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using iKCoderSDK;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iKCoderComps
{
    public class MessageHelper
    {

        public static string ExecuteSucessful()
        {
			return "<root><executed>" + "true" + "</executed></root>";
        }

        public static string ExecuteSucessful(string MSGCODE, string MSG)
        {
            return "<root><executed>true</executed><msgcode>" + MSGCODE + "</msgcode><msg>" + MSG + "</msg></root>";
        }

		public static string ExecuteSucessfulDoc(Dictionary<string,string> resultMAP)
		{
			StringBuilder returnMsg = new StringBuilder();
			returnMsg.Append("<root><executed>true</executed><return>");
			foreach(string resultMapKey in resultMAP.Keys)
			{
				returnMsg.Append("<item attr='" + resultMapKey + "' value='" + resultMAP[resultMapKey] + "'></item>");
			}
			returnMsg.Append("</return></root>");
			return returnMsg.ToString();
		}


		public static string ExecuteFalse()
        {
            return "<root><executed>false</executed></root>";
        }

        public static string ExecuteFalse(string MSGCODE, string MSG)
        {
            return "<root><executed>false</executed><msgcode>" + MSGCODE + "</msgcode><msg>" + MSG + "</msg></root>";
        }

		
        public static string AssetExecute(bool result)
        {
            if (result)
                return "<root><executed>true</executed></root>";
            else
                return "<root><executed>false</executed></root>";
        }

        public static string TransDatatableToXML(DataTable dtData)
        {
            XmlDocument _doc = new XmlDocument();
            _doc.LoadXml("<root></root>");
            foreach (DataRow dr in dtData.Rows)
            {
                XmlNode rowNode = Util_XmlOperHelper.CreateNode(_doc, "row", "");
                _doc.SelectSingleNode("/root").AppendChild(rowNode);
                foreach (DataColumn dc in dtData.Columns)
                {
                    Util_XmlOperHelper.SetAttribute(rowNode, dc.ColumnName, dr[dc].ToString());
                }
            }
            return _doc.OuterXml;
        }
    }
}
