using System;
using System.Collections.Generic;
using System.Text;
using QRCoder;
using System.Drawing;
using System.DrawingCore;
using System.IO;

namespace iKCoderSDK
{
    public class Util_QRCoder
    {
        public static Bitmap GernerateQRCode(string QRContent)
        {
            QRCodeGenerator objGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = objGenerator.CreateQrCode(QRContent, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            return qRCode.GetGraphic(20);
        }

        public static byte[] GernerateQRCodeForBytes(string QRContent)
        {
            QRCodeGenerator objGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = objGenerator.CreateQrCode(QRContent, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap returnObj = qRCode.GetGraphic(20);
            using (MemoryStream stream = new MemoryStream())
            {
                returnObj.Save(stream, System.DrawingCore.Imaging.ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

    }
}
