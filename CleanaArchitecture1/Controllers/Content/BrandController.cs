using Application.Interfaces.Content;
using Application.Interfaces.Content.Brands;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanaArchitecture1.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrand _brandService;

        public BrandController(IBrand brandService)
        {
            _brandService = brandService;
        }

        #region Get All Brands Endpoint

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var brands = await _brandService.GetAll();

            if (brands is null)
                return BadRequest("No Data here !");

            return Ok(brands);
        }

        #endregion Get All Brands Endpoint

        #region Get Brand Endpoint

        // GET: api/Brands/5
        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _brandService.GetById(id);

            if (brand == null)
            {
                return NotFound($"No brand has found with this {id} ");
            }

            return brand;
        }

        #endregion Get Brand Endpoint

        #region Create Brand Endpoint

        // POST: api/Brands
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand([FromQuery] Brand brand)
        {
            if (await _brandService.BrandIsExist(brand.Brand_Name))
            {
                return BadRequest($"Brand name: {brand.Brand_Name} is already registered");
            }

            var result = await _brandService.Add_Brand(brand);
            return Ok(await _brandService.GetAll());
        }

        #endregion Create Brand Endpoint

        #region Update Category
        // PUT: api/Brands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, Brand model)
        {
            var brand = await _brandService.GetById(id);

            if (brand == null)
                return NotFound($"brand: {model.Brand_Name} with{brand.Brand_Id} has not found");

            if (await _brandService.BrandIsExist(model.Brand_Name))
                return BadRequest(" this Brand name is already registred");


            brand.Brand_Name = model.Brand_Name;
            await _brandService.Update_Brand(brand);

            return Ok(brand);
        }
        #endregion

        #region Delete Category
        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.GetById(id);
            if (brand == null)
            {
                return NotFound($"brand:{brand.Brand_Name} with Id:{brand.Brand_Id} has not found");
            }

            await _brandService.Delete_Brand(brand);

            return Ok($"Brand : {brand.Brand_Name} with Id : ({brand.Brand_Id}) is deleted");
        }
        #endregion
    }
}