using alxbrn_api.Data;
using alxbrn_api.Filters;
using alxbrn_api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;

namespace alxbrn_api.Controllers
{
    /// <summary>
    /// Class for managing all Product related operations.
    /// </summary>
    [Authorize]
    public class ProductsController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all products in a xlm or json format
        /// </summary>
        /// <returns>Products</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        /// <summary>
        /// Gets a search filtered list of products
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>Product</returns>
        [ResponseType(typeof(Product))]
        [Route("api/Products/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetProducts([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Product> source = db.Products.OrderBy(a => a.Title).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a => a.Title.Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<Product> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific product with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>Product</returns>
        [ResponseType(typeof(Product))]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Updates a specific product with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="product">product</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        /// Creates a new product by getting xlm or json object from body
        /// </summary>
        /// <param name="product">product</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        /// <summary>
        /// Deletes a specific product with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
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
        /// Checks if a specific product exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}