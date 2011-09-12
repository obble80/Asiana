using System;
namespace Asiana.UI.Services
{
    public interface IDeepZoomService
    {
        string GetDeepZoomImage(string sourceImage, string destinationPath);
    }
}
