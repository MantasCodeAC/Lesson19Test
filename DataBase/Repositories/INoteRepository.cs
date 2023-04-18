using Lesson19Test.DataBase.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.DataBase.Repositories
{
    public interface INoteRepository
    {
        Note GetNote(string name);
        Image GetImage(string name);
        Category GetCategory(string categoryName);
        void AddNote(Note note);
        void RemoveNote(Note note);
        public void UpdateNote();
        void AddImage(Image image);
        void AssignImageToNote(ImageAndNote imageAndNote);
    }
}
