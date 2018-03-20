using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    public class Viaturas
    {
        //construtor
        public Viaturas() {
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]
        public int ID { get; set; }//Primary Key
       
        //dados de uma viatura
        public string Matricula { get; set; }
        public string Marca{ get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        
        //dados do dono de uma viatura
        public string NomeDono  { get; set; }
        public string MoradaDono{ get; set; }
        public string CodPostalDono { get; set; }

        // referencia as multas que um condutor  'recebe' numa viatura
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}