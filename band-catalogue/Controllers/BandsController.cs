using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using band_catalogue.Data;
using band_catalogue.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/bands")]
public class BandsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BandsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/bands
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Band>>> GetBands()
    {
        return await _context.Bands.Include(b => b.Albums).ToListAsync();
    }

    // GET: api/bands/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Band>> GetBand(int id)
    {
        var band = await _context.Bands.Include(b => b.Albums)
                                       .ThenInclude(a => a.Songs)
                                       .FirstOrDefaultAsync(b => b.BandId == id);

        if (band == null) return NotFound();

        return band;
    }

    // POST: api/bands
    [HttpPost]
    public async Task<ActionResult<Band>> CreateBand(Band band)
    {
        _context.Bands.Add(band);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBand), new { id = band.BandId }, band);
    }

    // DELETE: api/bands/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBand(int id)
    {
        var band = await _context.Bands.FindAsync(id);
        if (band == null) return NotFound();

        _context.Bands.Remove(band);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
