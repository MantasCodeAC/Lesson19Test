using Lesson19Test.DataBase.Data;
using Lesson19Test.DataBase.Model.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson19Test.DataBase.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public Category GetCategory(string name)
        {
            return _context.Categories.SingleOrDefault(x => x.Name == name);
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void UpdateCategory()
        {
            _context.SaveChanges();
        }
        public void RemoveCategory(Category category)
        {      
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
