using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioTecnicoAZGuidance.Models
{
    public class PermissaoCliente
    {
        [Key]
        public int ClienteID { get; set; }
        public bool Permitido { get; set; }

        public int TipoEmailID { get; set; }
        public int EnviarParaID { get; set; }
        public int FormaEnvioRmID { get; set; }

        // Propriedades de navegação
        [ForeignKey("TipoEmailID")]
        public virtual PermissaoTipo TipoEmail { get; set; }

        [ForeignKey("EnviarParaID")]
        public virtual PermissaoEnviarPara EnviarPara { get; set; }

        [ForeignKey("FormaEnvioRmID")]
        public virtual PermissaoFormaEnvio FormaEnvio { get; set; }
    }

    public class PermissaoTipo
    {
        [Key]
        public int TipoEmailID { get; set; }
        public string Descricao { get; set; }
    }

    public class PermissaoEnviarPara
    {
        [Key]
        public int EnviarParaID { get; set; }
        public string Descricao { get; set; }
    }

    public class PermissaoFormaEnvio
    {
        [Key]
        public int FormaEnvioRmID { get; set; }
        public string Descricao { get; set; }
    }
}
