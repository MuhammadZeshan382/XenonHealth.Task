using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using XenonHealth.Task.Models;
using AppContext = System.AppContext;

namespace XenonHealth.Task.Controllers
{
    public class ImageController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ImageController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("{controller}/{id}")]
        public IActionResult ViewImage([FromRoute] string id)
        {
            if (Guid.TryParse(id, out Guid guidOutput))
            {
                var image = _dbContext.Images.FirstOrDefault(x => x.Id == id);

                return image == null ? NotFound("Image doesn't exist") : View("ViewImage", image);
            }
            return BadRequest("The Guid is Invalid");
        }
        [Route("{controller}/List")]
        public IActionResult List()
        {
            var imagelist = _dbContext.Images.ToList();
            return View(imagelist);
        }
        [HttpPost]
        [RequestSizeLimit(10000000)]
        [Route("{controller}/Create")]
        public async Task<IActionResult> Create(ImageInputDTO dtomodel)
        {
            if (ModelState.IsValid)
            {
                ImageModel domainmodel = new ImageModel();

                if (dtomodel.ImageFile is { Length: > 0 })
                {
                    using var memoryStream = new MemoryStream();
                    await dtomodel.ImageFile.CopyToAsync(memoryStream);
                    domainmodel.ImageData = memoryStream.ToArray();
                }
                domainmodel.ImageName = dtomodel.ImageName;
                domainmodel.Id = Guid.NewGuid().ToString();
                domainmodel.ImageExtension = Path.GetExtension(dtomodel.ImageFile.FileName);
                domainmodel.CreateDate = DateTime.Now; ;
                _dbContext.Images.Add(domainmodel);
                await _dbContext.SaveChangesAsync();
                TempData["issaved"] = true;
                TempData["ID"] = domainmodel.Id;
                return RedirectToAction("Index", "Image");
            }
            TempData["issaved"] = false;
            string messages = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

            TempData["message"] = messages;
            return View("Index");
        }
    }
}
