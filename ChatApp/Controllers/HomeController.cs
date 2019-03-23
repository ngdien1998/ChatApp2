using ChatApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private ChatContext database = ChatContext.Instance;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult SubmitLogin(string tenDangNhap, string matKhau)
        {
            NguoiDung nguoiDung = database.NguoiDungs.Where(e => e.TenDangNhap == tenDangNhap && e.MatKhau == matKhau).FirstOrDefault();
            if (nguoiDung != null)
            {
                Session["NguoiDungDangNhap"] = nguoiDung;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag["ThongBao"] = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View();
            }
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Signup")]
        public ActionResult SubmitSignup(string tenDangNhap, string hoTen, string matKhau, HttpPostedFileBase hinhAnh)
        {
            if (database.NguoiDungs.Any(e => e.TenDangNhap == tenDangNhap))
            {
                ViewBag["ThongBao"] = "Tên đăng nhập đã tồn tại, vui lòng chọn cái khác."; 
                return View();
            }

            string filename = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString() + Path.GetExtension(hinhAnh.FileName);
            string path = Path.Combine(Server.MapPath("~/Upload/profile"), filename);
            hinhAnh.SaveAs(path);

            NguoiDung nguoiDung = new NguoiDung
            {
                TenDangNhap = tenDangNhap,
                HoTen = hoTen,
                MatKhau = matKhau,
                HinhAnh = "~/Upload/profile/" + filename
            };

            database.NguoiDungs.Add(nguoiDung);
            database.SaveChanges();

            Session["NguoiDungDangNhap"] = nguoiDung;
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            if (Session["NguoiDungDangNhap"] == null)
            {
                return RedirectToAction("Login");
            }
            return View(database.NguoiDungs);
        }
    }
}