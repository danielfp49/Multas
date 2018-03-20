using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Multas1.Models
{
    public class MultasDb : DbContext // classe especial que representa a base de dados

    {
       
        public MultasDb() : base("name=MultasDBConnectionString")  {
            
            }          
        public virtual DbSet<Multas> Multas { get; set; } //representa a tabela das multas
        public virtual DbSet<Condutores> Condutores { get; set; } // representa a tabela dos condutores
        public virtual DbSet<Agentes> Agentes { get; set; } // representa a tabela dos agentes
        public virtual DbSet<Viaturas> Viaturas { get; set; }  // representa a tabela das viaturas



    }
}