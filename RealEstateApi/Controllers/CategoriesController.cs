using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;
using RealEstateApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();

        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Categories);
        }

        [HttpGet("[Action]")]
        public IActionResult SortCategories()
        {
            return Ok(_dbContext.Categories.OrderByDescending(x => x.Name));
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category categoryObj)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("Record Not Found Against This Id: "+id);
            }
            category.Name = categoryObj.Name;
            category.ImageUrl = categoryObj.ImageUrl;
            _dbContext.SaveChanges();
            return Ok("Record Updated.");
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if(category == null)
            {
                return NotFound("Record Not Found Against This Id: "+id);
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return Ok("Record Deleted.");
        }
    }
}
