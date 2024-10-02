using System.Text.Json.Serialization;

public class PermissaoCliente
{
    public int ClienteID { get; set; }

    // Propriedade para o valor booleano
    public bool Permitido { get; set; }

    // Propriedade para o valor que vem do JSON
    [JsonPropertyName("permitido")]
    public string PermitidoString
    {
        get => Permitido ? "Sim" : "Não";
        set => Permitido = value == "Sim";
    }

    public int TipoEmailID { get; set; }
    public int EnviarParaID { get; set; }
    public int FormaEnvioRmID { get; set; }
}
