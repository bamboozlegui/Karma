using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace Karma.Services
{
    public class JsonFileRequestService<T> where T : PostModel, IHasJsonFile
    {
        public JsonFileRequestService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName(System.Type type)
        {
            if(type == typeof(RequestModel))
                return Path.Combine(WebHostEnvironment.ContentRootPath, "data", (new RequestModel()).GetJsonName()); 
            if(type == typeof(SubmitModel))
                return Path.Combine(WebHostEnvironment.ContentRootPath, "data", (new SubmitModel()).GetJsonName()); 
            return "";  
        }

        public IEnumerable<T> GetPosts()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName(typeof(T))))
            {
                return JsonSerializer.Deserialize<T[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void RefreshPosts(IEnumerable<T> requests)
        {
            File.WriteAllTextAsync(
                JsonFileName(typeof(T)), 
                JsonSerializer.Serialize<IEnumerable<T>>(requests, 
                new JsonSerializerOptions {WriteIndented = true}));
        }
    }
}