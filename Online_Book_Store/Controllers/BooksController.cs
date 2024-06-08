using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;

namespace Online_Book_Store.Controllers
{
    public class BooksController : Controller 
    { 
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
           _service = service;

         }


        public async Task<IActionResult> Index()
        {
            var BooksData = await _service.GetAllAsync();
            return View(BooksData);
        }

        // Create
        // Get :
        public IActionResult Create() 
        { 
           
          return View();
        }

        // Create
        // Post :
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name,Author, ImageURL")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            await _service.AddAsync(book);

            return RedirectToAction(nameof(Index));
        }

        // Details
        // Get:
        public async Task<IActionResult> Details(int id)
        {
            //var actorDetails = _service.GetById(id);
            // (23)
            var bookDetails = await _service.GetByIdAsync(id);

            if (bookDetails == null) { return View("NotFound"); }

            return View(bookDetails);
        }
        // Edit
        // Get :
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id); // var mı/yok mu

            if (bookDetails == null) return View("NotFound");

            return View(bookDetails);
        }

        // Edit
        // Post : Update
        [HttpPost] // View tarafından gönderilecek verileri yakalamak için
        public IActionResult Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            _service.UpdateAsync(id, book); // Servisde çalışacak kısım

            return RedirectToAction(nameof(Index));

        }

        // Delete
        // Get: 
        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id); // var mı/yok mu

            if (bookDetails == null) return View("NotFound");

            return View(bookDetails);
        }

        // Delete
        // Post :
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookDetails = _service.GetByIdAsync(id);

            if (bookDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
