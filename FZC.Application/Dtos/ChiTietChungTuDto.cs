using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZC.Application.Dtos
{
    public class ChiTietChungTuDto
    {
        public int Id { get; set; }
        public string MaTaiKhoan { get; set; }
        public string DienGiai { get; set; }
        public decimal SoTien { get; set; }
        public string LoaiGiaoDich { get; set; }
        public int ChungTuId { get; set; }
    }
}
