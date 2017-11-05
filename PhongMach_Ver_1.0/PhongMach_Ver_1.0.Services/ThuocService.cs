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
    public interface IThuocService
    {
        ThuocEntities GetByID(int id);
 
        List<ThuocEntities> GetAll();
        List<ThuocEntities> GetByLoaiThuoc(int id_LoaiThuoc);
        ThuocEntities GetByName(String name);

        int Create(ThuocEntities Option);
        int CreateTemp(String name);
        bool Update(ThuocEntities Option);
        bool Delete(int id);
    }
    public class _thuocService : IThuocService
    {
        private readonly UnitOfWork _unitOfWork;
        public _thuocService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public int Create(ThuocEntities Option)
        {
            using (var scope = new TransactionScope())
            {
                Mapper.CreateMap<ThuocEntities, Thuoc>();
                var o = Mapper.Map<ThuocEntities, Thuoc>(Option);
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.ThuocRepository.Insert(o);
                _unitOfWork.Save();
                scope.Complete();
                return o.ID_Thuoc;
            }
        }
        public int CreateTemp(string name)
        {
            using (var scope = new TransactionScope())
            {
                //Mapper.CreateMap<ThuocEntities, Thuoc>();
                //var o = Mapper.Map<ThuocEntities, Thuoc>(name);
                Thuoc o = new Thuoc();
                o.donGia = 0;
                o.loaiThuoc = -1;
                o.soLuongTon = 0;
                o.tenThuoc = name;
                o.GhiChu = "Thuốc không nằm trong kho";
                o.IsDelete = false;
                o.Date_Created = DateTime.Now;
                _unitOfWork.ThuocRepository.Insert(o);
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
                    var o = _unitOfWork.ThuocRepository.GetByID(id);
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

        public List<ThuocEntities> GetAll()
        {
            var _listData = new List<ThuocEntities>();
            var os = _unitOfWork.ThuocRepository.GetAll().Where(x => x.IsDelete == false).ToList();

            if (os.Any())
            {
                foreach (Thuoc item in os)
                {
                    Mapper.CreateMap<Thuoc, ThuocEntities>();
                    var oModel = Mapper.Map<Thuoc, ThuocEntities>(item);
                    _listData.Add(oModel);
                }               
            }
            return _listData;
        }

        public ThuocEntities GetByID(int id)
        {
            var o = _unitOfWork.ThuocRepository.Get(x => x.ID_Thuoc == id && x.IsDelete == false);
            if (o != null)
            {
                Mapper.CreateMap<Thuoc, ThuocEntities>();
                var oModel = Mapper.Map<Thuoc,ThuocEntities>(o);
                return oModel;
            }
            return null;
        }

        public List<ThuocEntities> GetByLoaiThuoc(int id_LoaiThuoc)
        {
            var _Listthuoc = new List<ThuocEntities>();
            var _thuoc = _unitOfWork.ThuocRepository.GetAll().Where(x => x.loaiThuoc == id_LoaiThuoc).ToList();
            if(_thuoc.Any())
            {
                Mapper.CreateMap<Thuoc, ThuocEntities>();
                _Listthuoc = Mapper.Map<List<Thuoc>, List<ThuocEntities>>(_thuoc);
            }
            return _Listthuoc;
        }
        public ThuocEntities GetByName(string name)
        {           
           var thuoc = _unitOfWork.ThuocRepository.Get(x => x.tenThuoc == name);
            var result = new ThuocEntities();
           if(thuoc!=null)
            {
                Mapper.CreateMap<Thuoc, ThuocEntities>();
                result = Mapper.Map<Thuoc, ThuocEntities>(thuoc);
            }
            return result;
        }

        public bool Update(ThuocEntities Option)
        {
            var success = false;
            if(Option!=null)
            {
                using (var scope = new TransactionScope())
                {
                    var o = _unitOfWork.ThuocRepository.Get(x => x.ID_Thuoc == Option.ID_Thuoc);
                    if (o != null)
                    {
                        o.ID_Thuoc = Option.ID_Thuoc;
                        o.tenThuoc = Option.tenThuoc;
                        o.loaiThuoc = Option.loaiThuoc;
                        o.soLuongTon = Option.soLuongTon;
                        o.GhiChu = Option.GhiChu;
                        o.Date_Edited = DateTime.Now;;
                        o.IsDelete = Option.IsDelete;
                    }
                    _unitOfWork.ThuocRepository.Update(o);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }
            return success;
        }
        }
    }
