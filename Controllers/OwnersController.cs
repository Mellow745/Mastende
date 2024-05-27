using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mastende.Models;
using Microsoft.AspNetCore.Authorization;

namespace Mastende.Controllers
{
    
    public class OwnersController : Controller
    {
        private readonly MastendeContext _context;

        public OwnersController(MastendeContext context)
        {
            _context = context;
        }

        // GET: Owners

        public async Task<IActionResult> Index(string searchString, string sortOrder, int? page)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LocationSortParm"] = sortOrder == "Location" ? "location_desc" : "Location";
            ViewData["CurrentFilter"] = searchString;

            var owners = from o in _context.LandlordDb
                         select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                owners = owners.Where(o => o.Fullname != null && o.Fullname.Contains(searchString) || o.Location != null && o.Location.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    owners = owners.OrderByDescending(o => o.Fullname);
                    break;
                case "Location":
                    owners = owners.OrderBy(o => o.Location);
                    break;
                case "location_desc":
                    owners = owners.OrderByDescending(o => o.Location);
                    break;
                default:
                    owners = owners.OrderBy(o => o.Fullname);
                    break;
            }

            var count = await owners.CountAsync();


            int pageSize = 10; 
            int pageNumber = (page ?? 1); 

            ViewBag.CurrentPage = page ?? 1; 
            ViewBag.TotalPages = (int)Math.Ceiling((double)count / pageSize);


            return View(await PaginatedList<Owner>.CreateAsync(owners.AsNoTracking(), pageNumber, pageSize));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LandlordDb == null)
            {
                return NotFound();
            }

            var owner = await _context.LandlordDb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        [Authorize]
        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Owner model)
        {
            if (ModelState.IsValid)
            {
                model.Fullname = model.GetFullName();
                model.Location = model.GetLocation();

                if (model.Pictures != null && model.Pictures.Any())
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var savedFilenames = new List<string>();

                    try
                    {
                        foreach (var picture in model.Pictures)
                        {
                            if (picture.Length > 0)
                            {
                                var fileExtension = Path.GetExtension(picture.FileName).ToLowerInvariant();

                                if (!allowedExtensions.Contains(fileExtension))
                                {
                                    return BadRequest("Only image files (jpg, jpeg, png, gif) are allowed.");
                                }

                                var fileName = Guid.NewGuid().ToString() + fileExtension;
                                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await picture.CopyToAsync(stream);
                                }

                                savedFilenames.Add(fileName);
                            }
                        }

                        model.PicturesFile = string.Join(",", savedFilenames);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Pictures", "Error saving images: " + ex.Message);
                        return View(model);
                    }
                }

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize]
        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LandlordDb == null)
            {
                return NotFound();
            }

            var owner = await _context.LandlordDb.FindAsync(id);

            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,StreetName,Area,Town,CellNumber,Bedrooms,Bathrooms,Garage,Description,Price")] Owner owner)
        {
            if (id != owner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        [Authorize]
        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LandlordDb == null)
            {
                return NotFound();
            }

            var owner = await _context.LandlordDb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LandlordDb == null)
            {
                return Problem("Entity set 'MastendeContext.LandlordDb'  is null.");
            }
            var owner = await _context.LandlordDb.FindAsync(id);
            if (owner != null)
            {
                _context.LandlordDb.Remove(owner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
          return (_context.LandlordDb?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
