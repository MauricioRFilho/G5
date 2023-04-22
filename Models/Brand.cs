using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvcMysql.Models
{
    [Table("Brand")]
    public class Brand
    {

        [Display(Name = "Código")]
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome da Marca")]
        [Column("BrandName")]
        public string BrandName { get; set; }

        [Display(Name = "Nacional")]
        [Column("National")]
        public bool National { get; set; }

        [Display(Name = "Status")]
        [Column("Status")]
        public bool Status { get; set; }

    }
}