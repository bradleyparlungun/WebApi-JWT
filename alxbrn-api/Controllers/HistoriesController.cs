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
    /// Controller for managing all Histories related operations.
    /// </summary>
    [Authorize]
    public class HistoriesController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all histories in a xlm or json format
        /// </summary>
        /// <returns>History</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<History> GetHistories()
        {
            return db.Histories;
        }

        /// <summary>
        /// Gets a search filtered list of histories
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>History</returns>
        [ResponseType(typeof(History))]
        [Route("api/Histories/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetHistories([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<History> source = db.Histories.OrderBy(a => a.Title).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a => a.Title.Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<History> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific history with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>History</returns>
        [ResponseType(typeof(History))]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public async Task<IHttpActionResult> GetHistory(int id)
        {
            History history = await db.Histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        /// <summary>
        /// Updates a specific history with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="user">history</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHistory(int id, History history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != history.Id)
            {
                return BadRequest();
            }

            db.Entry(history).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryExists(id))
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
        /// Creates a new history by getting xlm or json object from body
        /// </summary>
        /// <param name="user">history</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(History))]
        public async Task<IHttpActionResult> PostHistory(History history)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Histories.Add(history);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = history.Id }, history);
        }

        /// <summary>
        /// Deletes a specific history with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(History))]
        public async Task<IHttpActionResult> DeleteHistory(int id)
        {
            History history = await db.Histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            db.Histories.Remove(history);
            await db.SaveChangesAsync();

            return Ok(history);
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
        /// Checks if a specific history exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool HistoryExists(int id)
        {
            return db.Histories.Count(e => e.Id == id) > 0;
        }
    }
}