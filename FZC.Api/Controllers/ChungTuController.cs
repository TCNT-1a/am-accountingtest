using FZC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FZC.Api.Controllers
{
    public class ChungTuController : ControllerBase
    {
        public readonly IChungTuRepository _chungTuRepository;
        public ChungTuController(IChungTuRepository chungTuRepository) {
            _chungTuRepository = chungTuRepository;
        }
        public async Task<IActionResult> Index()
        {
            var chungTus = await _chungTuRepository.Query().Include(x => x.ChiTietChungTu).ToListAsync();
            return Ok(chungTus);
        }
    }
}
