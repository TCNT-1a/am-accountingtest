using FZC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FZC.Infrastructure.Data;
using FZC.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FZC.Infrastructure.Repositories
{

    public class ChungTuRepository : BaseRepository<ChungTu>, IChungTuRepository
    {
        public ChungTuRepository(ApplicationDbContext context) : base(context) { }

        
    }
}

