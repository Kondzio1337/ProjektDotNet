using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Produkt
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        // public int CategoryID { get; set; }
        [Required]
        [InverseProperty("Produkt")]
        public virtual ICollection<Connector> ProduktyKategorie { get; set; }
    }
}
