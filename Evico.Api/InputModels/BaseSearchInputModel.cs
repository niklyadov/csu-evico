using System.ComponentModel.DataAnnotations;
using Evico.Api.Enums;

namespace Evico.Api.InputModels;

public abstract class BaseSearchInputModel
{
    [StringLength(64)]
    public String? SearchQuery { get; set; }
    public SearchSortOrderType SortOrder { get; set; } 
        = SearchSortOrderType.Asc;
    [Range(1, 100)]
    public int Limit { get; set; } = 10;

    public int Offset { get; set; } = 0;
}