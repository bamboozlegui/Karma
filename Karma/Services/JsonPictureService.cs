using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public class JsonPictureService
    {
	public void DeletePicture(IWebHostEnvironment webEnv, string photoName)
	{
            if(photoName != null && photoName != "noimage.jpg")
            {
		string filePath = Path.Combine(webEnv.WebRootPath, "images", photoName);
		System.IO.File.Delete(filePath);
            }
        }

        internal string ProcessUploadedFile(IWebHostEnvironment webEnv, IFormFile photo)
	{
	    string uniqueFileName = "noimage.jpg";

	    if (photo != null)
	    {
		string uploadsFolder = Path.Combine(webEnv.WebRootPath, "images");

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
