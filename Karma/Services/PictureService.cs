using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public class PictureService
    {
        public void DeletePicture(string rootPath, string photoName)
        {
            if (photoName != null && photoName != "noimage.jpg")
            {
                string filePath = Path.Combine(rootPath, "images", photoName);
                System.IO.File.Delete(filePath);
            }
        }

        internal string ProcessUploadedFile(string rootPath, IFormFile photo)
        {
            string uniqueFileName = "noimage.jpg";

            if (photo != null)
            {
                string uploadsFolder = Path.Combine(rootPath, "images");

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
