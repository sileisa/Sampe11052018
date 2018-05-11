using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sampe.Models
{
    public class SampeContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SampeContext() : base("name=SampeContext")
        {
        }

        public System.Data.Entity.DbSet<Sampe.FormularioTrocaMolde> FormularioTrocaMoldes { get; set; }

        public System.Data.Entity.DbSet<Sampe.FormularioOrdemServico> FormularioOrdemServicoes { get; set; }

        public System.Data.Entity.DbSet<Sampe.Cargo> Cargoes { get; set; }

        public System.Data.Entity.DbSet<Sampe.Maquina> Maquinas { get; set; }

        public System.Data.Entity.DbSet<Sampe.Molde> Moldes { get; set; }

        public System.Data.Entity.DbSet<Sampe.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<Sampe.AtividadeOS> AtividadeOS { get; set; }

        public System.Data.Entity.DbSet<Sampe.AtividadeTM> AtividadeTMs { get; set; }

        public System.Data.Entity.DbSet<Sampe.FormularioOSAtividade> FormularioOSAtividade { get; set; }

        public System.Data.Entity.DbSet<Sampe.FormularioTMAtividade> FormularioTMAtividade { get; set; }

        public System.Data.Entity.DbSet<Sampe.Login> Logins { get; set; }
    }
}
