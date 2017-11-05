using PhongMach_Ver_1._0.Datamodel.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMach_Ver_1._0.Datamodel.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly PhongMachEntities _context;
        private GenericRepository<BenhAn> _BenhAnRepository;
        private GenericRepository<BenhNhan> _BenhNhanRepository;
        private GenericRepository<ChiTietDonThuoc> _ChiTietDonThuocRepository;
        private GenericRepository<CTHD> _CTHDRepository;
        private GenericRepository<DinhNghiaGiaTri> _DinhNgiaGiaTriRepository;
        private GenericRepository<DonThuoc> _DonThuocRepository;
        private GenericRepository<GiaTriMacDinh> _GiaTriMacDinhRepository;
        private GenericRepository<HoaDon> _HoaDonRepository;
        private GenericRepository<LichKham> _LichKhamRepository;
        private GenericRepository<NguoiDung> _NguoiDungRepository;
        private GenericRepository<Thuoc> _ThuocRepository;
        public UnitOfWork()
        {
            _context = new PhongMachEntities();
        }
        public GenericRepository<BenhAn> BenhAnRepository
        {
            get
            {
                if (this._BenhAnRepository == null)

                    this._BenhAnRepository = new GenericRepository<BenhAn>(_context);
                return _BenhAnRepository;
            }
        }
        public GenericRepository<BenhNhan> BenhNhanRepository
        {
            get
            {
                if (this._BenhNhanRepository == null)

                    this._BenhNhanRepository = new GenericRepository<BenhNhan>(_context);
                return _BenhNhanRepository;
            }
        }
        public GenericRepository<ChiTietDonThuoc> ChiTietDonThuocRepository
        {
            get
            {
                if (this._ChiTietDonThuocRepository == null)

                    this._ChiTietDonThuocRepository = new GenericRepository<ChiTietDonThuoc>(_context);
                return _ChiTietDonThuocRepository;
            }
        }
        public GenericRepository<CTHD> CTHDRepository
        {
            get
            {
                if (this._CTHDRepository == null)

                    this._CTHDRepository = new GenericRepository<CTHD>(_context);
                return _CTHDRepository;
            }
        }
        public GenericRepository<DinhNghiaGiaTri> DinhNghiaGiaTriRepository
        {
            get
            {
                if (this._DinhNgiaGiaTriRepository == null)

                    this._DinhNgiaGiaTriRepository = new GenericRepository<DinhNghiaGiaTri>(_context);
                return _DinhNgiaGiaTriRepository;
            }
        }
        public GenericRepository<DonThuoc> DonThuocRepository
        {
            get
            {
                if (this._DonThuocRepository == null)

                    this._DonThuocRepository = new GenericRepository<DonThuoc>(_context);
                return _DonThuocRepository;
            }
        }
        public GenericRepository<GiaTriMacDinh> GiaTriMacDinhRepository
        {
            get
            {
                if (this._GiaTriMacDinhRepository == null)

                    this._GiaTriMacDinhRepository = new GenericRepository<GiaTriMacDinh>(_context);
                return _GiaTriMacDinhRepository;
            }
        }
        public GenericRepository<HoaDon> HoaDonRepository
        {
            get
            {
                if (this._HoaDonRepository == null)

                    this._HoaDonRepository = new GenericRepository<HoaDon>(_context);
                return _HoaDonRepository;
            }
        }
        public GenericRepository<LichKham> LichKhamRepository
        {
            get
            {
                if (this._LichKhamRepository == null)

                    this._LichKhamRepository = new GenericRepository<LichKham>(_context);
                return _LichKhamRepository;
            }
        }
        public GenericRepository<NguoiDung> NguoiDungRepository
        {
            get
            {
                if (this._NguoiDungRepository == null)

                    this._NguoiDungRepository = new GenericRepository<NguoiDung>(_context);
                return _NguoiDungRepository;
            }
        }
        public GenericRepository<Thuoc> ThuocRepository
        {
            get
            {
                if (this._ThuocRepository == null)

                    this._ThuocRepository = new GenericRepository<Thuoc>(_context);
                return _ThuocRepository;
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                //thaco.ultils.clsShared.writeLog(outputLines);

                throw e;
            }
        }
    }
}
