using Lesson19Test.DataBase.Model.DatabaseModels;
using Lesson19Test.DataBase.Model.DTO;
using Lesson19Test.DataBase.Repositories;
using Lesson19Test.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = Lesson19Test.DataBase.Model.DatabaseModels.Image;

namespace Lesson19Test.Services.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NoteService(INoteRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public ResponseDto AddNote(string name, string text, string categoryName)
        {
            var existingNote = _repository.GetNote(name);
            if (existingNote is not null)
                return new ResponseDto(false, "Note with this name already exists");
            Note note = CreateNote(name, text, categoryName);
            if (note is null)
                return new ResponseDto(false, $"The category name doesn't exist");
            _repository.AddNote(note);
            return new ResponseDto(true, $"Note {name} created successfully");
        }

        public ResponseDto AddImage(ImageUploadRequest imageUploadRequest)
        {
            Image image = CreateImage(imageUploadRequest);
            _repository.AddImage(image);
            return new ResponseDto(true, $"Note {image.Name} image added to database successfully");
        }
        public ResponseDto AssignImageToNote(string noteName, string imageName)
        {
            ImageAndNote imageAndNote = CreateImageAndNote(noteName, imageName);
            _repository.AssignImageToNote(imageAndNote);
            return new ResponseDto(true, $"{imageName} image assigned to {noteName} note successfully");
        }
        public ResponseDto DeleteNote(string noteName)
        {
            throw new NotImplementedException();
        }

        public ResponseDto UpdateNote(string currentName, string updatedName)
        {
            throw new NotImplementedException();
        }
        
        private Note CreateNote(string name, string text, string categoryName)
        {
            var tryCategory = _repository.GetCategory(categoryName);
            if(tryCategory is null)        
                return null;        
            var note = new Note
            {
                Id = Guid.NewGuid(),
                Name = name,
                Text = text,
                CategoryId = tryCategory.Id,

            };
            return note;
        }
        private Image CreateImage(ImageUploadRequest imageUploadRequest)
        {
            using var memoryStream = new MemoryStream();
            imageUploadRequest.Image.CopyTo(memoryStream);
            Image image = new()
            {
                Id = Guid.NewGuid(),
                Name = imageUploadRequest.Image.FileName,
                Data = memoryStream.ToArray(),
            };
            return image;
        }
        private ImageAndNote CreateImageAndNote(string noteName, string imageName)
        {
            ImageAndNote imageAndNote = new ImageAndNote();
            imageAndNote.Image = _repository.GetImage(imageName);
            imageAndNote.Note = _repository.GetNote(noteName);
            return imageAndNote;
        }
    }
}
