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
    /// Controller for managing all Roles related operations.
    /// </summary>
    [Authorize]
    public class RolesController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all roles in a xlm or json format
        /// </summary>
        /// <returns>Role</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<Role> GetRoles()
        {
            return db.Roles;
        }

        /// <summary>
        /// Gets a search filtered list of roles
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>Role</returns>
        [ResponseType(typeof(Role))]
        [Route("api/Roles/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetRoles([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Role> source = db.Roles.OrderBy(a => a.Name).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a =>
                    a.Name.Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<Role> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific role with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>Role</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [ResponseType(typeof(Role))]
        public async Task<IHttpActionResult> GetRole(int id)
        {
            Role role = await db.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        /// <summary>
        /// Updates a specific role with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="user">role</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRole(int id, Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }

            db.Entry(role).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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
        /// Creates a new role by getting xlm or json object from body
        /// </summary>
        /// <param name="user">role</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(Role))]
        public async Task<IHttpActionResult> PostRole(Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Roles.Add(role);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = role.Id }, role);
        }

        /// <summary>
        /// Deletes a specific role with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(Role))]
        public async Task<IHttpActionResult> DeleteRole(int id)
        {
            Role role = await db.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            db.Roles.Remove(role);
            await db.SaveChangesAsync();

            return Ok(role);
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
        /// Checks if a specific role exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool RoleExists(int id)
        {
            return db.Roles.Count(e => e.Id == id) > 0;
        }
    }
}