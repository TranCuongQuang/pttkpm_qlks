using Newtonsoft.Json;
using QLKS.Class;
using System;

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

            return response;
        }
    }
}