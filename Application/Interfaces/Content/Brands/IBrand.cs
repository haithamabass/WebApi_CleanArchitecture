using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.Content.Brands
{
    public interface IBrand
    {
        Task<List<Brand>> GetAll();

        Task<Brand> GetById(int id);

        Task<Brand> Add_Brand(Brand model);

        Task<Brand> Update_Brand(Brand model);

        Task<Brand> Delete_Brand(Brand model);

        Task<bool> BrandIsExist(string code);

    }
}
