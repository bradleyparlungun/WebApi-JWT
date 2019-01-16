using alxbrn_api.Data;
using alxbrn_api.Filters;
using alxbrn_api.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace alxbrn_api.Controllers
{
    /// <summary>
    /// Controller for managing all OrderLines related operations.
    /// </summary>
    [Authorize]
    public class OrderLinesController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all orderlines in a xlm or json format
        /// </summary>
        /// <returns>OrderLine</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<OrderLine> GetOrderLines()
        {
            return db.OrderLines;
        }

        /// <summary>
        /// Gets a search filtered list of orderlines
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>OrderLine</returns>
        [ResponseType(typeof(OrderLine))]
        [Route("api/OrderLines/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetOrderLines([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<OrderLine> source = db.OrderLines.OrderByDescending(a => a.Id).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a =>
                    a.Id.ToString().Contains(pagingparametermodel.QuerySearch) ||
                    a.OrderId.ToString().Contains(pagingparametermodel.QuerySearch) ||
                    a.TotalPrice.ToString().Contains(pagingparametermodel.QuerySearch) ||
                    a.UnitId.ToString().Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<OrderLine> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific orderline with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>OrderLine</returns>
        [ResponseType(typeof(OrderLine))]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public async Task<IHttpActionResult> GetOrderLine(int id)
        {
            OrderLine orderLine = await db.OrderLines.FindAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            return Ok(orderLine);
        }

        /// <summary>
        /// Updates a specific orderline with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="user">orderline</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrderLine(int id, OrderLine orderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderLine.Id)
            {
                return BadRequest();
            }

            db.Entry(orderLine).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLineExists(id))
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

        /// <summary>
        /// Creates a new orderline by getting xlm or json object from body
        /// </summary>
        /// <param name="user">orderline</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(OrderLine))]
        public async Task<IHttpActionResult> PostOrderLine(OrderLine orderLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderLines.Add(orderLine);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = orderLine.Id }, orderLine);
        }

        /// <summary>
        /// Deletes a specific orderline with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(OrderLine))]
        public async Task<IHttpActionResult> DeleteOrderLine(int id)
        {
            OrderLine orderLine = await db.OrderLines.FindAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            db.OrderLines.Remove(orderLine);
            await db.SaveChangesAsync();

            return Ok(orderLine);
        }

        /// <summary>
        /// Disposes of current database context
        /// </summary>
        /// <param name="disposing">void</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Checks if a specific orderline exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool OrderLineExists(int id)
        {
            return db.OrderLines.Count(e => e.Id == id) > 0;
        }
    }
}