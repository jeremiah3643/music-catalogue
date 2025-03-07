using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using band_catalogue.Data;
using band_catalogue.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

public class AlbumsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AlbumsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ LIST: Show all albums
    public async Task<IActionResult> Index()
    {
        var albums = await _context.Albums
            .Include(a => a.Band)
            .ToListAsync();
        return View(albums);
    }

    // ✅ DETAILS: Show details of a specific album
    public async Task<IActionResult> Details(int id)
    {
        var album = await _context.Albums
            .Include(a => a.Band)
            .Include(a => a.Songs)
            .FirstOrDefaultAsync(m => m.AlbumId == id);

        if (album == null) return NotFound();
        return View(album);
    }

    // ✅ CREATE: Show form
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Bands = new SelectList(_context.Bands, "BandId", "BandName");
        return View();
    }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Title, ReleaseYear, BandId")] Album album)
{
    // Log received values
    Console.WriteLine($"📌 Received Album Data -> Title: {album.Title}, ReleaseYear: {album.ReleaseYear}, BandId: {album.BandId}");

    if (!ModelState.IsValid)
    {
        foreach (var key in ModelState.Keys)
        {
            var errors = ModelState[key].Errors.Select(e => e.ErrorMessage).ToList();
            if (errors.Count > 0)
            {
                Console.WriteLine($"❌ Validation Error - {key}: {string.Join(", ", errors)}");
            }
        }

        // Repopulate dropdown
        ViewBag.Bands = new SelectList(_context.Bands, "BandId", "BandName", album.BandId);
        return View(album);
    }

    // Save album
    _context.Albums.Add(album);
    await _context.SaveChangesAsync();
    Console.WriteLine("✅ Album successfully created!");
    return RedirectToAction(nameof(Index));
}


    // ✅ EDIT: Show form
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var album = await _context.Albums.FindAsync(id);
        if (album == null) return NotFound();

        ViewBag.Bands = new SelectList(_context.Bands, "BandId", "BandName", album.BandId);
        return View(album);
    }

    // ✅ EDIT: Handle form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Album album)
    {
        if (id != album.AlbumId) return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.Bands = new SelectList(_context.Bands, "BandId", "BandName", album.BandId);
            return View(album);
        }

        try
        {
            _context.Update(album);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Albums.Any(e => e.AlbumId == id)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }


// ✅ GET: Show delete confirmation page
[HttpGet]
public async Task<IActionResult> Delete(int id)
{
    var album = await _context.Albums
        .Include(a => a.Band)
        .FirstOrDefaultAsync(m => m.AlbumId == id);

    if (album == null) return NotFound();

    return View(album); // Show confirmation page
}

// ✅ POST: Handle album deletion
[HttpPost, ActionName("DeleteConfirmed")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var album = await _context.Albums.FindAsync(id);
    if (album != null)
    {
        _context.Albums.Remove(album);
        await _context.SaveChangesAsync();
        Console.WriteLine($"✅ Deleted Album: {album.Title}");
    }
    else
    {
        Console.WriteLine($"❌ Attempted to delete a non-existing album with ID {id}");
    }

    return RedirectToAction(nameof(Index));
}


}
