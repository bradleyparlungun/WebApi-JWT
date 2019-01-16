using alxbrn_api.Data;
using alxbrn_api.DTOs;
using alxbrn_api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace alxbrn_api.Controllers
{
    /// <summary>
    /// Controller for managing all Authorize related operations.
    /// </summary>
    public class AuthorizeController : ApiController
    {
        private readonly ApplicationDatabaseContext _db = new ApplicationDatabaseContext();
        private readonly JwtTokenHelper _tokenHelper = new JwtTokenHelper();

        /// <summary>
        /// Gets a list of all authorized applications
        /// </summary>
        /// <returns>List AppDto</returns>
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public List<AppDto> Get()
        {
            return _db.AuthorizedApps
                    .Select(i => new AppDto
                    {
                        Name = i.Name,
                        TokenExpiration = i.TokenExpiration
                    }).ToList();
        }

        /// <summary>
        /// Creates a new authorized application
        /// </summary>
        /// <param name="request">AuthorizeRequestDto</param>
        /// <returns>StatusCode</returns>
        public IHttpActionResult Post(AuthorizeRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Models.AuthorizedApp authApp = _db.AuthorizedApps
                .FirstOrDefault(i => i.AppToken == request.AppToken
                                     && i.AppSecret == request.AppSecret
                                     && DateTime.UtcNow < i.TokenExpiration);

            if (authApp == null)
            {
                return Unauthorized();
            }

            TokenDto token = _tokenHelper.CreateToken(authApp);
            return Ok(token);
        }
    }
}
