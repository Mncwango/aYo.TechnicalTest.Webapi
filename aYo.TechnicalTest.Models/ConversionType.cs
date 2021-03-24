
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace aYo.TechnicalTest.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConversionType
    {
        Temperature,
        Length,
        Speed,
        Mass,
        Volume,
        Pressure
    }
}