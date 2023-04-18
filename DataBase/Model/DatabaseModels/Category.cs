using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.DataBase.Model.DatabaseModels
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Note> Notes { get; set; }
        public Guid UserId { get; set; }
    }
}
