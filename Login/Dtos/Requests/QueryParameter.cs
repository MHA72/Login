using System.ComponentModel.DataAnnotations;

namespace Login.Dtos.Requests;

public enum SortType
{
    Asc,
    Desc
}

public record Search([Required(AllowEmptyStrings = false)] string Text, [Required] [MinLength(1)] string[] Fields);

public record SortParameter(string ColumnName, SortType Type);

public record QueryParameter(DateTime? FromCreateDate, DateTime? ToCreateDate, int Skip, int Take,
    IEnumerable<SortParameter>? Sort);