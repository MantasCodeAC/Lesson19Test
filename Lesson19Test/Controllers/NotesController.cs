using Lesson19Test.DataBase.Model.DTO;
using Lesson19Test.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lesson19Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }
        [HttpPost("CreateNote")]
        public ActionResult<ResponseDto> CreateNote([FromForm] NoteDto noteDto)
        {
            var response = _noteService.AddNote(noteDto.Name, noteDto.Text, noteDto.CategoryName, noteDto.imageUploadRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPost("UploadImage")]
        public ActionResult<ResponseDto> UploadImage([FromForm] ImageUploadRequest imageUploadRequest)
        {
            var response = _noteService.AddImage(imageUploadRequest);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
        [HttpPost("AssignImageToNote")]
        public ActionResult<ResponseDto> AssignImageToNote(string noteName, string imageName)
        {
            var response = _noteService.AssignImageToNote(noteName, imageName);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
    }
}
