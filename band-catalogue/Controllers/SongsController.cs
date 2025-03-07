using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using band_catalogue.Data;
using band_catalogue.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

public class SongsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SongsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ LIST: Show all songs for an album
    public async Task<IActionResult> Index(int albumId)
    {
        var songs = await _context.Songs
            .Where(s => s.AlbumId == albumId)
            .Include(s => s.Album)
            .ToListAsync();

        ViewBag.Album = await _context.Albums.FindAsync(albumId);

        return View(songs);
    }

    // ✅ DETAILS: Show song details
    public async Task<IActionResult> Details(int id)
    {
        var song = await _context.Songs
            .Include(s => s.Album)
            .FirstOrDefaultAsync(s => s.SongId == id);

        if (song == null) return NotFound();
        return View(song);
    }

    // ✅ CREATE: Show form
    [HttpGet]
    public IActionResult Create(int albumId)
    {
        ViewBag.AlbumId = albumId;
        ViewBag.AlbumTitle = _context.Albums.Find(albumId)?.Title;
        return View();
    }

    // ✅ CREATE: Handle form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title, Duration, AlbumId")] Song song)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.AlbumId = song.AlbumId;
            ViewBag.AlbumTitle = _context.Albums.Find(song.AlbumId)?.Title;
            return View(song);
        }

        _context.Songs.Add(song);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", new { albumId = song.AlbumId });
    }

    // ✅ EDIT: Show form
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var song = await _context.Songs.FindAsync(id);
        if (song == null) return NotFound();

        return View(song);
    }

    // ✅ EDIT: Handle form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Song song)
    {
        if (id != song.SongId) return NotFound();

        if (!ModelState.IsValid)
        {
            return View(song);
        }

        try
        {
            _context.Update(song);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Songs.Any(e => e.SongId == id)) return NotFound();
            throw;
        }

        return RedirectToAction("Index", new { albumId = song.AlbumId });
    }

    // ✅ DELETE: Show confirmation page
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var song = await _context.Songs
            .Include(s => s.Album)
            .FirstOrDefaultAsync(m => m.SongId == id);

        if (song == null) return NotFound();
        return View(song);
    }

    // ✅ DELETE: Handle form submission
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var song = await _context.Songs.FindAsync(id);
        if (song != null)
        {
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", new { albumId = song.AlbumId });
    }
}
