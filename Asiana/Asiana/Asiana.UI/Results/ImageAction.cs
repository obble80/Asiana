using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.Mvc;
using System.Drawing.Imaging;

namespace Asiana.UI.Results
{
    public class ImageResult : ActionResult
    {
        Image image;

        public ImageResult(Image image)
        {
            this.image = image;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "image/jpeg";
            image.Save(response.OutputStream, ImageFormat.Jpeg);
            image.Dispose();
        }
    }
}