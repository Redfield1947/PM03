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
    public interface ICTDTService
    {
        ChiTietDonThuocEntities getById(int id);

        List<ChiTietDonThuocEntities> getAll();
        List<ChiTietDonThuocEntities> getByIdDonThuoc(int id);

        int Create(ChiTietDonThuocEntities Option);
        bool Update(ChiTietDonThuocEntities Option);
        bool Delete(int id);
    }
    public class CTDTService : ICTDTService
    {
        private readonly UnitOfWork _unitOfWork;
        public CTDTService()
        {
            this._unitOfWork = new UnitOfWork();
        }

        public int Create(ChiTietDonThuocEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<ChiTietDonThuocEntities, ChiTietDonThuoc>();
                var o = Mapper.Map<ChiTietDonThuocEntities, ChiTietDonThuoc>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.ChiTietDonThuocRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_Thuoc;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.ChiTietDonThuocRepository.GetByID(id);
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

        public List<ChiTietDonThuocEntities> getAll()
        {
            var _listData = new List<ChiTietDonThuocEntities>();
            var os = _unitOfWork.ChiTietDonThuocRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (ChiTietDonThuoc item in os)
                {
                    Mapper.CreateMap<ChiTietDonThuoc, ChiTietDonThuocEntities>();
                    var oModel = Mapper.Map<ChiTietDonThuoc, ChiTietDonThuocEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public ChiTietDonThuocEntities getById(int id)
        {
            var o = _unitOfWork.ChiTietDonThuocRepository.Get(x => x.ID_Thuoc == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<ChiTietDonThuoc, ChiTietDonThuocEntities>();
                var oModel = Mapper.Map<ChiTietDonThuoc, ChiTietDonThuocEntities>(o);
                return oModel;
            }
            return null;
        }

        public List<ChiTietDonThuocEntities> getByIdDonThuoc(int id)
        {
            var _listData = new List<ChiTietDonThuocEntities>();
            var o = _unitOfWork.ChiTietDonThuocRepository.Get(x => x.ID_DonThuoc == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<ChiTietDonThuoc, ChiTietDonThuocEntities>();
                var oModel = Mapper.Map<ChiTietDonThuoc, ChiTietDonThuocEntities>(o);
                _listData.Add(oModel);
            }
            return _listData;
        }

        public bool Update(ChiTietDonThuocEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.ChiTietDonThuocRepository.Get(x => x.ID_DonThuoc == Option.ID_DonThuoc && x.ID_Thuoc == Option.ID_Thuoc);
                    if (o != null)
                    {
                        Mapper.CreateMap<ChiTietDonThuocEntities, ChiTietDonThuoc>();
                        o = Mapper.Map<ChiTietDonThuocEntities, ChiTietDonThuoc>(Option);
                    }
                    _unitOfWork.ChiTietDonThuocRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }

            }
            return success;
        }
    }
}
