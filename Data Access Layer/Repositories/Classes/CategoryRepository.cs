using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Classes
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly GymSystemDBContext _context;
        public CategoryRepository(GymSystemDBContext dBContext)
        {
            _context = dBContext;
        }
        public int AddCategory(Category category)
        {
            _context.Categories.Add(category);
            return _context.SaveChanges();

        }

        public int DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                return _context.SaveChanges();
            }
            return 0;

        }

        public IEnumerable<Category> GetAllMembers()
        {
            return _context.Categories.ToList();

        }

        public Category? GetCategory(int id)
        {
            return _context.Categories.Find(id);


        }

        public int UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            return _context.SaveChanges();

        }
    }
}
