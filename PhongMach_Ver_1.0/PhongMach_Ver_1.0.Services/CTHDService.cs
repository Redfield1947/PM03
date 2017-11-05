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
    public interface ICTHDService
    {
        CTHDEntities getByID(int ID_Thuoc,int ID_HoaDon);
        List<CTHDEntities> GetAll();
        CTHD Create(CTHDEntities Option);
        bool Update(CTHDEntities Option);
        bool Delete(int id_HD,int id_T);
    }
    public class CTHDService : ICTHDService
    {
        private readonly UnitOfWork _unitOfWork;
        public CTHDService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        public CTHD Create(CTHDEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<CTHDEntities, CTHD>();
                var o = Mapper.Map<CTHDEntities, CTHD>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.CTHDRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o;
            }
        }

        public bool Delete(int ID_HD, int ID_T)
        {
            var success = false;
            if (ID_HD > 0 && ID_T>0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.CTHDRepository.Get(x=>x.ID_HoaDon==ID_HD && x.ID_Thuoc==ID_T);
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

        public List<CTHDEntities> GetAll()
        {
            var _listData = new List<CTHDEntities>();
            var os = _unitOfWork.CTHDRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (CTHD item in os)
                {
                    Mapper.CreateMap<CTHD, CTHDEntities>();
                    var oModel = Mapper.Map<CTHD, CTHDEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public CTHDEntities getByID(int ID_Thuoc,int ID_HoaDon)
        {
            var o = _unitOfWork.CTHDRepository.Get(x => x.ID_HoaDon==ID_HoaDon && x.IsDelete == false && x.ID_Thuoc==ID_Thuoc);
            if (o != null)
            {
                Mapper.CreateMap<CTHD, CTHDEntities>();
                var oModel = Mapper.Map<CTHD, CTHDEntities>(o);
                return oModel;
            }
            return null;
        }

        public bool Update(CTHDEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.CTHDRepository.Get(x =>x.ID_HoaDon== Option.ID_HoaDon && x.ID_Thuoc==Option.ID_Thuoc);
                    if (o != null)
                    {
                        Mapper.CreateMap<CTHDEntities, CTHD>();
                        o = Mapper.Map<CTHDEntities, CTHD>(Option);
                    }
                    _unitOfWork.CTHDRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }

            }
            return success;
        }
    }
}
