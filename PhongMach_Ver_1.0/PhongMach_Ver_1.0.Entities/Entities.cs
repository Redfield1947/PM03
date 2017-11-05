using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMach_Ver_1._0.Entities
{
    public class DinhNghiaGiaTriEntities
    {
        public int ID_DinhNghiaGiaTri { get; set; }
        public string tenDinhNghia { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
    public class GiaTriMacDinhEntities
    {
        public int ID_GiaTri { get; set; }
        public string GiaTri { get; set; }
        public int ID_DinhNghiaGiaTri { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
    public class ThuocEntities
    {
        public int ID_Thuoc { get; set; }
        public string tenThuoc { get; set; }
        public int loaiThuoc { get; set; }
        public decimal soLuongTon { get; set; }
        public decimal donGia { get; set; }
        public string GhiChu { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
    public class NguoiDungEntities
    {
        public string username { get; set; }
        public string pass { get; set; }
        public int ID_role { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
    public class BenhNhanEntities
    {
        public int ID_BenhNhan { get; set; }
        public string ten_BenhNhan { get; set; }
        public string gioi_Tinh { get; set; }
        public System.DateTime ngaySinh { get; set; }
        public string diaChi { get; set; }
        public string SDT { get; set; }
        public int ID_LoaiKhachHang { get; set; } //Để lưu làm khách hàng tiềm năng hoặc khách hàng chính thức, những thằng gọi điện để đặt lịch khám thì default vẫn tạo một bệnh nhân ( khách hàng mới ) nhưng mặc định nó sẽ là khách hàng tiềm năng, còn khi nào khám rồi thì mới là khách hàng chính thức
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
    }
    public class BenhAnEntities
    {
        public int ID_BenhAn { get; set; }
        public int ID_BenhNhan { get; set; }
        public string TrieuChung { get; set; }
        public string ChuanDoan { get; set; }
        public int ID_donThuoc { get; set; }
        public System.DateTime ngayKham { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
    }
    public class DonThuocEntities
    {
        public int ID_donthuoc { get; set; }
        public System.DateTime ngayCapThuoc { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
    public class ChiTietDonThuocEntities
    {
        public int ID_Thuoc { get; set; }
        public int ID_DonThuoc { get; set; }
        public decimal SoLuong { get; set; }
        public string Ghi_Chu { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
    public class HoaDonEntities
    {
        public int ID_HoaDon { get; set; }
        public int ID_BenhNhan { get; set; }
        public int ID_LichKham { get; set; }
        public System.DateTime NgayLap { get; set; }
        public decimal GiaTriHoaDon { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
    public class CTHDEntities
    {
        public int ID_HoaDon { get; set; }
        public int ID_Thuoc { get; set; }
        public int SoLuong { get; set; }
        public decimal tongCong { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
    }
    public class LichKhamEntities
    {
        public int ID_LichKham { get; set; }
        public int ID_KhachHang { get; set; }
        public System.DateTime NgayKham { get; set; }
        public int ID_PhiKhamBenh { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> IsComplete { get; set; } //kiểm tra xem khách hàng đã đi khám chưa
        public bool IsDelete { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
    }
}
