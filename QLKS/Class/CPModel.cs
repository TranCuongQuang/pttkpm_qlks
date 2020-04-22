using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLKS.Class
{
    public class EquimentRoom
    {
        public int MaTTBP { get; set; }
        public int? MaThietBi { get; set; }
        public int? MaPhong { get; set; }
        public int SoLuong { get; set; }
    }

    public class Equiment
    {
        public int MaTB { get; set; }
        public string TenTB { get; set; }
        public bool TinhTrang { get; set; }
    }

    public class Product
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGia { get; set; }
    }

    public class Service
    {
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public decimal DonGia { get; set; }
    }

    public class Room
    {
        public int MaPhong { get; set; }
        public string TenPhong { get; set; }
        public bool TrangThai { get; set; }
        public decimal DonGia { get; set; }
    }

    public class Customer
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
    }

    public class Employee
    {
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string SDT { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string ChucVu { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
    }
}