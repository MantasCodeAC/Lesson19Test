using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.DataBase.Model.DatabaseModels
{
    public class ImageAndNote
    {
        public Guid ImageId { get; set; }
        public Guid NoteId { get; set; }
        public Note Note { get; set; }
        public Image Image { get; set; }
    }
}
