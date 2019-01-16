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
    /// Controller for managing all HistoryActions related operations.
    /// </summary>
    [Authorize]
    public class HistoryActionsController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all historyactions in a xlm or json format
        /// </summary>
        /// <returns>HistoryAction</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<HistoryAction> GetHistoryActions()
        {
            return db.HistoryActions;
        }

        /// <summary>
        /// Gets a search filtered list of historyactions
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>HistoryAction</returns>
        [ResponseType(typeof(HistoryAction))]
        [Route("api/HistoryActions/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetHistoryActions([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<HistoryAction> source = db.HistoryActions.OrderBy(a => a.Name).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a => a.Name.Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<HistoryAction> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific historyaction with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>HistoryAction</returns>
        [ResponseType(typeof(HistoryAction))]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public async Task<IHttpActionResult> GetHistoryAction(int id)
        {
            HistoryAction historyAction = await db.HistoryActions.FindAsync(id);
            if (historyAction == null)
            {
                return NotFound();
            }

            return Ok(historyAction);
        }

        /// <summary>
        /// Updates a specific historyaction with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="user">history</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHistoryAction(int id, HistoryAction historyAction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historyAction.Id)
            {
                return BadRequest();
            }

            db.Entry(historyAction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryActionExists(id))
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
        /// Creates a new historyaction by getting xlm or json object from body
        /// </summary>
        /// <param name="user">historyaction</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(HistoryAction))]
        public async Task<IHttpActionResult> PostHistoryAction(HistoryAction historyAction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HistoryActions.Add(historyAction);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = historyAction.Id }, historyAction);
        }

        /// <summary>
        /// Deletes a specific historyaction with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(HistoryAction))]
        public async Task<IHttpActionResult> DeleteHistoryAction(int id)
        {
            HistoryAction historyAction = await db.HistoryActions.FindAsync(id);
            if (historyAction == null)
            {
                return NotFound();
            }

            db.HistoryActions.Remove(historyAction);
            await db.SaveChangesAsync();

            return Ok(historyAction);
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
        /// Checks if a specific historyaction exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool HistoryActionExists(int id)
        {
            return db.HistoryActions.Count(e => e.Id == id) > 0;
        }
    }
}