using Application.Interfaces.Content.Categories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanaArchitecture1.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryService;

        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        #region Get All Categories Endpoint
        // GET: CategoriesController
        [HttpGet("GET_ALLCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categries = await _categoryService.GetAll();
            if (categries is null)
                return NotFound("No data here!");
            return Ok(categries);
        }
        #endregion

        #region Get Category Endpoint
        // GET: CategoriesController/Details/5
        [HttpGet("GET_Category")]
        public async Task<ActionResult> Details(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound($"No Category has found with this {id} ");

            return Ok(category);
        }
        #endregion

        #region Create Category Endpoint
        // POST: CategoriesController/Create
        [HttpPost("Add_NewCategory")]
        public async Task<IActionResult> CreateCategory([FromQuery] Category model)
        {
            if (ModelState.IsValid is false)
            {
                return BadRequest("Invalid Inputs");
            }

            if (await _categoryService.CategoryIsExist(model.Category_Name))
                return BadRequest(" this Category name is already registred");

            var categy = new Category
            {
                Category_Name = model.Category_Name,
            };

            _categoryService.AddCategory(categy);

            return Ok(await _categoryService.GetAll());
        }
        #endregion

        #region Update Category
        // POST: CategoriesController/Edit/5
        [HttpPut("Edit_Category")]
        public async Task<IActionResult> UpdateCategory(int id, Category model)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound($"No Category was found with ID {id}");

            if (await _categoryService.CategoryIsExist(model.Category_Name))
                return BadRequest(" this Category name is already registred");

            category.Category_Name = model.Category_Name;

            _categoryService.UpdateCategory(category);

            return Ok(category);
        }
        #endregion

        #region Delete Category
        // POST: CategoriesController/Delete/5
        [HttpDelete("Delete_Category")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound($"No Ctegory was found with ID {id}");

            _categoryService.DeleteCategory(category);

            return Ok($"Category : {category.Category_Name} with Id : ({category.Categoty_Id}) is deleted");
        }
        #endregion
    }
}