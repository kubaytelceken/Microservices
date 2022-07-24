using KT.Services.Catalog.Dtos.CategoryDtos;
using KT.Services.Catalog.Models;
using KT.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Services.Catalog.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
        Task<ResponseDto<NoContent>> UpdataAsync(CategoryUpdateDto categoryUpdateDto);
        Task<ResponseDto<NoContent>> DeleteAsync(string id);

    }
}
