﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [Table("comentario")]
    public partial class Comentario
    {
        public Comentario() {
            Data = DateTime.Now;
            IdUsu = 1;
        } 
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idTopico")]
        public int IdTopico { get; set; }
        [Column("data", TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Column("idUsu")]
        public int IdUsu { get; set; }
        [Required]
        [Column("dsc")]
        [StringLength(2000)]
        public string Dsc { get; set; }

        [ForeignKey("IdTopico")]
        [InverseProperty("Comentario")]
        public virtual Topico IdTopicoNavigation { get; set; }
        [ForeignKey("IdUsu")]
        [InverseProperty("Comentario")]
        public virtual Usuario IdUsuNavigation { get; set; }
    }
}