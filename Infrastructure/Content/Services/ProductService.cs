using Application.Dtos;
using Application.Interfaces.Content.Products;
using Domain.Models;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Content.Services
{
    public class ProductService : IProduct
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        #region GetAll
        public async Task<List<ProductDtoDetails>> GetAll()
        {
            var product = await _context.Products
                 .Include(r => r.Brand)
                 .Include(r => r.Category)
                 .Select(x => new ProductDtoDetails
                 {
                     Product_Id = x.Product_Id,
                     Product_Name = x.Product_Name,
                     Brand_Name = x.Brand.Brand_Name,
                     Brand_Id = x.Brand_Id,
                     Category_Name = x.Category.Category_Name,
                     Category_Id = x.Categoty_Id,
                     Product_Price = x.Product_Price,
                     Product_Quantity = x.Product_Quantity,
                     Product_Code = x.Product_Code
                 })
                 .AsNoTracking()
                 .ToListAsync();

            return product;
        }
        #endregion

        # region GetByCode
        //return Dto : (ProductDtoDetails)
        public async Task<ProductDtoDetails> GetByCode(string code)
        {
            var product = await _context.Products
                .Where(c => c.Product_Code == code)
                 .Include(r => r.Brand)
                 .Select(x => new ProductDtoDetails
                 {
                     Product_Id = x.Product_Id,
                     Product_Name = x.Product_Name,
                     Brand_Name = x.Brand.Brand_Name,
                     Brand_Id = x.Brand_Id,
                     Category_Name = x.Category.Category_Name,
                     Category_Id = x.Categoty_Id,
                     Product_Price = x.Product_Price,
                     Product_Quantity = x.Product_Quantity,
                     Product_Code = x.Product_Code
                 })
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            return product;
        }
        #endregion

        #region GetProductByCode
        // return entity/model : (Product)
        public Product GetProductByCode(string code)
        {
            var product = _context.Products.FirstOrDefault(p => p.Product_Code == code);

            return product;
        }
        #endregion

        #region GetById
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        #endregion

        #region Add 
        public async Task<Product> Add(Product model)
        {
            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Update
        public Product Update(Product model)
        {
            _context.Update(model);
            _context.SaveChangesAsync();
            return model;
        }
        #endregion

        #region Delete
        public Product Delete(Product model)
        {
            _context.Products.Remove(model);
            _context.SaveChangesAsync();

            return model;
        }
        #endregion

        #region ProductIsExist
        public async Task<bool> ProductIsExist(string code)
        {
            return await _context.Products.AnyAsync(p => p.Product_Code == code);
        }
        #endregion

        #region WithDraw
        public void WithDraw(WithDrawProducts dto)
        {
            var product = GetProductByCode(dto.Product_Code);

            product.Product_Quantity -= dto.Product_Quantity;

            SaveChanges();
        }
        #endregion

        #region SaveChanges
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        #endregion

    }
}