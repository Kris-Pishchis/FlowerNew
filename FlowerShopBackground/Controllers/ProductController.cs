using FlowerShopBackground.Data;
using FlowerShopSharedModels.Models;
using FlowerShopSharedModels.requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Diagnostics;

namespace FlowerShopBackground.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly AppDataContext _context;

        public ProductController(AppDataContext context)
        {
            _context = context;
        }

        [HttpGet("getProducts")]
        public async Task<ActionResult<IEnumerable<List<Product>>>> GetProducts()
        {
            try
            {
                var listProducts = await (from p in _context.Products
                                          select new Product()
                                          {
                                              Id = p.Id,
                                              Name = p.Name,
                                              Cost = p.Cost,
                                              PhotoHashed = p.PhotoHashed != null? p.PhotoHashed : null
                                          }).ToListAsync();
                return Ok(listProducts);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
