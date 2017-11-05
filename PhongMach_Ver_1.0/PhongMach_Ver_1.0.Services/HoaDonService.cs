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
    public interface IHoaDonService
    {
        HoaDonEntities GetByID(int id);

        List<HoaDonEntities> GetByBenhNhan(int id);
        List<HoaDonEntities> GetAll();
        List<HoaDonEntities> GetByDate(System.DateTime Date);

        int Create(HoaDonEntities Option);
        bool Update(HoaDonEntities Option);
        bool Delete(int id);
    }
    public class HoaDonService : IHoaDonService
    {
        private readonly UnitOfWork _unitOfWork;
        public HoaDonService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public int Create(HoaDonEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<HoaDonEntities, HoaDon>();
                var o = Mapper.Map<HoaDonEntities, HoaDon>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.HoaDonRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_HoaDon;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.HoaDonRepository.GetByID(id);
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

        public List<HoaDonEntities> GetAll()
        {
            var _listData = new List<HoaDonEntities>();
            var os = _unitOfWork.HoaDonRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (HoaDon item in os)
                {
                    Mapper.CreateMap<HoaDon, HoaDonEntities>();
                    var oModel = Mapper.Map<HoaDon, HoaDonEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public List<HoaDonEntities> GetByBenhNhan(int id)
        {
            var _ListHoaDon = new List<HoaDonEntities>();
            var _HoaDon = _unitOfWork.HoaDonRepository.GetAll().Where(x => x.ID_BenhNhan == id).ToList();
            if (_HoaDon.Any())
            {
                Mapper.CreateMap<HoaDon, HoaDonEntities>();
                _ListHoaDon = Mapper.Map<List<HoaDon>, List<HoaDonEntities>>(_HoaDon);
            }
            return _ListHoaDon;
        }

        public List<HoaDonEntities> GetByDate(DateTime Date)
        {
            var _ListHoaDon = new List<HoaDonEntities>();
            var _HoaDon = _unitOfWork.HoaDonRepository.GetAll().Where(x => x.NgayLap == Date).ToList();
            if (_HoaDon.Any())
            {
                Mapper.CreateMap<HoaDon, HoaDonEntities>();
                _ListHoaDon = Mapper.Map<List<HoaDon>, List<HoaDonEntities>>(_HoaDon);
            }
            return _ListHoaDon;
        }

        public HoaDonEntities GetByID(int id)
        {
            var o = _unitOfWork.HoaDonRepository.Get(x => x.ID_HoaDon == id);
            if (o != null)
            {
                Mapper.CreateMap<HoaDon, HoaDonEntities>();
                var oModel = Mapper.Map<HoaDon, HoaDonEntities>(o);
                return oModel;
            }
            return null;
        }

        public bool Update(HoaDonEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.HoaDonRepository.Get(x => x.ID_HoaDon == Option.ID_HoaDon);
                    if (o != null)
                    {
                        //o.ID_Thuoc = Option.ID_Thuoc;
                        //o.tenThuoc = Option.tenThuoc;
                        //o.loaiThuoc = Option.loaiThuoc;
                        //o.soLuongTon = Option.soLuongTon;
                        //o.GhiChu = Option.GhiChu;
                        //o.Date_Edited = DateTime.Now; ;
                        //o.IsDelete = Option.IsDelete;
                        Mapper.CreateMap<HoaDonEntities, HoaDon>();
                        o = Mapper.Map<HoaDonEntities, HoaDon>(Option);
                    }
                    _unitOfWork.HoaDonRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }
            return success;
        }
    }
}
