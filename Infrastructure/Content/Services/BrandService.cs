using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Content.Brands;
using Domain.Models;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BrandService:IBrand
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetAll()
        {
            return await _context.Brands.ToListAsync();
        }

         
        public async Task<Brand> GetById(int id)
        {
            return await _context.Brands.Where(m => m.Brand_Id == id).FirstOrDefaultAsync();
        }


        public async Task<Brand> Add_Brand(Brand model)
        {
           await _context.Brands.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<Brand> Update_Brand(Brand model)
        {
            _context.Brands.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<Brand> Delete_Brand(Brand model)
        {
            _context.Brands.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<bool> BrandIsExist(string brand_Name)
        {
            return await _context.Brands.AnyAsync(b => b.Brand_Name == brand_Name);
        }







    }
}
