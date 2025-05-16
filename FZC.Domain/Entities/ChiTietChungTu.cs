using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZC.Domain.Entities
{
    public class ChiTietChungTu: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string MaTaiKhoan { get; set; } = string.Empty;
        public string DienGiai { get; set; } = string.Empty;
        public decimal SoTien {  get; set; }    
        public string LoaiGiaoDich {  get; set; } = string.Empty;
        
        public int ChungTuId;
        public virtual ChungTu ChungTu { get; set; }
    }
}
