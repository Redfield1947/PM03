using AutoMapper;
using PhongMach_Ver_1._0.Datamodel;
using PhongMach_Ver_1._0.Datamodel.UnitOfWork;
using PhongMach_Ver_1._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PhongMach_Ver_1._0.Services
{
    public interface IBenhNhanService
    {
        BenhNhanEntities GetByID(int id);

        List<BenhNhanEntities> GetAll();
        List<BenhNhanEntities> GetByLoaiKhachHang(int id_KhachHang);      

        int Create(BenhNhanEntities Option);
        int CreateTemp(String Name);
        bool Update(BenhNhanEntities Option);
        bool Delete(int id);
    }
    public class BenhNhanService : IBenhNhanService
    {
        private readonly UnitOfWork _unitOfWork;
        public BenhNhanService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int Create(BenhNhanEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<BenhNhanEntities, BenhNhan>();
                var o = Mapper.Map<BenhNhanEntities, BenhNhan>(Option);
                o.ID_LoaiKhachHang = 1;
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.BenhNhanRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_BenhNhan;
            }
        }

        public int CreateTemp(String Option)
        {
            using (var scope = new TransactionScope())
            {
                var o = new BenhNhan();
                o.ten_BenhNhan = Option;
                o.gioi_Tinh = " ";
                o.ID_LoaiKhachHang = 1;
                o.ngaySinh = DateTime.Now;
                o.SDT = " ";
                o.diaChi = " ";
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.BenhNhanRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_BenhNhan;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.BenhNhanRepository.GetByID(id);
                    if (o != null)
                    {
                        o.IsDelete = true;
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public List<BenhNhanEntities> GetAll()
        {
            var _listData = new List<BenhNhanEntities>();
            var os = _unitOfWork.BenhNhanRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (BenhNhan item in os)
                {
                    Mapper.CreateMap<BenhNhan, BenhNhanEntities>();
                    var oModel = Mapper.Map<BenhNhan, BenhNhanEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public BenhNhanEntities GetByID(int id)
        {
            var o = _unitOfWork.BenhNhanRepository.Get(x => x.ID_BenhNhan == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<BenhNhan, BenhNhanEntities>();
                var oModel = Mapper.Map<BenhNhan, BenhNhanEntities>(o);
                return oModel;
            }
            return null;
        }

        public List<BenhNhanEntities> GetByLoaiKhachHang(int id_KhachHang)
        {
            var _Listkhachhang = new List<BenhNhanEntities>();
            var _benhnhan = _unitOfWork.BenhNhanRepository.GetAll().Where(x => x.ID_BenhNhan == id_KhachHang).ToList();
            if (_benhnhan.Any())
            {
                Mapper.CreateMap<BenhNhan, BenhNhanEntities>();
                _Listkhachhang = Mapper.Map<List<BenhNhan>, List<BenhNhanEntities>>(_benhnhan);
            }
            return _Listkhachhang;
        }

        public List<BenhNhanEntities> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool Update(BenhNhanEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.BenhNhanRepository.Get(x => x.ID_BenhNhan == Option.ID_BenhNhan);
                    if (o != null)
                    {
                        o.ID_BenhNhan = Option.ID_BenhNhan;
                        o.gioi_Tinh = Option.gioi_Tinh;
                        o.ID_LoaiKhachHang = Option.ID_LoaiKhachHang;
                        o.ngaySinh = Option.ngaySinh;
                        o.SDT = Option.SDT;
                        o.ten_BenhNhan = Option.ten_BenhNhan;
                        o.diaChi = Option.diaChi;
                        o.IsDelete = Option.IsDelete;
                        
                    }
                    _unitOfWork.BenhNhanRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }
            return success;
        }
    }
}
