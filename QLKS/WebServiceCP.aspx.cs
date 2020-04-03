﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using QLKS.Class;

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

                default:

                    Response.End();
                    break;
            }
        }
        [WebMethod(EnableSession = true)]
        //[WebMethod]
        [ScriptMethod]
        public AjaxReponseModel<dynamic> GetEmpList()
        {
            var response = new AjaxReponseModel<dynamic>(AjaxReponseStatusEnum.Success);
            var data = new StreamReader(Request.InputStream).ReadToEnd();
            var dym = JsonConvert.DeserializeObject<dynamic>(data);

            //string userName1 = dym.userName;
            //string passWord1 = dym.passWord;
            try
            {
                using (var ctx = new qlksEntities())
                {
                    var emp = ctx.tblNhanViens.ToList();
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
    }
}