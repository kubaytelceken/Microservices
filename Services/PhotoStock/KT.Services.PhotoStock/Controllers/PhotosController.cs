using KT.Services.PhotoStock.Dtos;
using KT.Shared.ControllerBases;
using KT.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo,CancellationToken cancellationToken)
        {
            if(photo != null && photo.Length>0)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(imagePath, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);


                var returnPath = "photos/" + photo.FileName;
                PhotoDto photoDto = new PhotoDto()
                {
                    ImageURL = returnPath
                };

                return CreateActionResultInstance(ResponseDto<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(ResponseDto<PhotoDto>.Fail("Photo is empty",400));
        }



        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(imagePath))
            {
                return CreateActionResultInstance(ResponseDto<NoContent>.Fail("Photo not found", 404));
            }
            System.IO.File.Delete(imagePath);
            return CreateActionResultInstance(ResponseDto<NoContent>.Success(204));
        }
    }
}
