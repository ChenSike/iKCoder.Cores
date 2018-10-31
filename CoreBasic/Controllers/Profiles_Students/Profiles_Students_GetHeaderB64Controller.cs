using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderComps;
using iKCoderSDK;
using System.Data;
using System.IO;
using System.Text;

namespace CoreBasic.Controllers.Profiles_Students
{
    [Produces("application/json")]
    [Route("api/Profile_Students_GetHeaderB64")]
    public class Profiles_Students_GetHeaderB64Controller : ControllerBase_Std
    {
        [ServiceFilter(typeof(Filter.Filter_InitServices))]
        [ServiceFilter(typeof(Filter.Filter_ConnectDB))]
        [ServiceFilter(typeof(Filter.Filter_UserAuthrization))]
        [HttpGet]
        public ContentResult actionResult()
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
                    if(string.IsNullOrEmpty(header))
                    {
                        return Content("");
                    }
                    string filePath = iKCoderComps.FileStore.GetImageStore(_appLoader.GetAPICurrentPath(), activeItem.id);
					try
					{
						FileStream fileStream = new FileStream(filePath + header, FileMode.Open);
						BinaryReader binaryReader = new BinaryReader(fileStream);
						byte[] dataBuffer = binaryReader.ReadBytes((int)fileStream.Length);
						string strDataBuffer = Encoding.Default.GetString(dataBuffer);
						string strB64 = Util_Common.Encoder_Base64(strDataBuffer);
						binaryReader.Close();
						fileStream.Close();
						string[] filenameAttrs = header.Split(".");
						string entendType = filenameAttrs[filenameAttrs.Length - 1];
						string result = "data:image/";
						/*if (entendType == "icon")
							result = result + "x-icon;base64,";
						else
							result = entendType + ";base64,";*/
						return Content(result + strB64);
					}
					catch
					{
						return Content("");
					}
                }
                else
                {
                    return Content("");
                }
            }
            catch (Basic_Exceptions err)
            {
                return Content("");
            }
        }
    }
}