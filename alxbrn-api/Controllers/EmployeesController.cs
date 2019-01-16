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
    /// Controller for managing all Employees related operations.
    /// </summary>
    [Authorize]
    public class EmployeesController : ApiController
    {
        private readonly ApplicationDatabaseContext db = new ApplicationDatabaseContext();

        /// <summary>
        /// Gets all employees in a xlm or json format
        /// </summary>
        /// <returns>Employee</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        /// <summary>
        /// Gets a search filtered list of employees
        /// </summary>
        /// <param name="pagingparametermodel">pagingparametermodel</param>
        /// <returns>Employee</returns>
        [ResponseType(typeof(Employee))]
        [Route("api/Employees/Search")]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IHttpActionResult GetEmployees([FromUri]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Employee> source = db.Employees.OrderBy(a => a.Firstname).AsQueryable();

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a => a.Firstname.Contains(pagingparametermodel.QuerySearch) ||
                                           a.Lastname.Contains(pagingparametermodel.QuerySearch) ||
                                           a.Email.Contains(pagingparametermodel.QuerySearch));
            }

            int PageSize = pagingparametermodel.PageSize;
            List<Employee> items = source.Skip((pagingparametermodel.PageNumber - 1) * PageSize).Take(PageSize).ToList();

            return Ok(items);
        }

        /// <summary>
        /// Gets a specific employee with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>Employee</returns>
        [ResponseType(typeof(Employee))]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Updates a specific employee with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <param name="user">employee</param>
        /// <returns>StatusCode</returns>
        [DbUpdateExceptionFilter]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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
        /// Creates a new employee by getting xlm or json object from body
        /// </summary>
        /// <param name="user">employee</param>
        /// <returns>CreatedAtRoute</returns>
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Deletes a specific employee with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>StatusCode</returns>
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return Ok(employee);
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
        /// Checks if a specific employee exists with a identifier
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>bool</returns>
        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }
    }
}