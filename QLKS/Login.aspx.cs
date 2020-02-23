using System;

namespace QLKS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void login_Click(object sender, EventArgs e)
        {
            using (var db = new qlksEntities())
            {
                tblDichVu dichVu = new tblDichVu();
                dichVu.TenDV = "A";
                dichVu.DonGia = 2000;
                db.tblDichVus.Add(dichVu);
                db.SaveChanges();
            }
        }
    }
}