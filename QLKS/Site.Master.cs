﻿using System;
using System.Web.UI;

namespace QLKS
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tblTaiKhoan user = Session["UserLogin"] as tblTaiKhoan;
                lblUserLogin.InnerText = user.TenNV;
                var html = "";

                html += "<li class='active'><a href='BookingRoom.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>Đặt phòng</span></a><b class='arrow'></b></li>" +
                        "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>Trả phòng</span></a><b class='arrow'></b></li>";

                if (user.Quyen == "QuanLy")
                {
                    html += "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>QL nhân viên</span></a><b class='arrow'></b></li>" +
                                "<li class=''><a href='CustomerManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>QL khách hàng</span></a><b class='arrow'></b></li>" +
                                "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>QL dịch vụ</span></a><b class='arrow'></b></li>" +
                                "<li class=''><a href='RoomManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>QL phòng</span></a><b class='arrow'></b></li>" +
                                "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>QL trang thiết bị</span></a><b class='arrow'></b></li>" +
                                "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>QL trang thiết bị phòng</span></a><b class='arrow'></b></li>" +
                                "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>QL loại sản phẩm</span></a><b class='arrow'></b></li>";
                }

                html += "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>Báo cáo doanh thu</span></a><b class='arrow'></b></li>" +
                        "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>Thống kê phòng</span></a><b class='arrow'></b></li>" +
                        "<li class=''><a href='EmployeeManagement.aspx'><i class='menu-icon fa fa-tachometer'></i><span class='menu-text'>Thống kê nhân viên</span></a><b class='arrow'></b></li>";

                listMenu.InnerHtml = html;
            }
        }
    }
}