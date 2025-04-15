using LMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.DTOs
{
    public class BookQueryParamsDTO
    {
        [EnumDataType(typeof(BookStatus), ErrorMessage = "Invalid status value.")]
        public string? Status { get; set; }

        [RegularExpression("^(Author|Genre)$", ErrorMessage = "Invalid sort field.")]
        public string? SortBy { get; set; }

        public bool? IsAscending { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than 0.")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; set; } = 10;
    }
}
