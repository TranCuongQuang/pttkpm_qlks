﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLKS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class qlksEntities : DbContext
    {
        public qlksEntities()
            : base("name=qlksEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblDichVuPhong> tblDichVuPhongs { get; set; }
        public virtual DbSet<tblKhachHang> tblKhachHangs { get; set; }
        public virtual DbSet<tblNhanVien> tblNhanViens { get; set; }
        public virtual DbSet<tblPhieuDatPhong> tblPhieuDatPhongs { get; set; }
        public virtual DbSet<tblPhong> tblPhongs { get; set; }
        public virtual DbSet<tblSanPham> tblSanPhams { get; set; }
        public virtual DbSet<tblSanPhamPhong> tblSanPhamPhongs { get; set; }
        public virtual DbSet<tblTaiKhoan> tblTaiKhoans { get; set; }
        public virtual DbSet<tblTrangThietBiPhong> tblTrangThietBiPhongs { get; set; }
        public virtual DbSet<tblDichVu> tblDichVus { get; set; }
        public virtual DbSet<tblTrangThietBi> tblTrangThietBis { get; set; }
    
        public virtual ObjectResult<GetUser_Result> GetUser(string user)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUser_Result>("GetUser", userParameter);
        }
    }
}
