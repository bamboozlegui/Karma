using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public class JsonPictureService
    {
	internal string ProcessUploadedFile(IWebHostEnvironment webEnv, IFormFile photo)
	{
	    string uniqueFileName = null;

	    if (photo != null)
	    {
		string uploadsFolder =
		Path.Combine(webEnv.WebRootPath, "images");
		uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
		string filePath = Path.Combine(uploadsFolder, uniqueFileName);
		using (var fileStream = new FileStream(filePath, FileMode.Create))
		{
		    photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
