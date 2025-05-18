using FZC.Application.Dtos;
using FZC.Domain.Entities;
using FZC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace FZC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietChungTuController : ControllerBase
    {
        private readonly IChiTietChungTuRepository _chitietChungTu;
        private readonly IChungTuRepository _chungTu;
        public ChiTietChungTuController(IChiTietChungTuRepository chitietChungTu, IChungTuRepository chungTu)
        {
            _chitietChungTu = chitietChungTu;
            _chungTu = chungTu;
        }
        // GET /api/chitietchungtu?filter={}&sort={}&range={}
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filter, [FromQuery] string? sort, [FromQuery] string? range)
        {
            var query = _chitietChungTu.Query();

            if (!string.IsNullOrEmpty(filter))
            {
                var filterObj = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(filter);
                if (filterObj != null)
                {
                    if (filterObj.TryGetValue("chungTuId", out var chungTuIdObj) && int.TryParse(chungTuIdObj?.ToString(), out var chungTuId))
                    {
                        query = query.Where(x => x.ChungTuId == chungTuId);
                    }
                }
            }

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
        public async Task<IActionResult> Create([FromBody] ChiTietChungTuCreate model)
        {
            ChiTietChungTu c = new ChiTietChungTu()
            {
                MaTaiKhoan = model.MaTaiKhoan,
                DienGiai = model.DienGiai,
                SoTien = model.SoTien,
                LoaiGiaoDich = model.LoaiGiaoDich,
                ChungTuId = model.chungTuId,
            };
            var result = await _chitietChungTu.AddAsync(c);
            await _chitietChungTu.SaveChangesAsync();
            await CapNhatTongTienChungTu(model.chungTuId);
            
            var r = new ChiTietChungTuDto()
            {
                Id = result.Id,
                MaTaiKhoan = result.MaTaiKhoan,
                DienGiai = result.DienGiai,
                SoTien = result.SoTien,
                LoaiGiaoDich = result.LoaiGiaoDich,
                ChungTuId = result.ChungTuId
            };
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, r);
        }

        // PUT /api/chitietchungtu/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChiTietChungTuEdit model)
        {
            var existing = await _chitietChungTu.Query().FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return NotFound();

            existing.MaTaiKhoan = model.MaTaiKhoan;
            existing.DienGiai = model.DienGiai;
            existing.SoTien = model.SoTien;
            existing.LoaiGiaoDich = model.LoaiGiaoDich;

            var result = await _chitietChungTu.UpdateAsync(existing);
            await _chitietChungTu.SaveChangesAsync();

            await CapNhatTongTienChungTu(existing.ChungTuId);

            var chitietChungTuDto = new ChiTietChungTuDto()
            {
                Id = result.Id,
                MaTaiKhoan = result.MaTaiKhoan,
                DienGiai = result.DienGiai,
                SoTien = result.SoTien,
                LoaiGiaoDich = result.LoaiGiaoDich,
            };
            return Ok(chitietChungTuDto);
        }

        // DELETE /api/chitietchungtu/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _chitietChungTu.Query().FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            int chungTuId = item.ChungTuId;
            await _chitietChungTu.DeleteAsync(item);
            await _chitietChungTu.SaveChangesAsync();
            await CapNhatTongTienChungTu(chungTuId);
            // return NoContent();
            return Ok(new { id });
        }
        private async Task CapNhatTongTienChungTu(int chungTuId)
        {
            var chungtu = _chungTu.Query().FirstOrDefault(p => p.Id == chungTuId);
            if (chungtu != null)
            {
                chungtu.TongTien = _chitietChungTu.Query()
                    .Where(x => x.ChungTuId == chungTuId)
                    .Sum(x => x.SoTien);

                await _chungTu.UpdateAsync(chungtu);
                await _chungTu.SaveChangesAsync();
            }
        }
        // [HttpGet("by-chungtu/{chungTuId}")]
        // public async Task<IActionResult> GetByChungTu(int chungTuId)
        // {
        //     var result = await _chitietChungTu.Query().Where(x => x.ChungTuId == chungTuId).ToListAsync();
        //     return Ok(result);
        // }

        // [HttpGet("tong-hop-so-tien")]
        // public async Task<IActionResult> TongHopSoTien()
        // {
        //     var result = await _chitietChungTu.Query()
        //         .GroupBy(x => x.ChungTuId)
        //         .Select(g => new { ChungTuId = g.Key, TongTien = g.Sum(x => x.SoTien) })
        //         .ToListAsync();
        //     return Ok(result);
        // }
    }
}
