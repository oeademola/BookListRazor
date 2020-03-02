using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private ApplicationDbContext _context;
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Json(new { data=await _context.Books.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookInDb = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookInDb == null)
                return Json(new { success = false, message = "Something unexpected happened." });

            _context.Books.Remove(bookInDb);
            await _context.SaveChangesAsync();

            return Json(new { success = "true", message = "Delete successful." });
        }
    }
}