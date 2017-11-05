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
    public interface IGiaTriMacDinhService
    {
        GiaTriMacDinhEntities getByID(int id);
        List<GiaTriMacDinhEntities> GetAll();
        List<GiaTriMacDinhEntities> GetByDinhNghia(int id_DinhNghia);

        int Create(GiaTriMacDinhEntities Option);
        bool Update(GiaTriMacDinhEntities Option);
        bool Delete(int id);
    }
    public class GiaTriMacDinhService : IGiaTriMacDinhService
    {
        private readonly UnitOfWork _unitOfWork;
        public GiaTriMacDinhService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public int Create(GiaTriMacDinhEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<GiaTriMacDinhEntities, GiaTriMacDinh>();
                var o = Mapper.Map<GiaTriMacDinhEntities, GiaTriMacDinh>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.GiaTriMacDinhRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_GiaTri;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.GiaTriMacDinhRepository.GetByID(id);
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

        public List<GiaTriMacDinhEntities> GetAll()
        {
            var _listData = new List<GiaTriMacDinhEntities>();
            var os = _unitOfWork.GiaTriMacDinhRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (GiaTriMacDinh item in os)
                {
                    Mapper.CreateMap<GiaTriMacDinh, GiaTriMacDinhEntities>();
                    var oModel = Mapper.Map<GiaTriMacDinh, GiaTriMacDinhEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public List<GiaTriMacDinhEntities> GetByDinhNghia(int id_DinhNghia)
        {
            var _ListGiaTriMacDinh = new List<GiaTriMacDinhEntities>();
            var _GiaTriMacDinh = _unitOfWork.GiaTriMacDinhRepository.GetAll().Where(x => x.ID_DinhNghiaGiaTri == id_DinhNghia).ToList();
            if (_GiaTriMacDinh.Any())
            {
                Mapper.CreateMap<GiaTriMacDinh, GiaTriMacDinhEntities>();
                _ListGiaTriMacDinh = Mapper.Map<List<GiaTriMacDinh>, List<GiaTriMacDinhEntities>>(_GiaTriMacDinh);
            }
            return _ListGiaTriMacDinh;
        }

        public GiaTriMacDinhEntities getByID(int id)
        {
            var o = _unitOfWork.GiaTriMacDinhRepository.Get(x => x.ID_GiaTri == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<GiaTriMacDinh, GiaTriMacDinhEntities>();
                var oModel = Mapper.Map<GiaTriMacDinh, GiaTriMacDinhEntities>(o);
                return oModel;
            }
            return null;
        }

        public bool Update(GiaTriMacDinhEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.GiaTriMacDinhRepository.Get(x => x.ID_GiaTri == Option.ID_GiaTri);
                    if (o != null)
                    {
                        o.ID_GiaTri = Option.ID_GiaTri;
                        o.GiaTri = Option.GiaTri;
                        o.ID_DinhNghiaGiaTri = Option.ID_DinhNghiaGiaTri;
                        o.Date_Edited = DateTime.Now; ;
                        o.IsDelete = Option.IsDelete;
                    }
                    _unitOfWork.GiaTriMacDinhRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }
            return success;
        }
    }
}

