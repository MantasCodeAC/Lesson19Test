using Lesson19Test.DataBase.Data;
using Lesson19Test.DataBase.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.DataBase.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }
        public void AddImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }

        public Note GetNote(string name)
        {
            return _context.Notes.SingleOrDefault(x => x.Name == name);
        }
        public Image GetImage(string name)
        {
            return _context.Images.SingleOrDefault(x => x.Name == name);
        }
        public Category GetCategory(string categoryName)
        {
            return _context.Categories.SingleOrDefault(x => x.Name == categoryName);
        }

        public void RemoveNote(Note note)
        {
            throw new NotImplementedException();
        }

        public void UpdateNote()
        {
            throw new NotImplementedException();
        }
        public void AssignImageToNote(ImageAndNote imageAndNote)
        {
            _context.ImageAndNote.Add(imageAndNote);
            _context.SaveChanges();
        }
    }
}
