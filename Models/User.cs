using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Login { get; set; }
        [Required]
        [MaxLength(512)]
        public string Password { get; set; }

        //public string Salt { get; set; }
        [Required]
        public int IdRoli { get; set; }
    }
}
