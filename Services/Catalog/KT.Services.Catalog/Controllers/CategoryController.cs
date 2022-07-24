using KT.Services.Catalog.Dtos.CategoryDtos;
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
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var response = await _categoryService.CreateAsync(categoryCreateDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var response = await _categoryService.UpdataAsync(categoryUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
