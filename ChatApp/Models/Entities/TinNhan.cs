namespace ChatApp.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinNhan")]
    public partial class TinNhan
    {
        [Key]
        public int IdTinNhan { get; set; }

        public string NoiDung { get; set; }

        [StringLength(50)]
        public string NguoiGoi { get; set; }

        [StringLength(50)]
        public string NguoiNhan { get; set; }

        public DateTime? ThoiGianGoi { get; set; }

        public int IdNhom { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual NguoiDung NguoiDung1 { get; set; }

        public virtual Nhom Nhom { get; set; }
    }
}
