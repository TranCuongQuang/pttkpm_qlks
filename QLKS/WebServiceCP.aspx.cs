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
                case "GetEmpList":
                    Response.Write(JsonConvert.SerializeObject(GetEmpList()));
                    Response.End();
                    break;

                case "GetEmpByID":
                    Response.Write(JsonConvert.SerializeObject(GetEmpByID()));
                    Response.End();
                    break;

                case "CreateEmp":
                    Response.Write(JsonConvert.SerializeObject(Create()));
                    Response.End();
                    break;

                case "UpdateEmp":
                    Response.Write(JsonConvert.SerializeObject(Update()));
                    Response.End();
                    break;

                case "DeleteEmp":
                    Response.Write(JsonConvert.SerializeObject(Delete()));
                    Response.End();
                    break;

                default:
                    Response.End();
                    break;
            }
        }

        //[WebMethod(EnableSession = true)]
        //[ScriptMethod]
        private AjaxReponseModel<dynamic> GetEmpList()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);

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
                int maNV = int.Parse(dym.MaNV);
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

        private AjaxReponseModel<dynamic> Create()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);

                string nameStaff = dym.TenNV;
                string phone = dym.SDT;
                string email = dym.Email;
                string address = dym.DiaChi;
                string birthday = dym.NgaySinh;
                string role = dym.ChucVu;

                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = new tblNhanVien();
                    nv.TenNV = dym.TenNV;
                    nv.SDT = dym.SDT;
                    nv.Email = dym.Email;
                    nv.DiaChi = dym.DiaChi;
                    nv.NgaySinh = dym.NgaySinh;
                    nv.ChucVu = dym.ChucVu;
                    db.tblNhanViens.Add(nv);
                    db.SaveChanges();
                    response.Message = "Lưu thành công!";
                };
                return response;
            }
            catch (Exception e)
            {
                response.Message = "Lưu thất bại!";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> Update()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maNV = int.Parse(dym.MaNV);
                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = db.tblNhanViens.SingleOrDefault(w => w.MaNV == maNV);
                    nv.TenNV = dym.nameStaff;
                    nv.SDT = dym.phone;
                    nv.Email = dym.email;
                    nv.DiaChi = dym.address;
                    nv.NgaySinh = DateTime.Parse(dym.birthday);
                    nv.ChucVu = dym.role;
                    db.SaveChanges();
                    response.Message = "Lưu thành công!";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "Lưu thất bại!";
                return response;
            }
            finally
            {
            }
        }

        private AjaxReponseModel<dynamic> Delete()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            try
            {
                var data = new StreamReader(Request.InputStream).ReadToEnd();
                var dym = JsonConvert.DeserializeObject<dynamic>(data);
                int maNV = int.Parse(dym.MaNV);
                using (var db = new qlksEntities())
                {
                    tblNhanVien nv = db.tblNhanViens.SingleOrDefault(w => w.MaNV == maNV);
                    db.tblNhanViens.Remove(nv);
                    db.SaveChanges();
                    response.Message = "Xoá thành công!";
                };

                return response;
            }
            catch (Exception e)
            {
                response.Message = "Xoá thất bại!";
                return response;
            }
            finally
            {
            }
        }
    }
}