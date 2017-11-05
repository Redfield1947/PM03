using PhongMach_Ver_1._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMach_Ver_1._0.Models.Models
{
    public class HoaDonModel : HoaDonEntities
    {
        public String TenBenhNhan { get; set; }
        public System.DateTime NgayKham { get; set; }
        public decimal TienThuoc { get; set; }
        public decimal TienKhamBenh { get; set; }
    }
}
