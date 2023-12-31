﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [AllowAnonymous]
        [HttpGet("{hotelId}")]
        [ResponseCache(CacheProfileName = "CacheProfile60sec")]
        public async Task<IActionResult> GetAllImagesByHotelId(Guid hotelId)
        {
            var images = await _imageRepository.GetAllImagesByHotelId(hotelId);
            return Ok(images);
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpPost]
        public async Task<IActionResult> InsertImage([FromBody] Image image)
        {
            if (await _imageRepository.InsertImage(image))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Hotel Manager")]
        [HttpPut("updateOrder")]
        public async Task<IActionResult> UpdateImageOrder([FromBody] IEnumerable<Image> images)
        {
            if (await _imageRepository.UpdateImagePositions(images))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Hotel Manager, Admin")]
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImageById(Guid imageId)
        {
            if (await _imageRepository.DeleteImageById(imageId))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
