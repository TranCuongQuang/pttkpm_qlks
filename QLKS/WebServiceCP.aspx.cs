using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace QLKS
{
    public partial class WebServiceCP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var action = Request.Params["Action"];
            //Response.ContentType = "application/json; charset=utf-8";

            //switch (action)
            //{
            //    case "GetEmp":
            //        Response.Write(JsonConvert.SerializeObject(GetEmpList()));
            //        Response.End();
            //        break;

            //    default:

            //        Response.End();
            //        break;
            //}
        }
        [WebMethod(EnableSession = true)]
        //[WebMethod]
        [ScriptMethod]
        public static dynamic GetEmpList(string userName, string passWord)
        {
            try
            {
                var ctx = new qlksEntities();
                var emp = ctx.tblNhanViens.Find();
                return emp;
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
            }
        }
    }
}