using KT.Services.Catalog.Dtos.CourseDtos;
using KT.Services.Catalog.Interfaces;
using KT.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserIdAsync(string userId)
        {
            var response = await _courseService.GetAllByUserIdAsync(userId);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateAsync(courseCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdataAsync(courseUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
