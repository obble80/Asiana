using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.DeepZoomTools;
using System.IO;

namespace Asiana.UI.Services
{
    public class DeepZoomService : IDeepZoomService
    {
        public string GetDeepZoomImage(string sourceImage, string destinationPath)
        {


            string sourceFileName = Path.GetFileNameWithoutExtension(sourceImage);
            string targetFileName = sourceFileName + ".xml";
            string targetPath = Path.Combine(destinationPath, targetFileName);

            if (!File.Exists(targetPath))
            {
                ImageCreator imageCreator = new ImageCreator();
                imageCreator.ConversionTileFormat = ImageFormat.Jpg;
                imageCreator.ServerFormat = ServerFormats.Default;
                imageCreator.TileFormat = ImageFormat.Jpg;
                imageCreator.Create(sourceImage, targetPath);
            }
            

            return targetPath;
        }
    }
}