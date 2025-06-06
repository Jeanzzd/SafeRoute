using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeRoute.Models
{
    public class RotasSegurancas
    {

        [Key]
        public int RotaId { get; set; }

  
        public string Nome { get; set; }

        public string Instrucoes { get; set; }


        public string PontosDePassagem { get; set; }


        public int AreaDeRiscoId { get; set; }

    }
}
