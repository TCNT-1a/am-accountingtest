using FZC.Domain.Entities;
using FZC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FZC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietChungTuController : ControllerBase
    {
        private readonly IChiTietChungTuRepository _chitietChungTu;
        public ChiTietChungTuController(IChiTietChungTuRepository chitietChungTu) {
            this._chitietChungTu = chitietChungTu;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var topics = await _chitietChungTu.GetAllAsync();
            return Ok(topics);
        }
    }
}
