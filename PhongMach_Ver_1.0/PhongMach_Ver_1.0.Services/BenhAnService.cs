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
    public interface IBenhAnService
    {
        BenhAnEntities GetByID(int id);

        List<BenhAnEntities> GetAll();
        List<BenhAnEntities> GetByBenhNhan(int id_BenhNhan);

        int Create(BenhAnEntities Option);
        bool Update(BenhAnEntities Option);
        bool Delete(int id);
    }

    public class BenhAnService : IBenhAnService
    {
        private readonly UnitOfWork _unitOfWork;
        public BenhAnService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        public int Create(BenhAnEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<BenhAnEntities, BenhAn>();
                var o = Mapper.Map<BenhAnEntities, BenhAn>(Option);
                var bn= _unitOfWork.BenhNhanRepository.GetByID(o.ID_BenhNhan);
                bn.ID_LoaiKhachHang = 2;
                _unitOfWork.BenhNhanRepository.Update(bn);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.BenhAnRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_BenhAn;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.BenhAnRepository.GetByID(id);
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

        public List<BenhAnEntities> GetAll()
        {
            var _listData = new List<BenhAnEntities>();
            var os = _unitOfWork.BenhAnRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (BenhAn item in os)
                {
                    Mapper.CreateMap<BenhAn, BenhAnEntities>();
                    var oModel = Mapper.Map<BenhAn, BenhAnEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public List<BenhAnEntities> GetByBenhNhan(int id_BenhNhan)
        {
            var _listData = new List<BenhAnEntities>();
            var _benhan = _unitOfWork.BenhAnRepository.GetAll().Where(x => x.ID_BenhNhan == id_BenhNhan).ToList();
            if (_benhan.Any())
            {
                Mapper.CreateMap<BenhAn, BenhAnEntities>();
                _listData = Mapper.Map<List<BenhAn>, List<BenhAnEntities>>(_benhan);
            }
            return _listData;
        }

        public BenhAnEntities GetByID(int id)
        {
            var o = _unitOfWork.BenhAnRepository.Get(x => x.ID_BenhAn == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<BenhAn, BenhAnEntities>();
                var oModel = Mapper.Map<BenhAn, BenhAnEntities>(o);
                return oModel;
            }
            return null;
        }

        public bool Update(BenhAnEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.BenhAnRepository.Get(x => x.ID_BenhAn == Option.ID_BenhAn && x.IsDelete==false);
                    if (o != null)
                    {
                        Mapper.CreateMap<BenhAnEntities, BenhAn>();
                        o = Mapper.Map<BenhAnEntities, BenhAn>(Option);
                    }
                    _unitOfWork.BenhAnRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }

            }
            return success;
        }
    }
}
