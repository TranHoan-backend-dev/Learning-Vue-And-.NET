using System.ComponentModel.DataAnnotations;
using MISA.BL.Enums;

namespace MISA.BL.DTOs.Response;

public class ApiResponse
{
    [property: Required] public int Status { get; set; }
    public string? Message { get; set; }

    public Object? Data { get; set; }
}