using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Sampe.Models;

namespace Sampe
{
    public class FormularioTrocaMolde
    {
        public FormularioTrocaMolde()
        {
            this.AtividadesTM = new HashSet<AtividadeTM>();
            this.FormularioTMAtividades = new HashSet<FormularioTMAtividade>();
        }
        [Key]
        public int FormularioTrocaMoldeId { get; set; }

        public string DtRetirada { get; set; }
        public string DtColocar { get; set; }
        public string ColocarInicio { get; set; }
        public string ColocarFim { get; set; }
        public String RetirarInicio { get; set; }
        public String RetirarFim { get; set; }
        public String Supervisor { get; set; }

        [ForeignKey("Molde")]
        public int MoldeId{ get; set; }
        public Molde Molde { get; set; }

        [ForeignKey("Maquina")]
        public int MaquinaId { get; set; }
        public Maquina Maquina{ get; set; }
      
        [ForeignKey("Usuario")]
        public int  UsuarioId{ get; set; }
        public Usuario Usuario { get; set; }
        
        public ICollection<AtividadeTM> AtividadesTM { get; set; }
        public ICollection<int> AtividadeTMId { get; set; }

        public ICollection<FormularioTMAtividade> FormularioTMAtividades { get; set; }
        public ICollection<int> FormularioTMAtividadeId { get; set; }
    }
}
