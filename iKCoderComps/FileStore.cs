using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace iKCoderComps
{
    public static class FileStore
    {

		public const string LocalStoreDriver = "c:\\";

		static FileStore()
		{
			if (!Directory.Exists("storepool"))
			{
				Directory.CreateDirectory("storepool");
			}		
		}

		public static string GetUerStoreName(string userid)
		{
			return "u" + "_" + userid;
		}

		public static void VerifyUserStorItem(string userid)
		{
			if (!Directory.Exists(LocalStoreDriver + "storepool\\" + GetUerStoreName(userid)))
				Directory.CreateDirectory(LocalStoreDriver + "\\storepool\\" + GetUerStoreName(userid));
			if (!Directory.Exists(LocalStoreDriver + "storepool\\" + GetUerStoreName(userid) + "\\images\\"))
				Directory.CreateDirectory(LocalStoreDriver + "storepool\\" + GetUerStoreName(userid) + "\\images\\");
			if (!Directory.Exists(LocalStoreDriver + "storepool\\" + GetUerStoreName(userid) + "\\data\\"))
				Directory.CreateDirectory(LocalStoreDriver + "storepool\\" + GetUerStoreName(userid) + "\\data\\");
		}

		public static string GetImageStore(string userid)
		{
			return LocalStoreDriver + "storepool\\" + GetUerStoreName(userid) + "\\images\\";
		}

		public static string GetDataStore(string userid)
		{
			return LocalStoreDriver + "storepool\\" + GetUerStoreName(userid) + "\\data\\";
		}

    }
}
