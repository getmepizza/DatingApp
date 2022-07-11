using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Context;
using api.Controllers.BaseController;
using api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly ApplicationContext _context;
        public BuggyController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("Auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]

        public ActionResult<ApplicationUser> GetNotFound()
        {
            var thing = _context.AppUser.Find(-1);

            if (thing == null)
                return NotFound();
            return Ok();


        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.AppUser.Find(-1);

            return thing.ToString();

        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("Bad Request.");
        }

    }
}