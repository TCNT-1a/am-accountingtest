using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZC.Application.Dtos
{
    public class ChungTuCreate
    {
        public string SoChungTu { get; set; } = string.Empty;
        public DateTime NgayChungTu { get; set; }
        [Required]
        [MaxLength(50)]
        public string LoaiChungTu { get; set; } = string.Empty;
        [MaxLength(255)]
        public string DienGiai { get; set; } = string.Empty;
    }
}
