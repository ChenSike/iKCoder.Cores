using System;
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

namespace iKCoderComps
{
    [EnableCors("AllowSameDomain")]
    public class ControllerBase_Std:Controller
    {

		public AppLoader _appLoader = new AppLoader();

	}
}
