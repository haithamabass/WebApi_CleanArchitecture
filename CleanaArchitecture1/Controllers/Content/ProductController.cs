using Application.Dtos;
using Application.Interfaces.Content.Products;
using Domain;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;

namespace CleanaArchitecture1.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productServcie;

        public ProductController(IProduct productServcie)
        {
            _productServcie = productServcie;

        }

        #region GetAllProducts Endpoint
        // GET: api/Products
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _productServcie.GetAll());
        }

        // GET: api/Products/5
        [HttpGet("{code}")]
        public async Task<ActionResult<Product>> GetProduct(string code)
        {
            if (await _productServcie.GetAll() == null)
            {
                return NotFound("The entity is Empty");
            }
            var product = await _productServcie.GetByCode(code);

            if (product == null)
            {
                return NotFound($"no Product with {code} was found");
            }

            return Ok(product);
        }
        #endregion

        #region Create Product Endpoint
        // POST: api/Products
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProduct([FromQuery] ProductDto model)
        {

            var isExist = await _productServcie.ProductIsExist(model.Product_Code);

            if (isExist is true)
            {
                var product = _productServcie.GetProductByCode(model.Product_Code);
                product.Product_Quantity += model.Product_Quantity;
                _productServcie.SaveChanges();

            }
            else
            {
                var product = new Product
                {
                    Product_Name = model.Product_Name,
                    Brand_Id = model.Brand_Id,
                    Categoty_Id = model.Category_Id,
                    Product_Price = model.Product_Price,
                    Product_Quantity = model.Product_Quantity,
                    Product_Code = model.Product_Code
                };

                await _productServcie.Add(product);
            }
            _productServcie.SaveChanges();
            return Ok();
        }
        #endregion

        #region Update Product Endpoint
        // PUT: api/Products/5
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id, ProductDto model)
        {
            var product = await _productServcie.GetById(id);

            if (product == null)
            {
                return NotFound($"brand:{product.Product_Name} with Id:{product.Product_Id} has not found");
            }

            product.Product_Name = model.Product_Name;
            product.Brand_Id = model.Brand_Id;
            product.Brand_Id = model.Brand_Id;
            product.Product_Price = model.Product_Price;
            product.Product_Quantity = model.Product_Quantity;

            _productServcie.Update(product);

            return Ok(product);
        }

        #endregion

        #region Delete Product Endpoint
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productServcie.GetById(id);
            if (product is null)
            {
                return NotFound($"brand:{product.Product_Name} with Id:{product.Product_Id} has not found");
            }

            _productServcie.Delete(product);

            return NoContent();
        }
        #endregion

        #region Withdraw Product Endpoint
        [HttpPut("WithDraw")]
        public async Task<IActionResult> WithDrawProduct(WithDrawProducts dto)
        {
            var exist = _productServcie.GetProductByCode(dto.Product_Code);

            if (exist is null)
            {
                return BadRequest($"No Products found with this Code {dto.Product_Code}");

            }

            if (dto.Product_Quantity > exist.Product_Quantity)
                return BadRequest($"Your request is bigger Than the stock , the sock has {exist.Product_Quantity} of {exist.Product_Name}");

            _productServcie.WithDraw(dto);

            return Ok(exist);
        }

        #endregion
    }
}