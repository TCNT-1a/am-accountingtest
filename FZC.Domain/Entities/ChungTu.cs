using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZC.Domain.Entities
{
    public class ChungTu: BaseEntity
    {
        [Key]
        public int Id { get; set; } 
        public string SoChungTu { get; set; } = string.Empty;
        public DateTime NgayChungTu { get; set; }
        public string LoaiChungTu { get; set; } = string.Empty;
        public string DienGiai { get; set; } = string.Empty;
        public decimal TongTien { get; set; }
        public int ChiTietChungTuId;
        public virtual ICollection<ChiTietChungTu> ChiTietChungTu { get; set; }
    }
}
