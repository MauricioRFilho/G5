using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvcMysql.Models
{
    [Table("Users")]
    public class Users
    {

        [Display(Name = "Código")]
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Column("Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Administrador")]
        [Column("Admin")]
        [Required]
        public bool? Admin { get; set; }

    }
}