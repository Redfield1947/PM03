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
    public interface IDInhNghiaGiaTriService
    {
        DinhNghiaGiaTriEntities getByID(int id);
        List<DinhNghiaGiaTriEntities> GetAll();

        int Create(DinhNghiaGiaTriEntities Option);
        bool Update(DinhNghiaGiaTriEntities Option);
        bool Delete(int id);
    }
    public class DinhNghiaGiaTriService : IDInhNghiaGiaTriService
    {
        private readonly UnitOfWork _unitOfWork;
        public DinhNghiaGiaTriService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public int Create(DinhNghiaGiaTriEntities Option)
        {
            using (var scope = new TransactionScope())
            {

                Mapper.CreateMap<DinhNghiaGiaTriEntities, DinhNghiaGiaTri>();
                var o = Mapper.Map<DinhNghiaGiaTriEntities, DinhNghiaGiaTri>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.DinhNghiaGiaTriRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_DinhNghiaGiaTri;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.DinhNghiaGiaTriRepository.GetByID(id);
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

        public List<DinhNghiaGiaTriEntities> GetAll()
        {
            var _listData = new List<DinhNghiaGiaTriEntities>();
            var os = _unitOfWork.DinhNghiaGiaTriRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (DinhNghiaGiaTri item in os)
                {
                    Mapper.CreateMap<DinhNghiaGiaTri, DinhNghiaGiaTriEntities>();
                    var oModel = Mapper.Map<DinhNghiaGiaTri, DinhNghiaGiaTriEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public DinhNghiaGiaTriEntities getByID(int id)
        {
            var o = _unitOfWork.DinhNghiaGiaTriRepository.Get(x => x.ID_DinhNghiaGiaTri == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<DinhNghiaGiaTri, DinhNghiaGiaTriEntities>();
                var oModel = Mapper.Map<DinhNghiaGiaTri, DinhNghiaGiaTriEntities>(o);
                return oModel;
            }
            return null;
        }

        public bool Update(DinhNghiaGiaTriEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.DinhNghiaGiaTriRepository.Get(x => x.ID_DinhNghiaGiaTri == Option.ID_DinhNghiaGiaTri);
                    if (o != null)
                    {
                        o.ID_DinhNghiaGiaTri = Option.ID_DinhNghiaGiaTri;
                        o.tenDinhNghia = Option.tenDinhNghia;
                        o.GhiChu = Option.GhiChu;
                        o.Date_Edited = DateTime.Now; ;
                        o.IsDelete = Option.IsDelete;
                    }
                    _unitOfWork.DinhNghiaGiaTriRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }
            return success;
        }
    }
}
