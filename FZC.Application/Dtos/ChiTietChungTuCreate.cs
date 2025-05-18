using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZC.Application.Dtos
{
    public class ChiTietChungTuCreate
    {
        public string MaTaiKhoan { get; set; }
        public string DienGiai { get; set; }
        [Range(0, double.MaxValue)]
        public decimal SoTien { get; set; }
        public string LoaiGiaoDich {get;set;}

        public int chungTuId { get; set; }
    }
}
