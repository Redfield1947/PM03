using PhongMach_Ver_1._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMach_Ver_1._0.Models.Models
{
    public class ThuocModel : ThuocEntities
    {
        public String TenLoaiThuoc { get; set; }
        public DateTime NgayKham { get; set; }

    }
}
