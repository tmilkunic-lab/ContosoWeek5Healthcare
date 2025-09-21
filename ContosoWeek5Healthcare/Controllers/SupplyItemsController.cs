using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoWeek5Healthcare.Data;
using ContosoWeek5Healthcare.Models;
using ContosoWeek5Healthcare.ViewModels;

namespace ContosoWeek5Healthcare.Controllers
{
    public class SupplyItemsController : Controller
    {
        private readonly SchoolContext _db;
        public SupplyItemsController(SchoolContext db) => _db = db;

         
        public async Task<IActionResult> Index(string? search, string? sort = "item",
                                              int page = 1, int pageSize = 10)
        {
            IQueryable<SupplyItem> q = _db.SupplyItems.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                q = q.Where(i => i.ItemNumber.Contains(search) || i.Description.Contains(search));
                ViewData["CurrentFilter"] = search;
            }

            q = sort switch
            {
                "desc" => q.OrderBy(i => i.Description),
                "desc_desc" => q.OrderByDescending(i => i.Description),
                "par" => q.OrderBy(i => i.ParLevel),
                "par_desc" => q.OrderByDescending(i => i.ParLevel),
                "item_desc" => q.OrderByDescending(i => i.ItemNumber),
                _ => q.OrderBy(i => i.ItemNumber)
            };
            ViewData["CurrentSort"] = sort;

            var total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["Total"] = total;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;

            return View(items);
        }

        public IActionResult Create() => View(new SupplyItemEditVm());

        [HttpPost, ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(SupplyItemEditVm vm)
        {
            if (!ModelState.IsValid) return View(vm); // validation

            var entity = new SupplyItem
            {
                ItemNumber = vm.ItemNumber,
                Description = vm.Description,
                UnitOfMeasure = vm.UnitOfMeasure,
                LeadTimeDays = vm.LeadTimeDays,
                ParLevel = vm.ParLevel,
                ReorderPoint = vm.ReorderPoint,
                OnHand = vm.OnHand
            };

            
            TryValidateModel(entity);
            if (!ModelState.IsValid) return View(vm);

            _db.Add(entity);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Supply item created."; 
            return RedirectToAction(nameof(Index));       
        }

        public async Task<IActionResult> Edit(int id)
        {
            var i = await _db.SupplyItems.FindAsync(id);
            if (i is null) return NotFound();

            return View(new SupplyItemEditVm
            {
                Id = i.Id,
                ItemNumber = i.ItemNumber,
                Description = i.Description,
                UnitOfMeasure = i.UnitOfMeasure,
                LeadTimeDays = i.LeadTimeDays,
                ParLevel = i.ParLevel,
                ReorderPoint = i.ReorderPoint,
                OnHand = i.OnHand
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplyItemEditVm vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            var i = await _db.SupplyItems.FindAsync(id);
            if (i is null) return NotFound();

            i.ItemNumber = vm.ItemNumber;
            i.Description = vm.Description;
            i.UnitOfMeasure = vm.UnitOfMeasure;
            i.LeadTimeDays = vm.LeadTimeDays;
            i.ParLevel = vm.ParLevel;
            i.ReorderPoint = vm.ReorderPoint;
            i.OnHand = vm.OnHand;

            TryValidateModel(i);
            if (!ModelState.IsValid) return View(vm);

            await _db.SaveChangesAsync();
            TempData["Success"] = "Supply item updated.";
            return RedirectToAction(nameof(Index));
        }
    }
}
