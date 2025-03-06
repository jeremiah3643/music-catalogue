using Microsoft.AspNetCore.Mvc;
using band_catalogue.Data;
using band_catalogue.Models;
using Microsoft.EntityFrameworkCore;

public class BandsController : Controller
{
    private readonly ApplicationDbContext _context;

    public BandsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Bands.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var band = await _context.Bands
            .Include(b => b.Albums)
            .ThenInclude(a => a.Songs)
            .FirstOrDefaultAsync(m => m.BandId == id);
        if (band == null) return NotFound();
        return View(band);
    }

    public IActionResult Create()
    {
        return View();
    }

     [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BandId,BandName,Genre,Country,FormedYear")] Band band)
    {
        if (ModelState.IsValid)
        {
            _context.Add(band);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(band);
    }

   // GET: Bands/Edit/{id}
    public async Task<IActionResult> Edit(int id)
    {
        var band = await _context.Bands.FindAsync(id);
        if (band == null)
        {
            return NotFound();
        }
        return View(band);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Band band)
    {
        if (id != band.BandId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(band);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bands.Any(e => e.BandId == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(band);
    }
    public async Task<IActionResult> Delete(int id)
    {
        var band = await _context.Bands.FirstOrDefaultAsync(m => m.BandId == id);
        if (band == null) return NotFound();
        return View(band);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var band = await _context.Bands.FindAsync(id);
        _context.Bands.Remove(band);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
