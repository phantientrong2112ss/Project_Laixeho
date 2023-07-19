using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KGProject_1.Controllers
{
    [Route("api/[controller]")]
    public class UploadImgController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public UploadImgController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public class FileUIAPI
        {
            public IFormFile files { get; set; }
        }
        [HttpPost]
        //[FromForm]
        public void ULA([FromForm] FileUIAPI obf)
        {
            var saveimg = Path.Combine(_environment.WebRootPath, "Imgs", obf.files.FileName);
            if (obf.files.Length > 0)
            {

                if (!Directory.Exists(_environment.WebRootPath + "\\Imgs\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Imgs\\");
                }
                using (FileStream fileStream = System.IO.File.Create(saveimg))
                {
                    obf.files.CopyTo(fileStream);
                    fileStream.Flush();
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imgs", obf.files.FileName);

                }
            }

        }
    }
}
