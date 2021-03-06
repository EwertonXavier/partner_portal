using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class OrderDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/OrderData/ListOrders
        [HttpGet]
        public IEnumerable<OrderDto> ListOrders()
        {
            List<Order> orders = db.Orders.ToList();
            List<OrderDto> ordersDto = new List<OrderDto>();
            orders.ForEach(o => ordersDto.Add(new OrderDto()
            {
                Order_Id = o.Order_Id,
                Type = o.Type,
                Status = o.Status,
                Create_Date = o.Create_Date,
                Price = o.Price,
                PartnerId= o.PartnerId ,
                Consultant_Id=o.Consultant_Id,
                Artifact_Id =o.Artifact_Id

    }
            ));

            return ordersDto;
        }

        // GET: api/OrderData/FindOrder/5
        [ResponseType(typeof(OrderDto))]
        [HttpGet]
        public IHttpActionResult FindOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            OrderDto orderDto = new OrderDto()
            {
                Order_Id = order.Order_Id,
                Type = order.Type,
                Status = order.Status,
                Create_Date = order.Create_Date,
                Price = order.Price,
                PartnerId = order.PartnerId,
                Consultant_Id = order.Consultant_Id,
                Artifact_Id = order.Artifact_Id
            };
            return Ok(orderDto);
        }

        // PUT: api/OrderData/UpdateOrder/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Order_Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrderData/AddOrder
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult AddOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Order_Id }, order);
        }

        // DELETE: api/OrderData/DeleteOrder/5
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Order_Id == id) > 0;
        }
    }
}