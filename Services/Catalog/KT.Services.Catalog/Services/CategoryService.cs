using AutoMapper;
using KT.Services.Catalog.Dtos.CategoryDtos;
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
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return ResponseDto<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), Convert.ToInt32(StatusCode.Success));
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var createdCategory = _mapper.Map<Category>(categoryCreateDto);
            await _categoryCollection.InsertOneAsync(createdCategory);
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(createdCategory), Convert.ToInt32(StatusCode.Success));
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if(category == null)
            {
                return ResponseDto<CategoryDto>.Fail("Category Not Found", Convert.ToInt32(StatusCode.NotFound));
            }
            return ResponseDto<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), Convert.ToInt32(StatusCode.Success));
        }

        public async Task<ResponseDto<NoContent>> UpdataAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var updatedCategory = _mapper.Map<Category>(categoryUpdateDto);
            var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.Id == categoryUpdateDto.Id, updatedCategory);

            if (result == null)
            {
                return ResponseDto<NoContent>.Fail("Category Not Found", Convert.ToInt32(StatusCode.NotFound));
            }
            return ResponseDto<NoContent>.Success(Convert.ToInt32(StatusCode.Success));
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {

            var result = await _categoryCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return ResponseDto<NoContent>.Success(Convert.ToInt32(StatusCode.Success));

            }
            return ResponseDto<NoContent>.Fail("Category Not Found", Convert.ToInt32(StatusCode.NotFound));
        }
    }
}
