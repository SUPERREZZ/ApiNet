using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Tugas3_Api_reza.Models;

[Table("AsalSekolah")]
public partial class AsalSekolah
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Unicode(false)]
    public string Name { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("AsalSekolah")]
    public virtual ICollection<Siswa> Siswas { get; set; } = new List<Siswa>();
}
