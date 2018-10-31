using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.IO;
using System.Data;

namespace CoreBasic.Controllers.Profiles_Students
{
    [Produces("application/json")]
    [Route("api/Profiles_Students_SetHeaderB64")]
    public class Profiles_Students_SetHeaderB64Controller : ControllerBase_Std
    {
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpGet]
		public ContentResult actionResult()
		{
			try
			{
				string data = _appLoader.get_PostData(HttpContext.Request);
				string token = _appLoader.get_ClientToken(Request, "student_token");
				if(string.IsNullOrEmpty(data))
					return Content(MessageHelper.ExecuteFalse());
				byte[] dataBuffer = Util_Common.Decoder_Base64ToBytes(data);
				Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
				Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@uid", activeItem.name);
				DataTable dt = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
				if (dt != null && dt.Rows.Count > 0)
				{
					string header = string.Empty;
					Data_dbDataHelper.GetColumnData(dt.Rows[0], "header", out header);
					if (string.IsNullOrEmpty(header))
					{
						return Content(MessageHelper.ExecuteFalse());
					}
					string filePath = iKCoderComps.FileStore.GetImageStore(_appLoader.GetAPICurrentPath(), activeItem.id);
					try
					{
						FileStream fileStream = new FileStream(filePath + header, FileMode.Create);
						BinaryWriter binaryWriter = new BinaryWriter(fileStream);
						binaryWriter.Write(dataBuffer);
						binaryWriter.Flush();
						binaryWriter.Close();
						fileStream.Close();
						return Content(MessageHelper.ExecuteSucessful());
					}
					catch
					{
						return Content(MessageHelper.ExecuteFalse());
					}
				}
				else
				{
					return Content(MessageHelper.ExecuteFalse());
				}
			}
			catch (Basic_Exceptions err)
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
	}
}