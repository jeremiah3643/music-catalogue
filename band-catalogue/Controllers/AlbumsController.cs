using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using band_catalogue.Data;
using band_catalogue.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/albums")]
public class AlbumsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AlbumsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/albums
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
    {
        return await _context.Albums.Include(a => a.Band)
                                    .Include(a => a.Songs)
                                    .ToListAsync();
    }

    // GET: api/albums/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Album>> GetAlbum(int id)
    {
        var album = await _context.Albums.Include(a => a.Band)
                                         .Include(a => a.Songs)
                                         .FirstOrDefaultAsync(a => a.AlbumId == id);

        if (album == null) return NotFound();
        return album;
    }

    // GET: api/albums/by-band/{bandId}
    [HttpGet("by-band/{bandId}")]
    public async Task<ActionResult<IEnumerable<Album>>> GetAlbumsByBand(int bandId)
    {
        var albums = await _context.Albums.Where(a => a.BandId == bandId).ToListAsync();
        if (albums.Count == 0) return NotFound();
        return albums;
    }

    // POST: api/albums
    [HttpPost]
    public async Task<ActionResult<Album>> CreateAlbum(Album album)
    {
        _context.Albums.Add(album);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAlbum), new { id = album.AlbumId }, album);
    }

    // PUT: api/albums/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAlbum(int id, Album album)
    {
        if (id != album.AlbumId) return BadRequest();
        _context.Entry(album).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/albums/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var album = await _context.Albums.FindAsync(id);
        if (album == null) return NotFound();

        _context.Albums.Remove(album);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
