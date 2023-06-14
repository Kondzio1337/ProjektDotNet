using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Connector
    {
        [Key]
        public int ConnectorId { get; set; }
        public int ProduktId { get; set; }
        public virtual Produkt Produkt { get; set; }
        public int KategoriaId { get; set; }
        public virtual Kategoria Kategoria { get; set; }
    }
}
