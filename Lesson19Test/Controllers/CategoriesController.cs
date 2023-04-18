using Azure;
using Azure.Core;
using Lesson19Test.DataBase.Model.DatabaseModels;
using Lesson19Test.DataBase.Model.DTO;
using Lesson19Test.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Lesson19Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // POST api/<CategoriesController>
        [HttpPost("CreateCategory")]
        public ActionResult<ResponseDto> Post([FromBody] string categoryName)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var response = _categoryService.AddCategory(categoryName, userId);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("ModifyCategoryName")]
        public ActionResult<ResponseDto> Put([Required] string currentName, [Required] string newName)
        {
            var response = _categoryService.UpdateCategory(currentName, newName);
            if (!response.IsSuccess )
                return BadRequest(response.Message);
            return response;
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("DeleteCategory")]
        public ActionResult<ResponseDto> Delete(string categoryName)
        {
            var response = _categoryService.DeleteCategory(categoryName);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
    }
}
