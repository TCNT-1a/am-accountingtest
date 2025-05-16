using Microsoft.EntityFrameworkCore;
using FZC.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
namespace FZC.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ChungTu> ChungTus { get; set; }
        public DbSet<ChiTietChungTu> ChiTietChungTus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<ChungTu> chungTus = new List<ChungTu>()
            {
                new ChungTu
                {
                    Id = 1,
                    SoChungTu = "ABC123",
                    NgayChungTu = DateTime.Now,
                    LoaiChungTu = "Chung Tu A",
                    DienGiai = "Demo chứng từ A",
                    TongTien = 100000
                },
                new ChungTu
                {
                    Id = 2,
                    SoChungTu = "XYZ456",
                    NgayChungTu = DateTime.Now.AddDays(-1),
                    LoaiChungTu = "Chung Tu B",
                    DienGiai = "Demo chứng từ B",
                    TongTien = 200000
                }
            };

            List<ChiTietChungTu> chiTietChungTus = new List<ChiTietChungTu>()
            {
                new ChiTietChungTu
                {
                    Id = 1,
                    ChungTuId = 1,
                    DienGiai = "Chi tiết 1 cho chứng từ 1",
                    SoTien = 50000
                },
                new ChiTietChungTu
                {
                    Id = 2,
                    ChungTuId = 1,
                    DienGiai = "Chi tiết 2 cho chứng từ 1",
                    SoTien = 50000
                },
                new ChiTietChungTu
                {
                    Id = 3,
                    ChungTuId = 2,
                    DienGiai = "Chi tiết 1 cho chứng từ 2",
                    SoTien = 200000
                }
            };

            modelBuilder.Entity<ChungTu>().HasData(chungTus);
            modelBuilder.Entity<ChiTietChungTu>().HasData(chiTietChungTus);
        }

    }
}