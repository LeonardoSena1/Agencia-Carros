using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDeAgencias.Models
{
    
    [Table("Agencia")]
    public class Agencia
    {
        [Key]
        public int AgenciaId { get; set; }

        [Required(ErrorMessage = "O nome é um campo obrigatório", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O endereço é um campo obrigatório", AllowEmptyStrings = false)]
        public string Endereco { get; set; }

        public string HorarioAberto { get; set; }

        public string HorarioFechado { get; set; }


        public List<Carro> Carros { get; set; }

    }
}
