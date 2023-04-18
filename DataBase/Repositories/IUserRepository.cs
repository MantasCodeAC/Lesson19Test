using Lesson19Test.DataBase.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.DataBase.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username);
        void SaveUser(User user);
    }
}
