using Application.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Content.Products
{
    public interface IProduct
    {
        Task<List<ProductDtoDetails>> GetAll();

        Task<Product> GetById(int id);
        Task<ProductDtoDetails> GetByCode(string code);
        Product GetProductByCode(string code);

        Task<Product> Add(Product model);

        Product Update(Product model);

        Product Delete(Product model);

        Task<bool> ProductIsExist(string code);
        void SaveChanges();
        void WithDraw(WithDrawProducts dto);
    }
}
