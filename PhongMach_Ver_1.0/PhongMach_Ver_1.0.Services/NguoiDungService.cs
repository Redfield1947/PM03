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
    public interface INguoiDungService
    {
        NguoiDungEntities GetByAccount(String UserName);
        List<NguoiDungEntities> GetByRole(int id);
        List<NguoiDungEntities> GetAll();

        String Create(NguoiDungEntities Option);
        bool Update(NguoiDungEntities Option);
        bool Delete(String name);
    }
    public class NguoiDungService : INguoiDungService
    {
        private readonly UnitOfWork _unitOfWork;
        public NguoiDungService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        public String Create(NguoiDungEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<NguoiDungEntities, NguoiDung>();
                var o = Mapper.Map<NguoiDungEntities, NguoiDung>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.NguoiDungRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.username;
            }
        }

        public bool Delete(String name)
        {
            var success = false;
            if (!string.IsNullOrEmpty(name))
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.NguoiDungRepository.Get(x=>x.username==name);
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

        public List<NguoiDungEntities> GetAll()
        {
            var _listData = new List<NguoiDungEntities>();
            var os = _unitOfWork.NguoiDungRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (NguoiDung item in os)
                {
                    Mapper.CreateMap<NguoiDung, NguoiDungEntities>();
                    var oModel = Mapper.Map<NguoiDung, NguoiDungEntities>(item);
                    _listData.Add(oModel);
                }
            }
            return _listData;
        }

        public NguoiDungEntities GetByAccount(String UserName)
        {
            var o = _unitOfWork.NguoiDungRepository.Get(x => x.username==UserName);
            if(o != null)
            {
                Mapper.CreateMap<NguoiDung, NguoiDungEntities>();
                var oModel = Mapper.Map<NguoiDung, NguoiDungEntities>(o);
                return oModel;
            }
            return null;
        }

        public List<NguoiDungEntities> GetByRole(int id)
        {
            var o = _unitOfWork.NguoiDungRepository.Get(x => x.ID_role == id);
            var AccountLs = new List<NguoiDungEntities>();
            if(o!=null)
            {
                Mapper.CreateMap<NguoiDung, NguoiDungEntities>();
                var oModel = Mapper.Map<NguoiDung, NguoiDungEntities>(o);
                AccountLs.Add(oModel);
            }
            return AccountLs;
        }

        public bool Update(NguoiDungEntities Option)
        {
            var success = false;
            if (Option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.NguoiDungRepository.Get(x => x.username==Option.username);
                    if (o != null)
                    {
                        Mapper.CreateMap<NguoiDungEntities, NguoiDung>();
                        o = Mapper.Map<NguoiDungEntities, NguoiDung>(Option);
                    }
                    _unitOfWork.NguoiDungRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }

            }
            return success;
        }
    }
}
