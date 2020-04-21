using Newtonsoft.Json;
using QLKS.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QLKS
{
    public partial class WebServiceCTQ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var action = Request.Params["Action"];
            Response.ContentType = "application/json; charset=utf-8";

            switch (action)
            {
                #region Login

                case "Login":
                    Response.Write(JsonConvert.SerializeObject(Login()));
                    Response.End();
                    break;

                #endregion Login

                case "SaveBookingRoom":
                    Response.Write(JsonConvert.SerializeObject(SaveBookingRoom()));
                    Response.End();
                    break;

                default:

                    Response.End();
                    break;
            }
        }

        private AjaxReponseModel<dynamic> Login()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            var data = new StreamReader(Request.InputStream).ReadToEnd();
            var dym = JsonConvert.DeserializeObject<dynamic>(data);

            string userName = dym.userName;
            string passWord = dym.passWord;

            using (var db = new qlksEntities())
            {
                var user = db.tblTaiKhoans.SingleOrDefault(w => w.MatKhau == passWord && w.TenDangNhap == userName);

                if (user != null)
                {
                    response.Message = "Đăng nhập thành công.";
                    Session["UserLogin"] = user;
                }
                else
                {
                    response.Status = AjaxReponseStatusEnum.Fail;
                    response.Message = "Đăng nhập thất bại. Vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu.";
                }
            };

            return response;
        }

        private AjaxReponseModel<dynamic> SaveBookingRoom()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            var data = new StreamReader(Request.InputStream).ReadToEnd();
            var dym = JsonConvert.DeserializeObject<BookingRoomModel>(data);
            tblTaiKhoan userLogin = Session["UserLogin"] as tblTaiKhoan;

            try
            {
                using (var db = new qlksEntities())
                {
                    tblPhieuDatPhong datPhong = new tblPhieuDatPhong()
                    {
                        MaKH = dym.MaKH,
                        MaPhong = dym.MaPhong,
                        MaNV = 4,
                        NgayBD = dym.NgayBD,
                        NgayKT = dym.NgayKT,
                        TongTien = dym.TongTien,
                        DonGia = dym.DonGia
                    };

                    db.tblPhieuDatPhongs.Add(datPhong);
                    var numberSave = db.SaveChanges();

                    if (numberSave > 0)
                    {
                        var room = db.tblPhongs.FirstOrDefault(f => f.MaPhong == dym.MaPhong);
                        if(room != null)
                        {
                            room.TrangThai = true;
                        }

                        List<tblDichVuPhong> listDVP = new List<tblDichVuPhong>();
                        foreach (var item in dym.DichVuPhong)
                        {
                            tblDichVuPhong dichVuPhong = new tblDichVuPhong()
                            {
                                MaPhieuDP = datPhong.MaPhieuDP,
                                MaDV = item.MaDV,
                                SoLuong = item.SoLuong,
                                DonGia = item.DonGia,
                                ThanhTien = item.ThanhTien
                            };
                            listDVP.Add(dichVuPhong);
                        }

                        db.tblDichVuPhongs.AddRange(listDVP);

                        List<tblSanPhamPhong> listSPP = new List<tblSanPhamPhong>();
                        foreach (var item in dym.SanPhamPhong)
                        {
                            tblSanPhamPhong dichVuPhong = new tblSanPhamPhong()
                            {
                                MaPhieuDP = datPhong.MaPhieuDP,
                                MaSP = item.MaSP,
                                SoLuong = item.SoLuong,
                                DonGia = item.DonGia,
                                ThanhTien = item.ThanhTien
                            };
                            listSPP.Add(dichVuPhong);
                        }

                        db.tblSanPhamPhongs.AddRange(listSPP);

                        db.SaveChanges();

                        response.Message = "Đặt phòng thành công.";
                    }
                    else
                    {
                        response.Status = AjaxReponseStatusEnum.Fail;
                        response.Message = "Đặt phòng thất bại.";
                    }
                };
            }
            catch (Exception e)
            {
                response.Status = AjaxReponseStatusEnum.Fail;
                response.Message = "Đặt phòng thất bại (Exception).";
            }

            return response;
        }
    }
}