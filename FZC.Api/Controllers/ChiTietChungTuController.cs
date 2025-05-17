using FZC.Domain.Entities;
using FZC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FZC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietChungTuController : ControllerBase
    {
        private readonly IChiTietChungTuRepository _chitietChungTu;
        public ChiTietChungTuController(IChiTietChungTuRepository chitietChungTu)
        {
            this._chitietChungTu = chitietChungTu;
        }
        // GET /api/chitietchungtu?filter={}&sort={}&range={}
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filter, [FromQuery] string? sort, [FromQuery] string? range)
        {
            var query = _chitietChungTu.Query();

            // React-admin expects pagination and filtering, you can parse filter/range here if needed
            // For demo, just return all
            var total = await query.CountAsync();
            var data = await query.ToListAsync();

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Add("Content-Range", $"chitietchungtu 0-{data.Count - 1}/{total}");

            return Ok(data);
        }

        // GET /api/chitietchungtu/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _chitietChungTu.Query().FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST /api/chitietchungtu
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChiTietChungTu model)
        {
            var result = await _chitietChungTu.AddAsync(model);
            await _chitietChungTu.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT /api/chitietchungtu/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChiTietChungTu model)
        {
            var existing = await _chitietChungTu.Query().FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return NotFound();

            model.Id = id;
            var result = await _chitietChungTu.UpdateAsync(model);
            await _chitietChungTu.SaveChangesAsync();
            return Ok(result);
        }

        // DELETE /api/chitietchungtu/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _chitietChungTu.Query().FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            await _chitietChungTu.DeleteAsync(item);
            await _chitietChungTu.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("by-chungtu/{chungTuId}")]
        public async Task<IActionResult> GetByChungTu(int chungTuId)
        {
            var result = await _chitietChungTu.Query().Where(x => x.ChungTuId == chungTuId).ToListAsync();
            return Ok(result);
        }

        [HttpGet("tong-hop-so-tien")]
        public async Task<IActionResult> TongHopSoTien()
        {
            var result = await _chitietChungTu.Query()
                .GroupBy(x => x.ChungTuId)
                .Select(g => new { ChungTuId = g.Key, TongTien = g.Sum(x => x.SoTien) })
                .ToListAsync();
            return Ok(result);
        }
    }
}
