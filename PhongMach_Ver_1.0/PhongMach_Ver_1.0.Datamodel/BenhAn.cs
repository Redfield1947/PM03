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
    
    public partial class BenhAn
    {
        public int ID_BenhAn { get; set; }
        public int ID_BenhNhan { get; set; }
        public string TrieuChung { get; set; }
        public string ChuanDoan { get; set; }
        public int ID_donThuoc { get; set; }
        public System.DateTime ngayKham { get; set; }
        public Nullable<System.DateTime> Date_Created { get; set; }
        public Nullable<System.DateTime> Date_Edited { get; set; }
        public bool IsDelete { get; set; }
    }
}