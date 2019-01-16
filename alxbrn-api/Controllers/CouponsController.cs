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
    /// Controller for managing all Coupons related operations.
    /// </summary>
    [Authorize]
    public class CouponsController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all coupons in a xlm or json format
        /// </summary>
        /// <returns>Coupon</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<Coupon> GetCoupons()
        {
            return db.Coupons;
        }

        /// <summary>
        /// Gets a search filtered list of coupons
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>Coupon</returns>
        [ResponseType(typeof(Coupon))]
        [Route("api/Coupons/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetCoupons([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Coupon> source = db.Coupons.OrderBy(a => a.Name).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a => a.Name.Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<Coupon> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific coupon with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>Coupon</returns>
        [ResponseType(typeof(Coupon))]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public async Task<IHttpActionResult> GetCoupon(int id)
        {
            Coupon coupon = await db.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        /// <summary>
        /// Updates a specific coupon with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="user">coupon</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCoupon(int id, Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coupon.Id)
            {
                return BadRequest();
            }

            db.Entry(coupon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponExists(id))
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
        /// Creates a new coupon by getting xlm or json object from body
        /// </summary>
        /// <param name="user">coupon</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(Coupon))]
        public async Task<IHttpActionResult> PostCoupon(Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Coupons.Add(coupon);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = coupon.Id }, coupon);
        }

        /// <summary>
        /// Deletes a specific coupon with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(Coupon))]
        public async Task<IHttpActionResult> DeleteCoupon(int id)
        {
            Coupon coupon = await db.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            db.Coupons.Remove(coupon);
            await db.SaveChangesAsync();

            return Ok(coupon);
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
        /// Checks if a specific coupon exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool CouponExists(int id)
        {
            return db.Coupons.Count(e => e.Id == id) > 0;
        }
    }
}