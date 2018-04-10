using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
// update-Database -Force
namespace Multas1.Models
{
    public class Agentes
    {
        public Agentes()
        {

            ListaDeMultas = new HashSet<Multas>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        //[RegularExpression("[A-Z][a-z]",ErrorMessage ="O {0} apenas pode conter letras e espaçoes em brancos. Cada palavra começa em maiscúla seguida de minúsculas...")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
       // [RegularExpression("[A-Z][a-z]", ErrorMessage = "O {0} apenas pode conter letras e espaçoes em brancos. Cada palavra começa em maiscúla seguida de minúsculas...")]
        public string Esquadra { get; set; }
        public string Fotografia { get; set; }

        // referencia às multas que um agente  'imite'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}