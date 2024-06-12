using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;

namespace Project_Keystone.Api.Controllers
{
    public class ProductsController : ControllerBase
    {
        private readonly ProjectKeystoneDbContext _dbContext;

        public ProductsController(ProjectKeystoneDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Product")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }
    }
}
