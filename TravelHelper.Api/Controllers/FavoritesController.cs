using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelHelper.Api.Data;
using TravelHelper.Api.Dto;
using TravelHelper.Api.Models;

namespace TravelHelper.Api.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly TravelContext _db;
    public FavoritesController(TravelContext db) { _db = db; }

    private int UserId => int.Parse(User.Claims.First(c => c.Type == "sub").Value);

    [HttpGet]
    public async Task<IEnumerable<FavoriteDto>> Get()
    {
        return await _db.Favorites
            .Where(f => f.UserId == UserId)
            .Select(f => new FavoriteDto(f.Id, f.City, f.Note, f.Created))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FavoriteDto dto)
    {
        var fav = new Favorite { City = dto.City, Note = dto.Note, UserId = UserId };
        _db.Favorites.Add(fav); await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = fav.Id }, fav);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] FavoriteDto dto)
    {
        var fav = await _db.Favorites.FirstOrDefaultAsync(f => f.Id == id && f.UserId == UserId);
        if (fav == null) return NotFound();
        fav.City = dto.City; fav.Note = dto.Note;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var fav = await _db.Favorites.FirstOrDefaultAsync(f => f.Id == id && f.UserId == UserId);
        if (fav == null) return NotFound();
        _db.Remove(fav); await _db.SaveChangesAsync();
        return NoContent();
    }
}