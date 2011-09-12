using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Asiana.UI.Results;
using Asiana.UI.Extensions;
using System.IO;
using System.Drawing.Imaging;
using Asiana.UI.Services;
namespace Asiana.UI.Controllers
{
    public class ImageController : Controller
    {
        private IDeepZoomService deepZoomService;

        public ImageController(IDeepZoomService deepZoomService)
        {
            this.deepZoomService = deepZoomService;
        }
        //
        // GET: /Image/

        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Any, VaryByParam = "*", Duration = 86400)]
        public FileContentResult Size(string name, int width, int height)
        {
            string imagePath = Url.Content("~/Content/Products/Source/" + name);
            string filePath = Server.MapPath(imagePath);

            string fileDirectory = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = Path.GetExtension(filePath);

            string sizedFilePath = String.Format(@"{0}\Sized\{1}.{2}.{3}{4}", fileDirectory, fileName, width, height, fileExtension);

            string defaultImagePath = Url.Content("~/Content/Products/Source/Default.jpg");

            if (System.IO.File.Exists(filePath))
            {
                if (System.IO.File.Exists(sizedFilePath))
                {
                    return new FileContentResult(
                    System.IO.File.ReadAllBytes(
                    sizedFilePath),
                    "image/jpeg"
                    );

                }
                else
                {
                    using (Image image = Image.FromFile(filePath))
                    {
                        image.Resize(width, height).SaveJpeg(sizedFilePath);
                        return new FileContentResult(
                    System.IO.File.ReadAllBytes(
                    sizedFilePath),
                    "image/jpeg"
                    );
                    }
                }
            }
            else
            {
                return new FileContentResult(
                    System.IO.File.ReadAllBytes(
                    Server.MapPath(defaultImagePath)),
                    "image/jpeg"
                    );
            }
        }

       
        public FileContentResult Zoom(string name, string level, string image)
        {
            if (!name.Contains("_files"))
            {
                string sourceImage = Url.Content("~/Content/Products/Source/" + name);
                string sourceImagePath = Server.MapPath(sourceImage);
                string destination = Url.Content("~/Content/Products/DeepZoom/");
                string destinationPath = Server.MapPath(destination);

                string sourceExtension = Path.GetExtension(name);
                string sourceFile = Path.GetFileNameWithoutExtension(name);

                var deepZoomImagePath = deepZoomService.GetDeepZoomImage(sourceImagePath, destinationPath);
                var deepZoomPath = Server.MapPath(Path.Combine(destination, sourceFile + ".xml"));

                return new FileContentResult(
                        System.IO.File.ReadAllBytes(
                        deepZoomPath),
                        "text/xml"
                        );
            }
            else
            {
                string destination = Server.MapPath(Url.Content("~/Content/Products/DeepZoom/" + name + "/" + level + "/" + image));
                return new FileContentResult(
                       System.IO.File.ReadAllBytes(
                       destination),
                       "image/jpeg"
                       );
            }
        }
    }
}
