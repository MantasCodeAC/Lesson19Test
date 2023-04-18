using Lesson19Test.DataBase.Model.DatabaseModels;
using Lesson19Test.DataBase.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.Services.Interfaces
{
    public interface ICategoryService
    {
        ResponseDto AddCategory(string name, Guid userId);
        ResponseDto UpdateCategory(string currentName, string updatedName);
        ResponseDto DeleteCategory(string categoryName);
    }
}
