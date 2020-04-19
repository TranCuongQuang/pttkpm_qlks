using Newtonsoft.Json;
using QLKS.Class;
using System;
using System.IO;
using System.Linq;

namespace QLKS
{
    public partial class WebServiceCP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var a = Request;
            var action = Request.Params["Action"];
            Response.ContentType = "application/json; charset=utf-8";

            switch (action)
            {
                //NV
                case "GetEmpList":
                    Response.Write(JsonConvert.SerializeObject(GetEmpList()));
                    Response.End();
                    break;

                case "GetEmpByID":
                    Response.Write(JsonConvert.SerializeObject(GetEmpByID()));
                    Response.End();
                    break;

                case "CreateEmp":
                    Response.Write(JsonConvert.SerializeObject(CreateEmp()));
                    Response.End();
                    break;

                case "UpdateEmp":
                    Response.Write(JsonConvert.SerializeObject(UpdateEmp()));
                    Response.End();
                    break;

                case "DeleteEmp":
                    Response.Write(JsonConvert.SerializeObject(DeleteEmp()));
                    Response.End();
                    break;

                //KH
                case "GetCustomerList":
                    Response.Write(JsonConvert.SerializeObject(GetCustomerList()));
                    Response.End();
                    break;

                case "GetCustomerByID":
                    Response.Write(JsonConvert.SerializeObject(GetCustomerByID()));
                    Response.End();
                    break;

                case "CreateCustomer":
                    Response.Write(JsonConvert.SerializeObject(CreateCustomer()));
                    Response.End();
                    break;

                case "UpdateCustomer":
                    Response.Write(JsonConvert.SerializeObject(UpdateCustomer()));
                    Response.End();
                    break;

                case "DeleteCustomer":
                    Response.Write(JsonConvert.SerializeObject(DeleteCustomer()));
                    Response.End();
                    break;

                //Phong
                case "GetRoomList":
                    Response.Write(JsonConvert.SerializeObject(GetRoomList()));
                    Response.End();
                    break;

                case "GetRoomByID":
                    Response.Write(JsonConvert.SerializeObject(GetRoomByID()));
                    Response.End();
                    break;

                case "CreateRoom":
                    Response.Write(JsonConvert.SerializeObject(CreateRoom()));
                    Response.End();
                    break;

                case "UpdateRoom":
                    Response.Write(JsonConvert.SerializeObject(UpdateRoom()));
                    Response.End();
                    break;

                case "DeleteRoom":
                    Response.Write(JsonConvert.SerializeObject(DeleteRoom()));
                    Response.End();
                    break;

                //Dịch vụ
                case "GetServiceList":
                    Response.Write(JsonConvert.SerializeObject(GetServiceList()));
                    Response.End();
                    break;

                case "GetServiceByID":
                    Response.Write(JsonConvert.SerializeObject(GetServiceByID()));
                    Response.End();
                    break;

                case "CreateService":
                    Response.Write(JsonConvert.SerializeObject(CreateService()));
                    Response.End();
                    break;

                case "UpdateService":
                    Response.Write(JsonConvert.SerializeObject(UpdateService()));
                    Response.End();
                    break;

                case "DeleteService":
                    Response.Write(JsonConvert.SerializeObject(DeleteService()));
                    Response.End();
                    break;

                default:
                    Response.End();
                    break;
            }
        }

        #region Nhân viên

        //[WebMethod(EnableSession = true)]
        //[ScriptMethod]
        private AjaxReponseModel<dynamic> GetEmpList()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                string maNV = dym.MaNV;
                string tenNV = dym.TenNV;
                using (var ctx = new qlksEntities())
                {
                    //var emp1 = ctx.tblNhanViens.ToList();
                    //.Where(st => st.MaNV == (maNV != null ? Convert.ToInt32(maNV) : 0) && st.TenNV == tenNV)
                    var emp = ctx.tblNhanViens.AsEnumerable()
                        .Where(st => (maNV == "" || st.MaNV == Convert.ToInt32(maNV)) && (tenNV == "" || st.TenNV == tenNV))
                        .Select(st => new
                        {
                            st.MaNV,
                            st.TenNV,
                            st.SDT,
                            NgaySinh = st.NgaySinh.GetValueOrDefault().ToString("dd-MM-yyyy"),
                            st.Email,
                            st.DiaChi,
                            st.ChucVu
                        }).ToList();
                    response.Data = emp;
                }
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> GetEmpByID()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maNV = Convert.ToInt32(dym.MaNV);
                using (var ctx = new qlksEntities())
                {
                    //var emp1 = ctx.tblNhanViens.ToList();
                    var emp = ctx.tblNhanViens.AsEnumerable().Select(st => new
                    {
                        st.MaNV,
                        st.TenNV,
                        st.SDT,
                        NgaySinh = st.NgaySinh.GetValueOrDefault().ToString("dd-MM-yyyy"),
                        st.Email,
                        st.DiaChi,
                        st.ChucVu
                    }).Where(st => st.MaNV == maNV).ToList();
                    response.Data = emp;
                }
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> CreateEmp()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);

                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = new tblNhanVien();
                    nv.TenNV = String.IsNullOrEmpty(dym.TenNV.ToString()) ? String.Empty : dym.TenNV.ToString().Trim();
                    nv.SDT = String.IsNullOrEmpty(dym.SDT.ToString()) ? String.Empty : dym.SDT.ToString().Trim();
                    nv.Email = String.IsNullOrEmpty(dym.Email.ToString()) ? String.Empty : dym.Email.ToString().Trim();
                    nv.DiaChi = String.IsNullOrEmpty(dym.DiaChi.ToString()) ? String.Empty : dym.DiaChi.ToString().Trim();
                    nv.NgaySinh = dym.NgaySinh;
                    nv.ChucVu = String.IsNullOrEmpty(dym.ChucVu.ToString()) ? String.Empty : dym.ChucVu.ToString().Trim();

                    db.tblNhanViens.Add(nv);
                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };
                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> UpdateEmp()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maNV = Convert.ToInt32(dym.MaNV);
                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = db.tblNhanViens.SingleOrDefault(w => w.MaNV == maNV);
                    nv.TenNV = String.IsNullOrEmpty(dym.TenNV.ToString()) ? String.Empty : dym.TenNV.ToString().Trim();
                    nv.SDT = String.IsNullOrEmpty(dym.SDT.ToString()) ? String.Empty : dym.SDT.ToString().Trim();
                    nv.Email = String.IsNullOrEmpty(dym.Email.ToString()) ? String.Empty : dym.Email.ToString().Trim();
                    nv.DiaChi = String.IsNullOrEmpty(dym.DiaChi.ToString()) ? String.Empty : dym.DiaChi.ToString().Trim();
                    nv.NgaySinh = dym.NgaySinh;
                    nv.ChucVu = String.IsNullOrEmpty(dym.ChucVu.ToString()) ? String.Empty : dym.ChucVu.ToString().Trim();

                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> DeleteEmp()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maNV = Convert.ToInt32(dym.MaNV);
                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = db.tblNhanViens.SingleOrDefault(w => w.MaNV == maNV);
                    db.tblNhanViens.Remove(nv);
                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        #endregion Nhân viên

        #region Khách hàng

        private AjaxReponseModel<dynamic> GetCustomerList()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                string maKH = dym.MaKH;
                string tenKH = dym.TenKH;
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblKhachHangs.AsEnumerable()
                        .Where(st => (maKH == "" || st.MaKH == Convert.ToInt32(maKH)) && (tenKH == "" || st.TenKH == tenKH))
                        .Select(st => new
                        {
                            st.MaKH,
                            st.TenKH,
                            st.SDT,
                            NgaySinh = st.NgaySinh.GetValueOrDefault().ToString("dd-MM-yyyy"),
                            st.Email,
                            st.DiaChi
                        }).ToList();
                    response.Data = emp;
                }
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> GetCustomerByID()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maKH = Convert.ToInt32(dym.MaKH);
                using (var ctx = new qlksEntities())
                {
                    //var emp1 = ctx.tblNhanViens.ToList();
                    var emp = ctx.tblKhachHangs.AsEnumerable().Select(st => new
                    {
                        st.MaKH,
                        st.TenKH,
                        st.SDT,
                        NgaySinh = st.NgaySinh.GetValueOrDefault().ToString("dd-MM-yyyy"),
                        st.Email,
                        st.DiaChi
                    }).Where(st => st.MaKH == maKH).ToList();
                    response.Data = emp;
                }
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> CreateCustomer()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);

                using (var db = new qlksEntities())
                {
                    tblKhachHang kh = new tblKhachHang();
                    kh.TenKH = String.IsNullOrEmpty(dym.TenKH.ToString()) ? String.Empty : dym.TenKH.ToString().Trim();
                    kh.SDT = String.IsNullOrEmpty(dym.SDT.ToString()) ? String.Empty : dym.SDT.ToString().Trim();
                    kh.Email = String.IsNullOrEmpty(dym.Email.ToString()) ? String.Empty : dym.Email.ToString().Trim();
                    kh.DiaChi = String.IsNullOrEmpty(dym.DiaChi.ToString()) ? String.Empty : dym.DiaChi.ToString().Trim();
                    kh.NgaySinh = dym.NgaySinh;

                    db.tblKhachHangs.Add(kh);
                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };
                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> UpdateCustomer()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maKH = Convert.ToInt32(dym.MaKH);
                using (var db = new qlksEntities())
                {
                    tblKhachHang kh = db.tblKhachHangs.SingleOrDefault(w => w.MaKH == maKH);
                    kh.TenKH = String.IsNullOrEmpty(dym.TenKH.ToString()) ? String.Empty : dym.TenKH.ToString().Trim();
                    kh.SDT = String.IsNullOrEmpty(dym.SDT.ToString()) ? String.Empty : dym.SDT.ToString().Trim();
                    kh.Email = String.IsNullOrEmpty(dym.Email.ToString()) ? String.Empty : dym.Email.ToString().Trim();
                    kh.DiaChi = String.IsNullOrEmpty(dym.DiaChi.ToString()) ? String.Empty : dym.DiaChi.ToString().Trim();
                    kh.NgaySinh = dym.NgaySinh;

                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> DeleteCustomer()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maKH = Convert.ToInt32(dym.MaKH);
                using (var db = new qlksEntities())
                {
                    tblKhachHang kh = db.tblKhachHangs.SingleOrDefault(w => w.MaKH == maKH);
                    db.tblKhachHangs.Remove(kh);
                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        #endregion Khách hàng

        #region Phòng

        private AjaxReponseModel<dynamic> GetRoomList()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                string maPhong = dym.MaPhong;
                string tenPhong = dym.TenPhong;
                string trangThai = dym.TrangThai;
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblPhongs.AsEnumerable()
                        .Where(st => (maPhong == "" || st.MaPhong == Convert.ToInt32(maPhong)) && (tenPhong == "" || st.TenPhong == tenPhong) && (trangThai == "" || st.TrangThai == trangThai.Equals("1")))
                        .Select(st => new
                        {
                            st.MaPhong,
                            st.TenPhong,
                            st.DonGia,
                            StrTrangThai = st.TrangThai == true ? "Đã đặt phòng" : "Trống",
                            TrangThai = Convert.ToInt32(st.TrangThai)
                        }).ToList();
                    response.Data = emp;
                }
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> GetRoomByID()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maPhong = Convert.ToInt32(dym.MaPhong);
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblPhongs.AsEnumerable().Select(st => new
                    {
                        st.MaPhong,
                        st.TenPhong,
                        st.DonGia,
                        StrTrangThai = st.TrangThai == true ? "Đã đặt phòng" : "Trống",
                        TrangThai = Convert.ToInt32(st.TrangThai)
                    }).Where(st => st.MaPhong == maPhong).ToList();
                    response.Data = emp;
                }
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> CreateRoom()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);

                using (var db = new qlksEntities())
                {
                    tblPhong p = new tblPhong();
                    p.TenPhong = String.IsNullOrEmpty(dym.TenPhong.ToString()) ? String.Empty : dym.TenPhong.ToString().Trim();
                    p.DonGia = dym.DonGia;
                    p.TrangThai = dym.TrangThai;

                    db.tblPhongs.Add(p);
                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };
                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> UpdateRoom()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maPhong = Convert.ToInt32(dym.MaPhong);
                using (var db = new qlksEntities())
                {
                    tblPhong p = db.tblPhongs.SingleOrDefault(w => w.MaPhong == maPhong);
                    p.TenPhong = String.IsNullOrEmpty(dym.TenPhong.ToString()) ? String.Empty : dym.TenPhong.ToString().Trim();
                    p.DonGia = dym.DonGia;
                    p.TrangThai = dym.TrangThai;

                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> DeleteRoom()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maPhong = Convert.ToInt32(dym.MaPhong);
                using (var db = new qlksEntities())
                {
                    tblPhong p = db.tblPhongs.SingleOrDefault(w => w.MaPhong == maPhong);
                    db.tblPhongs.Remove(p);
                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        #endregion Phòng

        #region Dịch vụ

        private AjaxReponseModel<dynamic> GetServiceList()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                string maDV = dym.MaDV;
                string tenDV = dym.TenDV;
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblDichVus.AsEnumerable()
                        .Where(st => (maDV == "" || st.MaDV == Convert.ToInt32(maDV)) && (tenDV == "" || st.TenDV == tenDV))
                        .Select(st => new
                        {
                            st.MaDV,
                            st.TenDV,
                            st.DonGia
                        }).ToList();
                    response.Data = emp;
                }
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> GetServiceByID()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maDV = Convert.ToInt32(dym.MaDV);
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblDichVus.AsEnumerable().Select(st => new
                    {
                        st.MaDV,
                        st.TenDV,
                        st.DonGia
                    }).Where(st => st.MaDV == maDV).ToList();
                    response.Data = emp;
                }
                return response;
            }
            catch (Exception e)
            {
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> CreateService()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);

                using (var db = new qlksEntities())
                {
                    tblDichVu dv = new tblDichVu();
                    dv.TenDV = String.IsNullOrEmpty(dym.TenDV.ToString()) ? String.Empty : dym.TenDV.ToString().Trim();
                    dv.DonGia = dym.DonGia;

                    db.tblDichVus.Add(dv);
                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };
                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> UpdateService()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maDV = Convert.ToInt32(dym.MaDV);
                using (var db = new qlksEntities())
                {
                    tblDichVu dv = db.tblDichVus.SingleOrDefault(w => w.MaDV == maDV);
                    dv.TenDV = String.IsNullOrEmpty(dym.TenDV.ToString()) ? String.Empty : dym.TenDV.ToString().Trim();
                    dv.DonGia = dym.DonGia;

                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> DeleteService()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maDV = Convert.ToInt32(dym.MaDV);
                using (var db = new qlksEntities())
                {
                    tblDichVu dv = db.tblDichVus.SingleOrDefault(w => w.MaDV == maDV);
                    db.tblDichVus.Remove(dv);
                    db.SaveChanges();
                    response.Message = "SUCCESS";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "ERROR";
                return response;
            }
            finally
            {
            }
        }

        #endregion Dịch vụ
    }
}