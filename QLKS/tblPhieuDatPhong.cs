//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tblPhieuDatPhong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPhieuDatPhong()
        {
            this.tblDichVuPhongs = new HashSet<tblDichVuPhong>();
            this.tblSanPhamPhongs = new HashSet<tblSanPhamPhong>();
        }
    
        public int MaPhieuDP { get; set; }
        public Nullable<int> MaKH { get; set; }
        public Nullable<int> MaPhong { get; set; }
        public Nullable<int> MaNV { get; set; }
        public Nullable<System.DateTime> NgayBD { get; set; }
        public Nullable<System.DateTime> NgayKT { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<bool> TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblDichVuPhong> tblDichVuPhongs { get; set; }
        public virtual tblKhachHang tblKhachHang { get; set; }
        public virtual tblPhong tblPhong { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSanPhamPhong> tblSanPhamPhongs { get; set; }
        public virtual tblNhanVien tblNhanVien { get; set; }
    }
}
