using Lesson19Test.DataBase.Model.DatabaseModels;
using Lesson19Test.DataBase.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.Services.Interfaces
{
    public interface INoteService
    {
        ResponseDto AddNote(string name, string text, string categoryName);
        ResponseDto UpdateNote(string currentName, string updatedName);
        ResponseDto DeleteNote(string noteName);
        ResponseDto AddImage(ImageUploadRequest imageUploadRequest);
        ResponseDto AssignImageToNote(string noteName, string imageName);
    }
}


