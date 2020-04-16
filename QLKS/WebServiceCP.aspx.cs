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
    }
}