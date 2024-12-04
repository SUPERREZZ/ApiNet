using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Tugas3_Api_reza.Models;

[Table("Siswa")]
public partial class Siswa
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string Sex { get; set; } = null!;

    [Column("AsalSekolahID")]
    public int AsalSekolahId { get; set; }
    [JsonIgnore]
    [ForeignKey("AsalSekolahId")]
    [InverseProperty("Siswas")]
    public virtual AsalSekolah AsalSekolah { get; set; } = null!;
}
