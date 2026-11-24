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
    public class OrderController : Controller
    {
        private readonly AppDataContext _context;

        public OrderController(AppDataContext context)
        {
            _context = context;
        }

       [HttpPost("addToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequestDTO requestDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Korzina newKortej = new Korzina()
                {
                    UserId = requestDTO.UserId,
                    ProductId = requestDTO.ProductId,
                    Cost = requestDTO.Cost,
                    Quantity = requestDTO.Quantity,
                    Status = requestDTO.Status
                };
                await _context.Korzinas.AddAsync(newKortej);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return StatusCode(200);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }

        [HttpPost("deleteProd")]
        public async Task<IActionResult> DeleteSelectedProd([FromBody] int? prodId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
               var existprod = await _context.Korzinas.Where(x => x.ProductId == prodId).FirstOrDefaultAsync();
               if(existprod == null)
                {
                    return BadRequest();
                }
                existprod.Status = false;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return StatusCode(200);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }

        [HttpGet("getProdInCart")]
        public async Task<ActionResult<IEnumerable<CartDTO?>>> GetCart([FromQuery] int? userId)
        {
            try
            {
                var list = await (from k in _context.Korzinas 
                join
                u in _context.Users on k.UserId equals u.Id 
                join p in _context.Products on k.ProductId equals p.Id where k.UserId == userId && k.Status == true select new CartDTO() 
                {
                    Id = k.Id,
                    UserId = k.UserId,
                    ProductId =  k.ProductId,
                    Quantity = k.Quantity,
                    Cost = k.Cost,
                    Status = k.Status,

                    ProductName = p.Name == null? "Нет данных" : p.Name,
                    UserName = u.Name == null? "Нет данных" : u.Name

                } ).ToListAsync();
                return list;
            }
            catch
            {
                return null;
            }
        }
        [HttpPost("addneworder")]
        public async Task<IActionResult> AddNewOrder([FromBody] AddOrderRequestDTO requestDTO)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == requestDTO.UserId);
            if (existUser == null)
            {
                return NotFound("Пользователь не найден");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var street = await _context.Streets
                    .FirstOrDefaultAsync(s => s.Name.ToLower() == requestDTO.StreetName.Trim().ToLower());

                if (street == null)
                {
                    street = new Street { Name = requestDTO.StreetName };
                    _context.Streets.Add(street);
                }

                var newOrder = new Order
                {
                    UserId = requestDTO.UserId,
                    DeliveryAdress = requestDTO.Adress,
                    DeliveryTime = requestDTO.DeliveryTime,
                    Cost = requestDTO.TotalCost
                };
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync(); // <-- Обязательно сохраняем здесь, чтобы newOrder.Id получил значение из БД!

                foreach (var prod in requestDTO.UserCart)
                {
                    _context.UserOrders.Add(new User_orders
                    {
                        ProductId = prod.ProductId,
                        OrderId = newOrder.Id, // <-- Теперь здесь гарантированно реальный Id из таблицы orders
                        Quantity = prod.Quantity
                    });
                }

                existUser.Apartment = requestDTO.ApartName;
                existUser.Home = requestDTO.HomeName;
                existUser.Family = requestDTO.Family;
                existUser.Name = requestDTO.Name;
                existUser.Father = requestDTO.Father;
                existUser.StreetId = street.Id; // (Если street тоже новый — его тоже нужно сохранить до этого места)
                existUser.Phone = requestDTO.Phone;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("deleteAllCart")]
        public async Task<IActionResult> DeleteAllCart([FromBody] List<CartDTO> cart)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach(var item in cart)
                {
                    var existprod = await _context.Korzinas.Where(x => x.ProductId == item.ProductId && x.Status == true).ToListAsync();
                    if(existprod == null)
                    {
                        return BadRequest();
                    }
                    foreach(var stat in existprod)
                    {
                        stat.Status = false;
                    }
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
                return StatusCode(200);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500);
            }
        }
    }
}
