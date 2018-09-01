using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Profiles_Students
{
	[Produces("application/text")]
	[Route("api/Profiles_Students_GetHeader")]
	public class Profiles_Students_GetHeaderController : ControllerBase_Std
	{
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpGet]
		public ContentResult actionResult(string withpath = "1")
		{
			try
			{
				string token = _appLoader.get_ClientToken(Request, "student_token");
				Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
				Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
				paramsMap_for_profle.Add("@uid", activeItem.name);
				DataTable dt = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
				if (dt != null && dt.Rows.Count > 0)
				{
					string header = string.Empty;
					Data_dbDataHelper.GetColumnData(dt.Rows[0], "header", out header);
					if (withpath == "1")
					{
						string filePath = iKCoderComps.FileStore.GetImageStore(activeItem.id);
						return Content(MessageHelper.ExecuteSucessful("800", filePath + "\\" + header));
					}
					else
					{
						return Content(MessageHelper.ExecuteSucessful("800", header));
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