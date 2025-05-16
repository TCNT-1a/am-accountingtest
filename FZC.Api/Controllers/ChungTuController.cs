using FZC.Domain.Entities;
using FZC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FZC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChungTuController : ControllerBase
    {
        public readonly IChungTuRepository _chungTuRepository;
        public ChungTuController(IChungTuRepository chungTuRepository)
        {
            _chungTuRepository = chungTuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var chungTus = await _chungTuRepository.Query().Include(x => x.ChiTietChungTus).ToListAsync();
            return Ok(chungTus);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChungTu model)
        {
            var result = await _chungTuRepository.AddAsync(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChungTu model)
        {
            model.Id = id;
            var result = await _chungTuRepository.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var chungTu = _chungTuRepository.Query().FirstOrDefault(x => x.Id == id);
            if (chungTu == null)
                return NotFound();
            await _chungTuRepository.DeleteAsync(chungTu);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? soChungTu, string? loaiChungTu, DateTime? ngayChungTu)
        {
            var query = _chungTuRepository.Query();
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
