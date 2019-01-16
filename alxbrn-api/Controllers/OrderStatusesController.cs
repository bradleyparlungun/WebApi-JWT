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
    /// Controller for managing all OderStatuses related operations.
    /// </summary>
    [Authorize]
    public class OrderStatusesController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all orderstatuses in a xlm or json format
        /// </summary>
        /// <returns>OrderStatus</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<OrderStatus> GetOrderStatuses()
        {
            return db.OrderStatuses;
        }

        /// <summary>
        /// Gets a search filtered list of orderstatuses
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>OrderStatus</returns>
        [ResponseType(typeof(OrderStatus))]
        [Route("api/OrderStatuses/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetOrderStatuses([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<OrderStatus> source = db.OrderStatuses.OrderBy(a => a.Name).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a =>
                    a.Name.Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<OrderStatus> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific orderstatus with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>OrderStatus</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [ResponseType(typeof(OrderStatus))]
        public async Task<IHttpActionResult> GetOrderStatus(int id)
        {
            OrderStatus orderStatus = await db.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(orderStatus);
        }

        /// <summary>
        /// Updates a specific orderstatus with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="user">orderstatus</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrderStatus(int id, OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderStatus.Id)
            {
                return BadRequest();
            }

            db.Entry(orderStatus).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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
        /// Creates a new orderstatus by getting xlm or json object from body
        /// </summary>
        /// <param name="user">orderstatus</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(OrderStatus))]
        public async Task<IHttpActionResult> PostOrderStatus(OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderStatuses.Add(orderStatus);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = orderStatus.Id }, orderStatus);
        }

        /// <summary>
        /// Deletes a specific orderstatus with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(OrderStatus))]
        public async Task<IHttpActionResult> DeleteOrderStatus(int id)
        {
            OrderStatus orderStatus = await db.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            db.OrderStatuses.Remove(orderStatus);
            await db.SaveChangesAsync();

            return Ok(orderStatus);
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
        /// Checks if a specific orderstatus exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool OrderStatusExists(int id)
        {
            return db.OrderStatuses.Count(e => e.Id == id) > 0;
        }
    }
}