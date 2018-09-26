using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMain.Global
{

	public class MsgKeyMap
	{
		public static string MsgKey_Login_Needed = "MsgKey_Login_Needed";
		public static string MsgKey_Fetch_Error = "MsgKey_Fetch_Error";
		public static string MsgKey_Request_Invalidate = "MsgKey_Request_Invalidate";
		public static string MsgKey_Invalidated_OperatorKey = "MsgKey_Invalidated_OperatorKey";
	}

    public class MsgMap
    {
		public static Dictionary<string, string> MsgCodeMap = new Dictionary<string, string>();
		public static Dictionary<string, string> MsgContentMap = new Dictionary<string, string>();

		static MsgMap()
		{
			MsgCodeMap.Add(MsgKeyMap.MsgKey_Login_Needed, "600");
			MsgContentMap.Add(MsgKeyMap.MsgKey_Login_Needed, "Login Needed");
			MsgCodeMap.Add(MsgKeyMap.MsgKey_Fetch_Error, "400");
			MsgContentMap.Add(MsgKeyMap.MsgKey_Fetch_Error, "Fail to fetch data.");
			MsgCodeMap.Add(MsgKeyMap.MsgKey_Request_Invalidate, "410");
			MsgContentMap.Add(MsgKeyMap.MsgKey_Request_Invalidate, "Invalidate Request Data.");
			MsgCodeMap.Add(MsgKeyMap.MsgKey_Invalidated_OperatorKey, "420");
			MsgContentMap.Add(MsgKeyMap.MsgKey_Invalidated_OperatorKey, "Invalidate Operator Key.");

		}

    }
}
