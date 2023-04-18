using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.DataBase.Model.DatabaseModels
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public List<ImageAndNote> ImageAndNote { get; set; }
        public Guid CategoryId { get; set; }
    }
}
