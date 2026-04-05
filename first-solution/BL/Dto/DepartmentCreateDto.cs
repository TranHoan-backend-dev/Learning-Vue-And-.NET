using System.ComponentModel.DataAnnotations;

namespace BL.Dto;

public class DepartmentCreateDto
{
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [MaxLength(10)]
    public string? PhoneNumber { get; set; }
}
