using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using band_catalogue.Data;
using band_catalogue.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/songs")]
public class SongsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SongsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/songs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
    {
        return await _context.Songs.Include(s => s.Album).ToListAsync();
    }

    // GET: api/songs/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetSong(int id)
    {
        var song = await _context.Songs.Include(s => s.Album)
                                       .FirstOrDefaultAsync(s => s.SongId == id);

        if (song == null) return NotFound();
        return song;
    }

    // GET: api/songs/by-album/{albumId}
    [HttpGet("by-album/{albumId}")]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongsByAlbum(int albumId)
    {
        var songs = await _context.Songs.Where(s => s.AlbumId == albumId).ToListAsync();
        if (songs.Count == 0) return NotFound();
        return songs;
    }

    // POST: api/songs
    [HttpPost]
    public async Task<ActionResult<Song>> CreateSong(Song song)
    {
        _context.Songs.Add(song);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSong), new { id = song.SongId }, song);
    }

    // PUT: api/songs/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSong(int id, Song song)
    {
        if (id != song.SongId) return BadRequest();
        _context.Entry(song).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/songs/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
        var song = await _context.Songs.FindAsync(id);
        if (song == null) return NotFound();

        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
