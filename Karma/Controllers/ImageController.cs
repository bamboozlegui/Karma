using System;
using System.IO;
using System.Threading.Tasks;
using Karma.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ImageUploadDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public PictureService PictureService { get; }

        public ImageController(IWebHostEnvironment webHostEnvironment, PictureService pictureService)
        {
            WebHostEnvironment = webHostEnvironment;
            PictureService = pictureService;
        }
        [HttpPost]
        public ActionResult<string> Post()
        {
            var picture = HttpContext.Request.Form.Files[0];
            if (picture != null)
            {
                try
                {
                    var path = PictureService.ProcessUploadedFile(WebHostEnvironment.WebRootPath, picture);
                    return Ok(path);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading data to the server");
                }
            }

            return NotFound(null);

        }

        [HttpPut("{existingFileName}")]
        public ActionResult<string> Put(string existingFileName)
        {
            var picture = HttpContext.Request.Form.Files[0];
            if (picture != null)
            {
                try
                {
                    if (existingFileName != null)
                    {
                        PictureService.DeletePicture(WebHostEnvironment.WebRootPath, existingFileName);
                    }

                    var path = PictureService.ProcessUploadedFile(WebHostEnvironment.WebRootPath, picture);
                    return Ok(path);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading data to the server");
                }
            }

            return NotFound(null);
        }

        [HttpDelete("{fileName}")]
        public ActionResult<bool> Delete(string fileName)
        {
            if (PictureService.DeletePicture(WebHostEnvironment.WebRootPath, fileName))
            {
                return Ok(true);
            }

            return NotFound(false);

        }
    }
}