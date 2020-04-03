using Newtonsoft.Json;
using QLKS.Class;
using System;
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
    }
}