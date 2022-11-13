using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Evico.Api.Enums;

namespace Evico.Api.InputModels;

public abstract class BaseSearchInputModel
{
    [StringLength(64)]
    public String? SearchQuery { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SearchSortOrderType SortOrder { get; set; } 
        = SearchSortOrderType.Asc;
    [MaxLength(100)]
    public int Limit { get; set; } = 10;
    public int Offset { get; set; }
}