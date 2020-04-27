using Newtonsoft.Json;
using QLKS.Class;
using System;
using System.Collections.Generic;
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

                //Sản phẩm
                case "GetProductList":
                    Response.Write(JsonConvert.SerializeObject(GetProductList()));
                    Response.End();
                    break;

                case "GetProductByID":
                    Response.Write(JsonConvert.SerializeObject(GetProductByID()));
                    Response.End();
                    break;

                case "CreateProduct":
                    Response.Write(JsonConvert.SerializeObject(CreateProduct()));
                    Response.End();
                    break;

                case "UpdateProduct":
                    Response.Write(JsonConvert.SerializeObject(UpdateProduct()));
                    Response.End();
                    break;

                case "DeleteProduct":
                    Response.Write(JsonConvert.SerializeObject(DeleteProduct()));
                    Response.End();
                    break;

                //Trang thiết bị
                case "GetEquipmentList":
                    Response.Write(JsonConvert.SerializeObject(GetEquipmentList()));
                    Response.End();
                    break;

                case "GetEquipmentByID":
                    Response.Write(JsonConvert.SerializeObject(GetEquipmentByID()));
                    Response.End();
                    break;

                case "CreateEquipment":
                    Response.Write(JsonConvert.SerializeObject(CreateEquipment()));
                    Response.End();
                    break;

                case "UpdateEquipment":
                    Response.Write(JsonConvert.SerializeObject(UpdateEquipment()));
                    Response.End();
                    break;

                case "DeleteEquipment":
                    Response.Write(JsonConvert.SerializeObject(DeleteEquipment()));
                    Response.End();
                    break;

                //Trang thiết bị phòng
                //case "GetEquipmentRoomList":
                //    Response.Write(JsonConvert.SerializeObject(GetEquipmentRoomList()));
                //    Response.End();
                //    break;

                //case "GetEquipmentRoomByID":
                //    Response.Write(JsonConvert.SerializeObject(GetEquipmentRoomByID()));
                //    Response.End();
                //    break;

                //case "CreateEquipmentRoom":
                //    Response.Write(JsonConvert.SerializeObject(CreateEquipmentRoom()));
                //    Response.End();
                //    break;

                //case "UpdateEquipmentRoom":
                //    Response.Write(JsonConvert.SerializeObject(UpdateEquipmentRoom()));
                //    Response.End();
                //    break;

                case "DeleteEquipmentRoom":
                    Response.Write(JsonConvert.SerializeObject(DeleteEquipmentRoom()));
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
                        .Where(st => (maNV == "" || st.MaNV == Convert.ToInt32(maNV)) && (tenNV == "" || st.TenNV.Contains(tenNV)))
                        .Select(st => new
                        {
                            st.MaNV,
                            st.TenNV,
                            st.SDT,
                            NgaySinh = st.NgaySinh.GetValueOrDefault().ToString("dd-MM-yyyy"),
                            st.Email,
                            st.DiaChi,
                            st.ChucVu,
                            st.TenDangNhap,
                            st.MatKhau
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
                var dym = JsonConvert.DeserializeObject<Employee>(data);
                int maNV = dym.MaNV;
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
                        st.ChucVu,
                        st.TenDangNhap,
                        st.MatKhau
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
                var dym = JsonConvert.DeserializeObject<Employee>(data);

                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = new tblNhanVien();
                    nv.TenNV = String.IsNullOrEmpty(dym.TenNV.ToString()) ? String.Empty : dym.TenNV.ToString().Trim();
                    nv.SDT = String.IsNullOrEmpty(dym.SDT.ToString()) ? String.Empty : dym.SDT.ToString().Trim();
                    nv.Email = String.IsNullOrEmpty(dym.Email.ToString()) ? String.Empty : dym.Email.ToString().Trim();
                    nv.DiaChi = String.IsNullOrEmpty(dym.DiaChi.ToString()) ? String.Empty : dym.DiaChi.ToString().Trim();
                    nv.NgaySinh = dym.NgaySinh;
                    nv.ChucVu = String.IsNullOrEmpty(dym.ChucVu.ToString()) ? String.Empty : dym.ChucVu.ToString().Trim();
                    nv.TenDangNhap = String.IsNullOrEmpty(dym.TenDangNhap.ToString()) ? String.Empty : dym.TenDangNhap.ToString().Trim();
                    nv.MatKhau = String.IsNullOrEmpty(dym.MatKhau.ToString()) ? String.Empty : dym.MatKhau.ToString().Trim();

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
                var dym = JsonConvert.DeserializeObject<Employee>(data);
                int maNV = dym.MaNV;
                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = db.tblNhanViens.SingleOrDefault(w => w.MaNV == maNV);
                    nv.TenNV = String.IsNullOrEmpty(dym.TenNV.ToString()) ? String.Empty : dym.TenNV.ToString().Trim();
                    nv.SDT = String.IsNullOrEmpty(dym.SDT.ToString()) ? String.Empty : dym.SDT.ToString().Trim();
                    nv.Email = String.IsNullOrEmpty(dym.Email.ToString()) ? String.Empty : dym.Email.ToString().Trim();
                    nv.DiaChi = String.IsNullOrEmpty(dym.DiaChi.ToString()) ? String.Empty : dym.DiaChi.ToString().Trim();
                    nv.NgaySinh = dym.NgaySinh;
                    nv.ChucVu = String.IsNullOrEmpty(dym.ChucVu.ToString()) ? String.Empty : dym.ChucVu.ToString().Trim();
                    nv.TenDangNhap = String.IsNullOrEmpty(dym.TenDangNhap.ToString()) ? String.Empty : dym.TenDangNhap.ToString().Trim();
                    nv.MatKhau = String.IsNullOrEmpty(dym.MatKhau.ToString()) ? String.Empty : dym.MatKhau.ToString().Trim();

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
                var dym = JsonConvert.DeserializeObject<Employee>(data);
                int maNV = dym.MaNV;
                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = db.tblNhanViens.SingleOrDefault(w => w.MaNV == maNV);
                    tblPhieuDatPhong pdp = db.tblPhieuDatPhongs.SingleOrDefault(w => w.MaNV == maNV);
                    if (pdp != null && pdp.MaPhieuDP > 0)
                    {
                        response.Message = "PDP_EXIST";
                    }
                    else
                    {
                        db.tblNhanViens.Remove(nv);
                        db.SaveChanges();
                        response.Message = "SUCCESS";
                    }
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
                        .Where(st => (maKH == "" || st.MaKH == Convert.ToInt32(maKH)) && (tenKH == "" || st.TenKH.Contains(tenKH)))
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
                var dym = JsonConvert.DeserializeObject<Customer>(data);
                int maKH = dym.MaKH;
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
                var dym = JsonConvert.DeserializeObject<Customer>(data);

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
                    response.Data = kh;
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
                var dym = JsonConvert.DeserializeObject<Customer>(data);
                int maKH = dym.MaKH;
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
                var dym = JsonConvert.DeserializeObject<Customer>(data);
                int maKH = dym.MaKH;
                using (var db = new qlksEntities())
                {
                    tblKhachHang kh = db.tblKhachHangs.SingleOrDefault(w => w.MaKH == maKH);
                    tblPhieuDatPhong pdp = db.tblPhieuDatPhongs.SingleOrDefault(w => w.MaKH == maKH);
                    if (pdp != null && pdp.MaPhieuDP > 0)
                    {
                        response.Message = "PDP_EXIST";
                    }
                    else
                    {
                        db.tblKhachHangs.Remove(kh);
                        db.SaveChanges();
                        response.Message = "SUCCESS";
                    }
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
                        .Where(st => (maPhong == "" || st.MaPhong == Convert.ToInt32(maPhong)) && (tenPhong == "" || st.TenPhong.Contains(tenPhong)) && (trangThai == "" || st.TrangThai == trangThai.Equals("1")))
                        .Select(st => new
                        {
                            st.MaPhong,
                            st.TenPhong,
                            st.DonGia,
                            StrTrangThai = st.TrangThai == true ? "Đã đặt" : "Trống",
                            TrangThai = Convert.ToInt32(st.TrangThai),
                            ThietBiPhong = st.tblTrangThietBiPhongs.Select(s1 => new
                            {
                                s1.MaTTBP,
                                s1.MaThietBi,
                                s1.MaPhong,
                                s1.tblTrangThietBi.TenThietBi,
                                s1.DonGia,
                                s1.SoLuong,
                                s1.ThanhTien
                            }).ToList(),
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
                var dym = JsonConvert.DeserializeObject<Room>(data);
                int maPhong = dym.MaPhong;
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblPhongs.AsEnumerable().Where(st => st.MaPhong == maPhong).Select(st => new
                    {
                        st.MaPhong,
                        st.TenPhong,
                        st.DonGia,
                        StrTrangThai = st.TrangThai == true ? "Đã đặt" : "Trống",
                        TrangThai = Convert.ToInt32(st.TrangThai),
                        ThietBiPhong = st.tblTrangThietBiPhongs.Select(s1 => new
                        {
                            s1.MaTTBP,
                            s1.MaThietBi,
                            s1.MaPhong,
                            s1.tblTrangThietBi.TenThietBi,
                            s1.DonGia,
                            s1.SoLuong,
                            s1.ThanhTien
                        }).ToList(),
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

        private AjaxReponseModel<dynamic> CreateRoom()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Room>(data);

                using (var db = new qlksEntities())
                {
                    tblPhong p = new tblPhong();
                    p.TenPhong = String.IsNullOrEmpty(dym.TenPhong.ToString()) ? String.Empty : dym.TenPhong.ToString().Trim();
                    p.DonGia = dym.DonGia;
                    p.TrangThai = dym.TrangThai;

                    db.tblPhongs.Add(p);
                    var numberSave = db.SaveChanges();

                    if (numberSave > 0)
                    {
                        List<tblTrangThietBiPhong> listTTBP = new List<tblTrangThietBiPhong>();
                        foreach (var item in dym.ThietBiPhong)
                        {
                            tblTrangThietBiPhong thietBiPhong = new tblTrangThietBiPhong()
                            {
                                MaPhong = p.MaPhong,
                                MaThietBi = item.MaThietBi,
                                SoLuong = item.SoLuong,
                                DonGia = item.DonGia,
                                ThanhTien = item.ThanhTien
                            };
                            listTTBP.Add(thietBiPhong);
                        }

                        db.tblTrangThietBiPhongs.AddRange(listTTBP);
                        db.SaveChanges();

                        response.Message = "SUCCESS";
                    }
                    else
                    {
                        response.Message = "ERROR";
                    }
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
                var dym = JsonConvert.DeserializeObject<Room>(data);
                int maPhong = dym.MaPhong;
                using (var db = new qlksEntities())
                {
                    tblPhong p = db.tblPhongs.SingleOrDefault(w => w.MaPhong == maPhong);
                    p.TenPhong = String.IsNullOrEmpty(dym.TenPhong.ToString()) ? String.Empty : dym.TenPhong.ToString().Trim();
                    p.DonGia = dym.DonGia;
                    p.TrangThai = dym.TrangThai;

                    foreach (var item in dym.ThietBiPhong)
                    {
                        if (item.MaTTBP != null && item.MaTTBP != 0)
                        {
                            var tbp = db.tblTrangThietBiPhongs.FirstOrDefault(f => f.MaPhong == dym.MaPhong && f.MaTTBP == item.MaTTBP);
                            tbp.SoLuong = item.SoLuong;
                            tbp.ThanhTien = item.ThanhTien;
                        }
                        else
                        {
                            tblTrangThietBiPhong thietBiPhong = new tblTrangThietBiPhong()
                            {
                                MaPhong = p.MaPhong,
                                MaThietBi = item.MaThietBi,
                                SoLuong = item.SoLuong,
                                DonGia = item.DonGia,
                                ThanhTien = item.ThanhTien
                            };
                            db.tblTrangThietBiPhongs.Add(thietBiPhong);
                        }
                    }
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
                var dym = JsonConvert.DeserializeObject<Room>(data);
                int maPhong = dym.MaPhong;
                using (var db = new qlksEntities())
                {
                    tblPhong p = db.tblPhongs.SingleOrDefault(w => w.MaPhong == maPhong);
                    tblPhieuDatPhong pdp = db.tblPhieuDatPhongs.SingleOrDefault(w => w.MaPhong == maPhong);
                    if (pdp != null && pdp.MaPhieuDP > 0)
                    {
                        response.Message = "PDP_EXIST";
                    }
                    else
                    {
                        List<tblTrangThietBiPhong> tbp = db.tblTrangThietBiPhongs.AsEnumerable()
                        .Where(st => st.MaPhong == maPhong).ToList();
                        if(tbp != null)
                        {
                            db.tblTrangThietBiPhongs.RemoveRange(tbp);
                        }
                        db.tblPhongs.Remove(p);
                        db.SaveChanges();
                        response.Message = "SUCCESS";
                    }
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
                        .Where(st => (maDV == "" || st.MaDV == Convert.ToInt32(maDV)) && (tenDV == "" || st.TenDV.Contains(tenDV)))
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
                var dym = JsonConvert.DeserializeObject<Service>(data);
                int maDV = dym.MaDV;
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
                var dym = JsonConvert.DeserializeObject<Service>(data);

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
                var dym = JsonConvert.DeserializeObject<Service>(data);
                int maDV = dym.MaDV;
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
                var dym = JsonConvert.DeserializeObject<Service>(data);
                int maDV = dym.MaDV;
                using (var db = new qlksEntities())
                {
                    tblDichVu dv = db.tblDichVus.SingleOrDefault(w => w.MaDV == maDV);
                    tblDichVuPhong pdp = db.tblDichVuPhongs.SingleOrDefault(w => w.MaDV == maDV);
                    if (pdp != null && pdp.MaPhieuDP > 0)
                    {
                        response.Message = "PDP_EXIST";
                    }
                    else
                    {
                        db.tblDichVus.Remove(dv);
                        db.SaveChanges();
                        response.Message = "SUCCESS";
                    }
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

        #region Sản phẩm

        private AjaxReponseModel<dynamic> GetProductList()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                string maSP = dym.MaSP;
                string tenSP = dym.TenSP;
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblSanPhams.AsEnumerable()
                        .Where(st => (maSP == "" || st.MaSP == Convert.ToInt32(maSP)) && (tenSP == "" || st.TenSP.Contains(tenSP)))
                        .Select(st => new
                        {
                            st.MaSP,
                            st.TenSP,
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

        private AjaxReponseModel<dynamic> GetProductByID()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Product>(data);
                int maSP = dym.MaSP;
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblSanPhams.AsEnumerable().Select(st => new
                    {
                        st.MaSP,
                        st.TenSP,
                        st.DonGia
                    }).Where(st => st.MaSP == maSP).ToList();
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

        private AjaxReponseModel<dynamic> CreateProduct()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Product>(data);

                using (var db = new qlksEntities())
                {
                    tblSanPham sp = new tblSanPham();
                    sp.TenSP = String.IsNullOrEmpty(dym.TenSP.ToString()) ? String.Empty : dym.TenSP.ToString().Trim();
                    sp.DonGia = dym.DonGia;

                    db.tblSanPhams.Add(sp);
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

        private AjaxReponseModel<dynamic> UpdateProduct()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Product>(data);
                int maSP = dym.MaSP;
                using (var db = new qlksEntities())
                {
                    tblSanPham sp = db.tblSanPhams.SingleOrDefault(w => w.MaSP == maSP);
                    sp.TenSP = String.IsNullOrEmpty(dym.TenSP.ToString()) ? String.Empty : dym.TenSP.ToString().Trim();
                    sp.DonGia = dym.DonGia;

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

        private AjaxReponseModel<dynamic> DeleteProduct()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Product>(data);
                int maSP = dym.MaSP;
                using (var db = new qlksEntities())
                {
                    tblSanPham sp = db.tblSanPhams.SingleOrDefault(w => w.MaSP == maSP);
                    tblSanPhamPhong pdp = db.tblSanPhamPhongs.SingleOrDefault(w => w.MaSP == maSP);
                    if (pdp != null && pdp.MaPhieuDP > 0)
                    {
                        response.Message = "PDP_EXIST";
                    }
                    else
                    {
                        db.tblSanPhams.Remove(sp);
                        db.SaveChanges();
                        response.Message = "SUCCESS";
                    }
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

        #endregion Sản phẩm

        #region Trang thiết bị

        private AjaxReponseModel<dynamic> GetEquipmentList()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                string maTB = dym.MaTB;
                string tenTB = dym.TenTB;
                string tinhTrang = dym.TinhTrang;
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblTrangThietBis.AsEnumerable()
                        .Where(st => (maTB == "" || st.MaThietBi == Convert.ToInt32(maTB)) && (tenTB == "" || st.TenThietBi.Contains(tenTB)) && (tinhTrang == "" || st.TinhTrang == tinhTrang.Equals("1")))
                        .Select(st => new
                        {
                            st.MaThietBi,
                            st.TenThietBi,
                            st.DonGia,
                            StrTinhTrang = st.TinhTrang == true ? "Sử dụng" : "Đã hư",
                            TinhTrang = Convert.ToInt32(st.TinhTrang)
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

        private AjaxReponseModel<dynamic> GetEquipmentByID()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Equiment>(data);
                int maTB = dym.MaTB;
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblTrangThietBis.AsEnumerable().Select(st => new
                    {
                        st.MaThietBi,
                        st.TenThietBi,
                        st.DonGia,
                        StrTinhTrang = st.TinhTrang == true ? "Sử dụng" : "Đã hư",
                        TinhTrang = Convert.ToInt32(st.TinhTrang)
                    }).Where(st => st.MaThietBi == maTB).ToList();
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

        private AjaxReponseModel<dynamic> CreateEquipment()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Equiment>(data);

                using (var db = new qlksEntities())
                {
                    tblTrangThietBi tb = new tblTrangThietBi();
                    tb.TenThietBi = String.IsNullOrEmpty(dym.TenTB.ToString()) ? String.Empty : dym.TenTB.ToString().Trim();
                    tb.DonGia = dym.DonGia;
                    tb.TinhTrang = dym.TinhTrang;

                    db.tblTrangThietBis.Add(tb);
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

        private AjaxReponseModel<dynamic> UpdateEquipment()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Equiment>(data);
                int maTB = dym.MaTB;
                using (var db = new qlksEntities())
                {
                    tblTrangThietBi tb = db.tblTrangThietBis.SingleOrDefault(w => w.MaThietBi == maTB);
                    tb.TenThietBi = String.IsNullOrEmpty(dym.TenTB.ToString()) ? String.Empty : dym.TenTB.ToString().Trim();
                    tb.DonGia = dym.DonGia;
                    tb.TinhTrang = dym.TinhTrang;

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

        private AjaxReponseModel<dynamic> DeleteEquipment()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<Equiment>(data);
                int maTB = dym.MaTB;
                using (var db = new qlksEntities())
                {
                    tblTrangThietBi sp = db.tblTrangThietBis.SingleOrDefault(w => w.MaThietBi == maTB);
                    tblTrangThietBiPhong pdp = db.tblTrangThietBiPhongs.SingleOrDefault(w => w.MaThietBi == maTB);
                    if (pdp != null && pdp.MaPhong > 0)
                    {
                        response.Message = "PDP_EXIST";
                    }
                    else
                    {
                        db.tblTrangThietBis.Remove(sp);
                        db.SaveChanges();
                        response.Message = "SUCCESS";
                    }
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

        #endregion Trang thiết bị

        #region Trang thiết bị phòng

        //private AjaxReponseModel<dynamic> GetEquipmentRoomList()
        //{
        //    var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
        //    try
        //    {
        //        var data = new StreamReader(Request.InputStream).ReadToEnd();
        //        var dym = JsonConvert.DeserializeObject<dynamic>(data);
        //        string maTTBP = dym.MaTTBP;
        //        string maTB = dym.MaTB;
        //        string maPhong = dym.MaPhong;
        //        using (var ctx = new qlksEntities())
        //        {
        //            var emp = ctx.tblTrangThietBiPhongs.AsEnumerable()
        //                .Where(st => (maTTBP == "" || st.MaTTBP == Convert.ToInt32(maTTBP)) && (maTB == "" || st.MaThietBi == Convert.ToInt32(maTB)) && (maPhong== "" || st.MaPhong == Convert.ToInt32(maPhong)))
        //                .Select(st => new
        //                {
        //                    st.MaTTBP,
        //                    st.MaThietBi,
        //                    st.MaPhong,
        //                    st.SoLuong
        //                }).ToList();
        //            response.Data = emp;
        //        }
        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        return response;
        //    }
        //    finally
        //    {
        //    }
        //}

        //private AjaxReponseModel<dynamic> GetEquipmentRoomByID()
        //{
        //    var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
        //    try
        //    {
        //        var data = new StreamReader(Request.InputStream).ReadToEnd();
        //        var dym = JsonConvert.DeserializeObject<dynamic>(data);
        //        int maTTBP = Convert.ToInt32(dym.MaTTBP);
        //        using (var ctx = new qlksEntities())
        //        {
        //            var emp = ctx.tblTrangThietBiPhongs.AsEnumerable().Select(st => new
        //            {
        //                st.MaTTBP,
        //                st.MaThietBi,
        //                st.MaPhong,
        //                st.SoLuong
        //            }).Where(st => st.MaTTBP == maTTBP).ToList();
        //            response.Data = emp;
        //        }
        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        return response;
        //    }
        //    finally
        //    {
        //    }
        //}

        //private AjaxReponseModel<dynamic> CreateEquipmentRoom()
        //{
        //    var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
        //    try
        //    {
        //        var data = new StreamReader(Request.InputStream).ReadToEnd();
        //        var dym = JsonConvert.DeserializeObject<EquimentRoom>(data);

        //        using (var db = new qlksEntities())
        //        {
        //            List<tblTrangThietBiPhong> listTTBP = new List<tblTrangThietBiPhong>();
        //            foreach (var item in dym.ThietBi)
        //            {
        //                tblTrangThietBiPhong thietBiPhong = new tblTrangThietBiPhong()
        //                {
        //                    MaPhong = dym.MaPhong,
        //                    MaThietBi = item.MaTB,
        //                    SoLuong = item.SoLuong,
        //                    DonGia = item.DonGia
        //                };
        //                listTTBP.Add(thietBiPhong);
        //            }

        //            db.tblTrangThietBiPhongs.AddRange(listTTBP);
        //            db.SaveChanges();
        //            response.Message = "SUCCESS";
        //        };
        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        response.Message = "ERROR";
        //        return response;
        //    }
        //    finally
        //    {
        //    }
        //}

        //private AjaxReponseModel<dynamic> UpdateEquipmentRoom()
        //{
        //    var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
        //    try
        //    {
        //        var data = new StreamReader(Request.InputStream).ReadToEnd();
        //        var dym = JsonConvert.DeserializeObject<EquimentRoom>(data);
        //        int maTTBP = dym.MaTTBP;
        //        using (var db = new qlksEntities())
        //        {
        //            tblTrangThietBiPhong tb = db.tblTrangThietBiPhongs.SingleOrDefault(w => w.MaTTBP == maTTBP);
        //            //tb.MaTTBP = dym.MaTTBP;
        //            tb.MaThietBi = dym.MaThietBi;
        //            tb.MaPhong = dym.MaPhong;
        //            tb.SoLuong = dym.SoLuong;

        //            List<tblTrangThietBiPhong> listTTBP = new List<tblTrangThietBiPhong>();
        //            foreach (var item in dym.ThietBi)
        //            {
        //                tblTrangThietBiPhong thietBiPhong = new tblTrangThietBiPhong()
        //                {
        //                    MaPhong = dym.MaPhong,
        //                    MaThietBi = item.MaTB,
        //                    SoLuong = item.SoLuong,
        //                    DonGia = item.DonGia
        //                };
        //                listTTBP.Add(thietBiPhong);
        //            }

        //            db.tblTrangThietBiPhongs.AddRange(listTTBP);

        //            db.SaveChanges();
        //            response.Message = "SUCCESS";
        //        };

        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        response.Message = "ERROR";
        //        return response;
        //    }
        //    finally
        //    {
        //    }
        //}

        private AjaxReponseModel<dynamic> DeleteEquipmentRoom()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<EquimentRoom>(data);
                using (var db = new qlksEntities())
                {
                    tblTrangThietBiPhong sp = db.tblTrangThietBiPhongs.SingleOrDefault(w => w.MaTTBP == dym.MaTTBP);
                    db.tblTrangThietBiPhongs.Remove(sp);
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

        #endregion Trang thiết bị phòng
    }
}