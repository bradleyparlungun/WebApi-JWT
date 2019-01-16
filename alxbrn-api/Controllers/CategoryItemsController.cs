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
    /// Controller for managing all CategoryItem related operations.
    /// </summary>
    [Authorize]
    public class CategoryItemsController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all categoryitems in a xlm or json format
        /// </summary>
        /// <returns>CategoryItem</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<CategoryItem> GetCategoryItems()
        {
            return db.CategoryItems;
        }

        /// <summary>
        /// Gets a search filtered list of categoryitems
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>CategoryItem</returns>
        [ResponseType(typeof(CategoryItem))]
        [Route("api/CategoryItems/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetCategoryItems([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<CategoryItem> source = db.CategoryItems.OrderBy(a => a.Name).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a => a.Name.Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<CategoryItem> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific categoryitem with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>CategoryItem</returns>
        [ResponseType(typeof(CategoryItem))]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public async Task<IHttpActionResult> GetCategoryItem(int id)
        {
            CategoryItem categoryItem = await db.CategoryItems.FindAsync(id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            return Ok(categoryItem);
        }

        /// <summary>
        /// Updates a specific categoryitem with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="user">categoryitem</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategoryItem(int id, CategoryItem categoryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoryItem.Id)
            {
                return BadRequest();
            }

            db.Entry(categoryItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryItemExists(id))
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
        /// Creates a new categoryitem by getting xlm or json object from body
        /// </summary>
        /// <param name="user">categoryitem</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(CategoryItem))]
        public async Task<IHttpActionResult> PostCategoryItem(CategoryItem categoryItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoryItems.Add(categoryItem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = categoryItem.Id }, categoryItem);
        }

        /// <summary>
        /// Deletes a specific categoryitem with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(CategoryItem))]
        public async Task<IHttpActionResult> DeleteCategoryItem(int id)
        {
            CategoryItem categoryItem = await db.CategoryItems.FindAsync(id);
            if (categoryItem == null)
            {
                return NotFound();
            }

            db.CategoryItems.Remove(categoryItem);
            await db.SaveChangesAsync();

            return Ok(categoryItem);
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
        /// Checks if a specific categoryitem exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool CategoryItemExists(int id)
        {
            return db.CategoryItems.Count(e => e.Id == id) > 0;
        }
    }
}