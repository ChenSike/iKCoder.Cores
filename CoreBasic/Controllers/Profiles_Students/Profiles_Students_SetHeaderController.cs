using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Net.Http.Headers;
using iKCoderComps;
using iKCoderSDK;
using System.Data;

namespace CoreBasic.Controllers.Profiles_Students
{
    [Produces("application/text")]
    [Route("api/Profiles_Students_SetHeader")]
    public class Profiles_Students_SetHeaderController : ControllerBase_Std
	{
		[ServiceFilter(typeof(Filter.Filter_InitServices))]
		[ServiceFilter(typeof(Filter.Filter_ConnectDB))]
		[ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
		[HttpPost]
		public ContentResult actionResult()
		{
			try
			{
				var files = Request.Form.Files;
				long fileSize = files.Sum(f => f.Length);
				List<string> filePathResultList = new List<string>();
				foreach (var file in files)
				{
					string token = _appLoader.get_ClientToken(Request, "student_token");
					Global.ItemAccountStudents activeItem = Global.LoginServices.Pull(token);
					string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim();
					string filePath = iKCoderComps.FileStore.GetImageStore(activeItem.id);
					iKCoderComps.FileStore.VerifyUserStorItem(activeItem.id);
					fileName = Guid.NewGuid() + "." + fileName.Split('.')[1];
					Dictionary<string, string> paramsMap_for_profle = new Dictionary<string, string>();
					paramsMap_for_profle.Add("@uid", activeItem.name);
					DataTable dtData = _appLoader.ExecuteSelectWithConditionsReturnDT(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle);
					string id = string.Empty;
					Data_dbDataHelper.GetColumnData(dtData.Rows[0], "id", out id);
					paramsMap_for_profle = new Dictionary<string, string>();
					paramsMap_for_profle.Add("@id", id);
					paramsMap_for_profle.Add("@header", fileName);
					if (_appLoader.ExecuteUpdate(Global.GlobalDefines.DB_KEY_IKCODER_BASIC, Global.MapStoreProcedures.ikcoder_basic.spa_operation_profile_students, paramsMap_for_profle))
					{
						string fileFullName = filePath + fileName;
						using (FileStream fs = System.IO.File.Create(fileFullName))
						{
							file.CopyTo(fs);
							fs.Flush();
						}
					}
					else
					{
						return Content(MessageHelper.ExecuteFalse());
					}
				}
				return Content(MessageHelper.ExecuteSucessful());
			}
			catch (Basic_Exceptions err)
			{
				return Content(MessageHelper.ExecuteFalse());
			}
		}
    }
}