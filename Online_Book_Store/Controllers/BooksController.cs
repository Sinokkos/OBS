using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Data.Services;
using Online_Book_Store.Models;
using Online_Book_Store.ViewModels;

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
            var BooksData = await _service.GetAllAsync(n => n.Author);
            return View(BooksData);
        }

        // Create
        // Get :
        public async Task<IActionResult> Create() 
        {
            var bookDropDownsData = await _service.GetNewBookDropdownsValues();
            ViewBag.Authors = new SelectList(bookDropDownsData.Authors, "Id", "Name");

            ViewBag.Publishers = new SelectList(bookDropDownsData.Publishers, "Id", "Name");

            

            return View();
            
        }

        // Create
        // Post :
        [HttpPost]
        public async Task<IActionResult> Create(NewBookVM book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropDownsData = await _service.GetNewBookDropdownsValues();
                ViewBag.Authors = new SelectList(bookDropDownsData.Authors, "Id", "Name");

                ViewBag.Publishers = new SelectList(bookDropDownsData.Publishers, "Id", "Name");
                return View(book);
            }


            await _service.AddNewBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        // Details
        // Get:
        public async Task<IActionResult> Details(int id)
        {
            //var actorDetails = _service.GetById(id);
            // (23)
            var bookDetails = await _service.GetBookByIdAsync(id);

            if (bookDetails == null) { return View("NotFound"); }

            return View(bookDetails);
        }
        // Edit
        // Get :
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id); // var mı/yok mu

            if (bookDetails == null) return View("NotFound");
            var data = new NewBookVM()
            {
                Id = bookDetails.Id,
                Name = bookDetails.Name,
                Description = bookDetails.Description,
                Price = bookDetails.Price,
                PublicationDate = bookDetails.PublicationDate,
                ImageURL = bookDetails.ImageURL,
                BookCategory = bookDetails.BookCategory,
                AuthorName = await _service.GetAuthor(bookDetails.AuthorId),
                PublisherName = await _service.GetPublisher(bookDetails.PublisherId),
                
            };
            var bookDropDownsData = await _service.GetNewBookDropdownsValues();
            
            ViewBag.Authors = new SelectList(bookDropDownsData.Authors, "Id", "Name");

            ViewBag.Publishers = new SelectList(bookDropDownsData.Publishers, "Id", "Name");

            
            return View(data);
        }

        // Edit
        // Post : Update
        [HttpPost] // View tarafından gönderilecek verileri yakalamak için
        public async Task<IActionResult> Edit(int id, NewBookVM book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropDownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.Authors = new SelectList(bookDropDownsData.Authors, "Id", "Name");

                ViewBag.Publishers = new SelectList(bookDropDownsData.Publishers, "Id", "Name");

                return View(book);
            }

            await _service.UpdateBookAsync(book);

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
            var bookDetails = _service.GetBookByIdAsync(id);

            if (bookDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Filter(string searchString)
        {
            // Burası film adı veya Description üzerinde arama kısmı

            // Öncelikle VT Movies tablosundaki tüm kayıtları bir okuyalım
            var allBooks = await _service.GetAllAsync(n => n.Author, n=> n.Publisher);

            // Searchtext i doldurulmadan da arama butonuna basılmış olabilir. searchString in dolu olup olmadığına bakılıyor

            if (!string.IsNullOrEmpty(searchString))
            {
                // Boş değilse

                var filteredResult = allBooks
                                .Where(n => n.Name.ToLower()
                                .Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                return View("Index", filteredResult);


            }

            return View("Index", allBooks);

        }
    }
}
