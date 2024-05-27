using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.WebAPI.DatabaseContext;
using Orders.WebAPI.Models;

namespace Orders.WebAPI.Controllers
{
    public class OrderItemsController : CustomController
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrdersItem()
        {
            if (_context.OrdersItem == null)
            {
                return NotFound();
            }
            return await _context.OrdersItem.ToListAsync();
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(Guid id)
        {
            if (_context.OrdersItem == null)
            {
                return NotFound();
            }
            var orderItem = await _context.OrdersItem.FindAsync(id);

            if (orderItem == null)
            {
                return Problem(detail: "Invalid order item Id", statusCode: 404, title: "Order item search");
            }

            return orderItem;
        }

        // PUT: api/OrderItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(Guid id, OrderItem orderItem)
        {
            if (id != orderItem.OrderItemId)
            {
                return BadRequest();
            }

            var existingOrderItem = await _context.OrdersItem.FindAsync(id);

            if (existingOrderItem == null)
            {
                return NotFound();
            }

            existingOrderItem.OrderId = orderItem.OrderId;
            existingOrderItem.ProductName = orderItem.ProductName;
            existingOrderItem.Quantity = orderItem.Quantity;
            existingOrderItem.UnitPrice = orderItem.UnitPrice;
            existingOrderItem.TotalPrice = orderItem.TotalPrice;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(existingOrderItem);
        }

        // POST: api/OrderItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            if (_context.OrdersItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrdersItem'  is null.");
            }
            _context.OrdersItem.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = orderItem.OrderItemId }, orderItem);
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            if (_context.OrdersItem == null)
            {
                return NotFound();
            }
            var orderItem = await _context.OrdersItem.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrdersItem.Remove(orderItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderItemExists(Guid id)
        {
            return (_context.OrdersItem?.Any(e => e.OrderItemId == id)).GetValueOrDefault();
        }

        //GET: api/OrderItems/5/items
        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderItems = _context.OrdersItem.Where(temp => temp.OrderId == id);

            if (orderItems == null)
            {
                return NotFound();
            }

            return await orderItems.ToListAsync();
        }
    }
}
