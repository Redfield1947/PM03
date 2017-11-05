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
    public interface ILichKhamService
    {
        LichKhamEntities GetByID(int id);

        List<LichKhamEntities> GetAll();
        List<LichKhamEntities> GetByKhachHang(int id_KhachHang);

        int Create(LichKhamEntities Option);
        bool Update(LichKhamEntities Option);
        bool Delete(int id);
    }
    public class LichKhamService : ILichKhamService
    {
        private readonly UnitOfWork _unitOfWork;
        public LichKhamService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public int Create(LichKhamEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<LichKhamEntities, LichKham>();
                var o = Mapper.Map<LichKhamEntities, LichKham>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.LichKhamRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_LichKham;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.LichKhamRepository.GetByID(id);
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

        public List<LichKhamEntities> GetAll()
        {
            var _listData = new List<LichKhamEntities>();
            var os = _unitOfWork.LichKhamRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (LichKham item in os)
                {
                    Mapper.CreateMap<LichKham, LichKhamEntities>();
                    var oModel = Mapper.Map<LichKham, LichKhamEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public LichKhamEntities GetByID(int id)
        {
            var o = _unitOfWork.LichKhamRepository.Get(x => x.ID_LichKham == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<LichKham, LichKhamEntities>();
                var oModel = Mapper.Map<LichKham, LichKhamEntities>(o);
                return oModel;
            }
            return null;
        }

        public List<LichKhamEntities> GetByKhachHang(int id_KhachHang)
        {
            var _Listlichkham = new List<LichKhamEntities>();
            var _lichkham = _unitOfWork.LichKhamRepository.GetAll().Where(x => x.ID_KhachHang == id_KhachHang).ToList();
            if (_lichkham.Any())
            {
                Mapper.CreateMap<LichKham, LichKhamEntities>();
                _Listlichkham = Mapper.Map<List<LichKham>, List<LichKhamEntities>>(_lichkham);
            }
            return _Listlichkham;
        }

        public bool Update(LichKhamEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.LichKhamRepository.Get(x => x.ID_KhachHang == Option.ID_KhachHang);
                    if (o != null)
                    {
                        o.ID_KhachHang = Option.ID_KhachHang;
                        o.ID_LichKham = Option.ID_LichKham;
                        o.ID_PhiKhamBenh = Option.ID_PhiKhamBenh;
                        o.NgayKham = Option.NgayKham;
                        o.IsComplete = Option.IsComplete;
                        o.GhiChu = Option.GhiChu;
                        o.Date_Edited = DateTime.Now; ;
                        o.IsDelete = Option.IsDelete;
                        o.Date_Created = Option.Date_Created;
                    }
                    _unitOfWork.LichKhamRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }
            return success;
        }
    }
}    

