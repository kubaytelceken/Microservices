using AutoMapper;
using KT.Services.Catalog.Dtos.CourseDtos;
using KT.Services.Catalog.Interfaces;
using KT.Services.Catalog.Models;
using KT.Services.Catalog.Settings;
using KT.Shared.Dtos;
using KT.Shared.Enums;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _mapper = mapper;
        }


        public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

        
            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), Convert.ToInt32(StatusCode.Success));
        }

        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return ResponseDto<CourseDto>.Fail("Course Not Found", Convert.ToInt32(StatusCode.NotFound));
            }
            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course), Convert.ToInt32(StatusCode.Success));
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x=>x.UserId == userId).ToListAsync();


            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return ResponseDto<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), Convert.ToInt32(StatusCode.Success));
        }

        public async Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var createdCourse = _mapper.Map<Course>(courseCreateDto);
            createdCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(createdCourse);
            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(createdCourse), Convert.ToInt32(StatusCode.Success));
        }
        public async Task<ResponseDto<NoContent>> UpdataAsync(CourseUpdateDto courseUpdateDto)
        {
            var updatedCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updatedCourse);

            if(result == null)
            {
                return ResponseDto<NoContent>.Fail("Course Not Found", Convert.ToInt32(StatusCode.NotFound));
            }
            return ResponseDto<NoContent>.Success(Convert.ToInt32(StatusCode.Success));
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount>0)
            {
                return ResponseDto<NoContent>.Success(Convert.ToInt32(StatusCode.Success));
                
            }
            return ResponseDto<NoContent>.Fail("Course Not Found", Convert.ToInt32(StatusCode.NotFound));
        }
    }


}
