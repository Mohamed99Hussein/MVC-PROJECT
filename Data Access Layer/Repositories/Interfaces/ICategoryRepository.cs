using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    internal interface ICategoryRepository
    {
        // Get all Categories
        IEnumerable<Category> GetAllMembers();

        //  Get Category by id
        Category? GetCategory(int id);

        //  Add Category
        int AddCategory(Category category);

        //  Update Category
        int UpdateCategory(Category category);

        //  Delete Category
        int DeleteCategory(int id);
    }
}
