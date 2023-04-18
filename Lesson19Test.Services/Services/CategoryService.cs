using Lesson19Test.DataBase.Model.DatabaseModels;
using Lesson19Test.DataBase.Model.DTO;
using Lesson19Test.DataBase.Repositories;
using Lesson19Test.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.Services.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryService(ICategoryRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseDto AddCategory(string newCategoryName, Guid userId)
        {
            var existingCategory = _repository.GetCategory(newCategoryName);
            if (existingCategory is not null)
                return new ResponseDto(false, "Category with this name already exist");          
            var category = CreateCategory(newCategoryName, userId);
            _repository.AddCategory(category);
            return new ResponseDto(true, "");
        }

        public ResponseDto DeleteCategory(string categoryName)
        {
            var existingCategory = _repository.GetCategory(categoryName);
            var currenUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            if (existingCategory is null)
                return new ResponseDto(false, "There is no category with this name");
            else if (currenUserId != existingCategory.UserId)
                return new ResponseDto(false, "There is no access to this category");
            _repository.RemoveCategory(existingCategory);
            return new ResponseDto(true, $"Category {existingCategory.Name} removed");
        }

        public ResponseDto UpdateCategory(string currentName, string updatedName)
        {
            var existingCategory = _repository.GetCategory(currentName);
            var currenUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Identity.GetUserId());
            if (existingCategory is null)
                return new ResponseDto(false, "There is no category with this name");
            else if(currenUserId != existingCategory.UserId)
                return new ResponseDto(false, "There is no access to this category");
            existingCategory.Name = updatedName;
            _repository.UpdateCategory();
            return new ResponseDto(true, $"Categories name changed from {currentName} to {updatedName}");
        }

        private Category CreateCategory(string name, Guid userId)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                Notes = new List<Note>(),
                UserId = userId
            };
          
            return category;
        }

    }
}
