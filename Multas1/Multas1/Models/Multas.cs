using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    public class Multas
    {
        [Key]
        public int ID{ get; set; }

        public string Infração { get; set; }

        public string LocalDaMulta { get; set; }

        public decimal ValorMulta { get; set; }

        public DateTime DataDaMulta { get; set; }

        //*************************************************************************************
        //representar as chaves forasteiras que relacionam esta classe com as outras classes
        //*************************************************************************************

        //FK para a tabela dos condutores
        // ...Costraint NomeDaTermo
        // ForeignKey NomeDoAtributo references NomeDaTabela(NomeDoAtributoPK)
        [ForeignKey("Condutor")]
        public int CondutorFK { get; set; }
        public virtual Condutores Condutor { get; set; }

        //FK para as viaturas
        [ForeignKey("Viatura")]
        public int ViaturaFK { get; set; }
        public virtual  Viaturas Viatura { get; set; }

        //FK para os agentes
        [ForeignKey("Agente")]
        public int AgenteFK { get; set; }
        public virtual Agentes Agente { get; set; }

    }
}