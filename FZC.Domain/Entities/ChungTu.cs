using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZC.Domain.Entities
{
    [Table("ChungTu")]
    public class ChungTu: BaseEntity
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        [MaxLength(50)]
        public string SoChungTu { get; set; } = string.Empty;
        public DateTime NgayChungTu { get; set; }
        [Required]
        [MaxLength(50)]
        public string LoaiChungTu { get; set; } = string.Empty;
        [MaxLength(255)]
        public string DienGiai { get; set; } = string.Empty;
        public decimal TongTien { get; set; }
        
        public virtual ICollection<ChiTietChungTu> ChiTietChungTus { get; set; }
    }
}
