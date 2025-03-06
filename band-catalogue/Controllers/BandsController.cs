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
            .FirstOrDefaultAsync(m => m.BandId == id);
        if (band == null) return NotFound();
        return View(band);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Band band)
    {
        if (ModelState.IsValid)
        {
            _context.Add(band);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(band);
    }
}
