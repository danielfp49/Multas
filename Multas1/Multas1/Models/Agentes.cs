using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    public class Agentes
    {
        public Agentes()
        {

            ListaDeMultas = new HashSet<Multas>();
        }
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public int Esquadra { get; set; }
        public string Fotografia { get; set; }

        // referencia às multas que um agente  'imite'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}