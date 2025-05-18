using FZC.Application.Dtos;
using FZC.Domain.Entities;
using FZC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using FZC.Application.Services;

namespace FZC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChungTuController : ControllerBase
    {
        public readonly IChungTuRepository _chungTu;
        private readonly IChiTietChungTuRepository _chitietChungTu;
        
        public ChungTuController(IChungTuRepository chungTuRepository, IChiTietChungTuRepository chitietChungTu)
        {
            _chungTu = chungTuRepository;
            _chitietChungTu = chitietChungTu;
        }
        // GET /api/chungtu?filter={}&sort={}&range={}
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filter, [FromQuery] string? sort, [FromQuery] string? range)
        {
            var query = _chungTu.Query();

            // React-admin expects pagination and filtering, you can parse filter/range here if needed
            // For demo, just return all
            // Parse filter from react-admin (expects JSON string)
            if (!string.IsNullOrEmpty(filter))
            {
                var filterObj = JsonSerializer.Deserialize<Dictionary<string, object>>(filter);
                if (filterObj != null)
                {

                    var soChungTu = FilterHelper.GetStringFromFilter(filterObj, "soChungTu");
                    if (!string.IsNullOrEmpty(soChungTu))
                        query = query.Where(x => x.SoChungTu.Contains(soChungTu));

                    var loaiChungTu = FilterHelper.GetStringFromFilter(filterObj, "loaiChungTu");
                    if (!string.IsNullOrEmpty(loaiChungTu))
                        query = query.Where(x => x.LoaiChungTu == loaiChungTu);

                    var ngayChungTu = FilterHelper.GetDateFromFilter(filterObj, "ngayChungTu");
                    if (ngayChungTu.HasValue)
                        query = query.Where(x => x.NgayChungTu.Date == ngayChungTu.Value.Date);
                }
            }
        

            // Parse range for pagination: range=[start, end]
            int skip = 0, take = 100;
            if (!string.IsNullOrEmpty(range))
            {
                try
                {
                    var rangeArr = JsonSerializer.Deserialize<int[]>(range);
                    if (rangeArr != null && rangeArr.Length == 2)
                    {
                        skip = rangeArr[0];
                        take = rangeArr[1] - rangeArr[0] + 1;
                    }
                }
                catch { }
            }

            var total = await query.CountAsync();
            var data = await query.Skip(skip).Take(take).ToListAsync();

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Add("Content-Range", $"chungtu 0-{data.Count - 1}/{total}");

            return Ok(data);
        }

        // GET /api/chungtu/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chungTu = await _chungTu.Query()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (chungTu == null)
                return NotFound();
            return Ok(chungTu);
        }

        // POST /api/chungtu
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChungTuCreate model)
        {

            ChungTu chungTu = new ChungTu()
            {
                SoChungTu = model.SoChungTu,
                NgayChungTu = model.NgayChungTu,
                LoaiChungTu = model.LoaiChungTu,
                DienGiai = model.DienGiai
            };
            var result = await _chungTu.AddAsync(chungTu);
            await _chungTu.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT /api/chungtu/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChungTuEdit model)
        {
            var existing = await _chungTu.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            model.Id = id;
            existing.SoChungTu = model.SoChungTu;
            existing.NgayChungTu = model.NgayChungTu;
            existing.LoaiChungTu = model.LoaiChungTu;
            existing.DienGiai = model.DienGiai;
            existing.TongTien = model.TongTien;

            var result = await _chungTu.UpdateAsync(existing);
            await _chungTu.SaveChangesAsync();
            return Ok(result);
        }

        // DELETE /api/chungtu/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var chungTu = await _chungTu.GetByIdAsync(id);
            if (chungTu == null)
                return NotFound();
            await _chungTu.DeleteAsync(chungTu);
            await _chungTu.SaveChangesAsync();
            var chitietchungtuList = _chitietChungTu.Query().Where(p => p.ChungTuId == id);

            foreach (var ct in chitietchungtuList)
            {
                ct.isDeleted = true;
                ct.updateDate = DateTime.UtcNow;
                await _chitietChungTu.UpdateAsync(ct);
            }
            await _chitietChungTu.SaveChangesAsync();
            return Ok(new { id });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? soChungTu, string? loaiChungTu, DateTime? ngayChungTu)
        {
            var query = _chungTu.Query();
            if (!string.IsNullOrEmpty(soChungTu))
                query = query.Where(x => x.SoChungTu.Contains(soChungTu));
            if (!string.IsNullOrEmpty(loaiChungTu))
                query = query.Where(x => x.LoaiChungTu == loaiChungTu);
            if (ngayChungTu.HasValue)
                query = query.Where(x => x.NgayChungTu.Date == ngayChungTu.Value.Date);

            var result = await query.ToListAsync();
            return Ok(result);
        }
    }
}
