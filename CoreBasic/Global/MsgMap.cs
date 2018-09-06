using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBasic.Global
{

	public class MsgKeyMap
	{
		public static string MsgKey_Login_Needed = "MsgKey_Login_Needed";
		public static string MsgKey_Profile_TextInfo = "MsgKey_Profile_TextInfo";
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
			MsgCodeMap.Add(MsgKeyMap.MsgKey_Profile_TextInfo, "100");
			MsgCodeMap.Add(MsgKeyMap.MsgKey_Invalidated_OperatorKey, "401");
			MsgContentMap.Add(MsgKeyMap.MsgKey_Invalidated_OperatorKey, "Invalidated Operator Key");
		}

    }
}
