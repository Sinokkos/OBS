using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;

namespace Online_Book_Store.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _service;

        public AuthorsController(IAuthorsService service)
        {
            _service = service;

        }
        public async Task<IActionResult> Index()
        {
            var AuthorsData = await _service.GetAllAsync();
            return View(AuthorsData);
        }

        // Create 
        // Get:

        public IActionResult Create()
        {
            return View();
        }

        // Create
        // Post
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name,Author, ImageURL")] Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            await _service.AddAsync(author);

            return RedirectToAction(nameof(Index));

        }

        // Details
        // Get:
        public async Task<IActionResult> Details(int id)
        {
            //var actorDetails = _service.GetById(id);
            // (23)
            var authorDetails = await _service.GetByIdAsync(id);

            if (authorDetails == null) { return View("NotFound"); }

            return View(authorDetails);
        }

        // Edit
        // Get :
        public async Task<IActionResult> Edit(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id); // var mı/yok mu

            if (authorDetails == null) return View("NotFound");

            return View(authorDetails);
        }

        // Edit
        // Post :
        [HttpPost] // View tarafından gönderilecek verileri yakalamak için
        public IActionResult Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            _service.UpdateAsync(id, author); // Servisde çalışacak kısım

            return RedirectToAction(nameof(Index));

        }
        // Delete
        // Get :
        public async Task<IActionResult> Delete(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id); // var mı/yok mu

            if (authorDetails == null) return View("NotFound");

            return View(authorDetails);
        }

        // Delete
        // Post :
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorDetails = _service.GetByIdAsync(id);

            if (authorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
