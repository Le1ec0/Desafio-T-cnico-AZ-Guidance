namespace DesafioTecnicoAZGuidance.Models
{
    public class PermissaoCliente
    {
        public int ClienteID { get; set; }
        public bool Permitido { get; set; }
        public int TipoEmailID { get; set; }
        public int EnviarParaID { get; set; }
        public int FormaEnvioRmID { get; set; }
    }
}
