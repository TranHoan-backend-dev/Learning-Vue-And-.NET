using System.ComponentModel.DataAnnotations;

namespace BL.Dto;

public class DepartmentDto
{
    public Guid DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}
