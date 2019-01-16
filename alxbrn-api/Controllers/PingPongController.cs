using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace alxbrn_api.Controllers
{
    /// <summary>
    /// PING PONG
    /// </summary>
    [Authorize]
    public class PingPongController : ApiController
    {
        /// <summary>
        /// PING PONG
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(string))]
        [Route("api/ping")]
        [AllowAnonymous]
        public IHttpActionResult GetPingPong()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string pong = "Pong!";

            return Ok(pong);
        }
    }
}
