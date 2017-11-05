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
    public interface IDonThuocService
    {
        List<DonThuocEntities> Getall();
        List<DonThuocEntities> GetByIDDonThuoc(int id);

        DonThuocEntities GetByID(int id);

        int Create(DonThuocEntities Option);
        int CreateTemp();
        bool Update(DonThuocEntities Option);
        bool Delete(int id);
    }
    public class DonThuocService : IDonThuocService
    {
        private readonly UnitOfWork _unitOfWork;
        public DonThuocService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public int Create(DonThuocEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<DonThuocEntities, DonThuoc>();
                var o = Mapper.Map<DonThuocEntities, DonThuoc>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.DonThuocRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_donthuoc;
            }
        }

        public bool Delete(int id)
        {
            var success = false;
            if (id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.DonThuocRepository.GetByID(id);
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

        public List<DonThuocEntities> Getall()
        {
            var _listData = new List<DonThuocEntities>();
            var os = _unitOfWork.DonThuocRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (DonThuoc item in os)
                {
                    Mapper.CreateMap<DonThuoc, DonThuocEntities>();
                    var oModel = Mapper.Map<DonThuoc, DonThuocEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public DonThuocEntities GetByID(int id)
        {
            var o = _unitOfWork.DonThuocRepository.Get(x => x.ID_donthuoc == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<DonThuoc, DonThuocEntities>();
                var oModel = Mapper.Map<DonThuoc, DonThuocEntities>(o);
                return oModel;
            }
            return null;
        }

        public List<DonThuocEntities> GetByIDDonThuoc(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(DonThuocEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.DonThuocRepository.Get(x => x.ID_donthuoc == Option.ID_donthuoc);
                    if (o != null)
                    {
                        //o.ID_donDonThuoc = Option.ID_donDonThuoc;
                        //o.tenDonThuoc = Option.tenDonThuoc;
                        //o.loaiDonThuoc = Option.loaiDonThuoc;
                        //o.soLuongTon = Option.soLuongTon;
                        //o.GhiChu = Option.GhiChu;
                        o.ID_donthuoc = Option.ID_donthuoc;
                        o.ngayCapThuoc = Option.ngayCapThuoc;
                        o.Date_Edited = DateTime.Now; ;
                        o.IsDelete = Option.IsDelete;
                    }
                    _unitOfWork.DonThuocRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }
            return success;
        }
        public int CreateTemp()
        {
            using (var scope = new TransactionScope())
            {
                var o = new DonThuoc();
                o.ngayCapThuoc = DateTime.Now;
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.DonThuocRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_donthuoc;
            }
        }
    }
}
