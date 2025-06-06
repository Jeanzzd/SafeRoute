using System.ComponentModel.DataAnnotations;

namespace SafeRoute.Models
{
    public class AreasComRiscos
    {
        [Key]
        public int AreaId { get; set; }

       
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public TiposDeRiscos TipoDeRisco { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<RotasSegurancas> RotasSeguras { get; set; }
    }
}
