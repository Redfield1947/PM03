//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhongMach_Ver_1._0.Datamodel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietDonThuoc
    {
        public int ID_Thuoc { get; set; }
        public int ID_DonThuoc { get; set; }
        public decimal SoLuong { get; set; }
        public string Ghi_Chu { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
}