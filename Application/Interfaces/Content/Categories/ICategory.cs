using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.Content.Categories
{
    public interface ICategory
    {
        Task<List<Category>> GetAll();


        Task<Category> GetById(int id);


        Task<Category> AddCategory(Category model);



        Category UpdateCategory(Category model);



        Category DeleteCategory(Category model);



        Task<bool> CategoryIsExist(string categoryName);

    }
}
