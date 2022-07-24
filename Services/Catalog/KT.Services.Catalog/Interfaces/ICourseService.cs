using KT.Services.Catalog.Dtos.CourseDtos;
using KT.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Services.Catalog.Interfaces
{
    public interface ICourseService
    {
        Task<ResponseDto<List<CourseDto>>> GetAllAsync();
        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);
        Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<ResponseDto<NoContent>> UpdataAsync(CourseUpdateDto courseUpdateDto);
        Task<ResponseDto<NoContent>> DeleteAsync(string id);
    }
}
