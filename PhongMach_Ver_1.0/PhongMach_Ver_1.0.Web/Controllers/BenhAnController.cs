using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhongMach_Ver_1._0.Services;
using PhongMach_Ver_1._0.Models.Models;
using PhongMach_Ver_1._0.Entities;
using System.Net.Http;
using System.Net;

namespace PhongMach_Ver_1._0.Web.Controllers
{
    public class BenhAnController : BaseController
    {
        public BenhAnController(IBenhAnService _benhanService, ICTDTService _ctdtService, ICTHDService _cthdService, IDInhNghiaGiaTriService _dngtService, IDonThuocService _donthuocService, IGiaTriMacDinhService _gtmdService, IHoaDonService _hoadonService, IBenhNhanService _benhnhanService, ILichKhamService _lichkhamService, INguoiDungService _nguoidungService, IThuocService _thuocService) : base(_benhanService, _ctdtService, _cthdService, _dngtService, _donthuocService, _gtmdService, _hoadonService, _benhnhanService, _lichkhamService, _nguoidungService, _thuocService)
        {
        }

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "NguoiDung");
            }
            return View();    
        }
        [HttpPost]
        public JsonResult GetAll()
        {
            var data = new List<BenhAnModel>();
            var result = _benhanService.GetAll().OrderBy(x => x.ID_BenhAn).ToList();
            if (result.Count > 0)
            {
                foreach (BenhAnEntities en in result)
                {
                    AutoMapper.Mapper.CreateMap<BenhAnEntities, BenhAnModel>();
                    var benhan = AutoMapper.Mapper.Map<BenhAnEntities, BenhAnModel>(en);
                    if (benhan.ID_BenhNhan != null || benhan.ID_BenhNhan != -1)
                    {
                        benhan.TenBenhNhan = _benhnhanService.GetByID(benhan.ID_BenhNhan).ten_BenhNhan.ToString();
                    }
                    data.Add(benhan);
                }
            }
            return new JsonResult
            {
                Data = data,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult CreateOrUpDate(BenhAnEntities Option)
        {
            var result = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            try
            {
                if (Option.ID_BenhAn > 0)
                {
                    Option.Date_Edited = DateTime.Now;
                    _benhanService.Update(Option);
                }

                else
                {
                    Option.Date_Created = DateTime.Now;
                    _donthuocService.CreateTemp();
                    _benhanService.Create(Option);
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.ReasonPhrase = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            try
            {
                if (id > 0)
                {
                    _benhanService.Delete(id);
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.ReasonPhrase = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }       
    }
}