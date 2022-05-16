using api.Context;
using api.Controllers.BaseController;
using api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            return await _context.AppUser.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApplicationUser>> GetUser(int id)
        {
            var user = await _context.AppUser.FindAsync(id);

            return user;
        }
    }
}