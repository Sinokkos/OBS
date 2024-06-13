using Microsoft.AspNetCore.Mvc;
using Online_Book_Store.Data;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;

namespace Online_Book_Store.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublishersService _service;

        public PublishersController(IPublishersService service)
        {
            _service = service;

        }
        public async Task<IActionResult> Index()
        {
            var PublishersData= await _service.GetAllAsync();
            return View(PublishersData);
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
        public async Task<IActionResult> Create([Bind("Id, Name,Author, ImageURL")] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            await _service.AddAsync(publisher);

            return RedirectToAction(nameof(Index));
        }

        // Details
        // Get:
        public async Task<IActionResult> Details(int id)
        {
            //var actorDetails = _service.GetById(id);
            // (23)
            var publisherDetails = await _service.GetByIdAsync(id);

            if (publisherDetails == null) { return View("NotFound"); }

            return View(publisherDetails);
        }

        // Edit
        // Get :
        public async Task<IActionResult> Edit(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id); // var mı/yok mu

            if (publisherDetails == null) return View("NotFound");

            return View(publisherDetails);
        }

        // Edit
        // Post :
        [HttpPost] // View tarafından gönderilecek verileri yakalamak için
        public IActionResult Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);
            }

            _service.UpdateAsync(id, publisher); // Servisde çalışacak kısım

            return RedirectToAction(nameof(Index));

        }

        // Delete
        // Get: 
        public async Task<IActionResult> Delete(int id)
        {
            var publisherDetails = await _service.GetByIdAsync(id); // var mı/yok mu

            if (publisherDetails == null) return View("NotFound");

            return View(publisherDetails);
        }

        // Delete
        // Post :
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisherDetails = _service.GetByIdAsync(id);

            if (publisherDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
