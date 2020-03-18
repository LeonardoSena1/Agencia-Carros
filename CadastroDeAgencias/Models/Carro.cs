using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDeAgencias.Models
{
    [Table("Carro")]
    public class Carro
    {
        [Key]
        public int CarroId { get; set; }

        [Required(ErrorMessage = "O nome do carro é obrigatório" ,AllowEmptyStrings = false)]
        public string Modelo { get; set; }


        [Required(ErrorMessage = "O nome do Fabricante é obrigatório" ,AllowEmptyStrings = false)]
        public string Fabricante { get; set; }


        [Required(ErrorMessage = "A Placa do veiculo é obrigatório" ,AllowEmptyStrings = false)]
        public string Placa { get; set; }


        [Required(ErrorMessage = "Cor do veiculo é obrigatório" ,AllowEmptyStrings = false)]
        public string Cor { get; set; }


        public int AgenciaId { get; set; }
        public virtual Agencia Agencia { get; set; }
    }
}