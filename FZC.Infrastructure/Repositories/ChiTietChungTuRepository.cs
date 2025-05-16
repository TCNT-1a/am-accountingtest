using FZC.Domain.Entities;
using FZC.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZC.Infrastructure.Repositories
{
    public class ChiTietChungTuRepository : BaseRepository<ChiTietChungTu>, IChiTietChungTuRepository
    {
        public ChiTietChungTuRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
