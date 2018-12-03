using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iKCoderSDK;
using System.Drawing;
using System.DrawingCore;

namespace AppMain.Controllers.Demo
{
    [Route("api/Demo_Get_QRCode")]
    [ApiController]
    public class Demo_Get_QRCodeController : ControllerBase
    {
        [HttpGet]
        public IActionResult actionResult(string symbol)
        {
            string url = "http://www.ikcoder.com/ikcoder/demo_raw.html?symbol=" + symbol;
            byte[] bufferData = Util_QRCoder.GernerateQRCodeForBytes(url);
            return new FileContentResult(bufferData, "image/jpeg");
        }
    }
}