using Newtonsoft.Json;
using QLKS.Class;
using System;
using System.IO;

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
            var data = (new StreamReader(Request.InputStream).ReadToEnd());
            var dym = JsonConvert.DeserializeObject<dynamic>(data);

            var a = Request.Params["a"];

            using (qlksEntities db = new qlksEntities)
            {
                tblDichVu tb = new tblDichVu();
                tb.DonGia = 1;

                db.tblDichVus.Add(tb);
                
                db.SaveChanges();

               
            };

                return response;
        }
    }
}