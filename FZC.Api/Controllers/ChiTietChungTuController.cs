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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var topics = await _chitietChungTu.GetAllAsync();
            return Ok(topics);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChiTietChungTu model)
        {
            var result = await _chitietChungTu.AddAsync(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChiTietChungTu model)
        {
            model.Id = id;
            var result = await _chitietChungTu.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var chitietChungTu = _chitietChungTu.Query().FirstOrDefault(q=>q.Id ==id);
            if(chitietChungTu == null) return NotFound();
            else if(chitietChungTu!=null)
            {
                await _chitietChungTu.DeleteAsync(chitietChungTu);
            }
           
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
