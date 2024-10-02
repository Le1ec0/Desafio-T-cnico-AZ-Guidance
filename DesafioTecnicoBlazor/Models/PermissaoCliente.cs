using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesafioTecnicoBlazor.Models
{
    public class PermissaoCliente
    {
        public int ClienteID { get; set; }

        [JsonPropertyName("permitido")]
        [JsonConverter(typeof(BooleanStringConverter))] // Use o conversor personalizado
        public bool Permitido { get; set; }

        public int TipoEmailID { get; set; }
        public int EnviarParaID { get; set; }
        public int FormaEnvioRmID { get; set; }

        // Propriedades adicionais para as descrições
        public string TipoEmailDescricao { get; set; }
        public string EnviarParaDescricao { get; set; }
        public string FormaEnvioDescricao { get; set; }
    }

    // Conversor personalizado para o tipo booleano
    public class BooleanStringConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return value switch
            {
                "Sim" => true,
                "Não" => false,
                _ => throw new JsonException($"Valor inesperado para conversão em booleano: {value}")
            };
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value ? "Sim" : "Não");
        }
    }
}
