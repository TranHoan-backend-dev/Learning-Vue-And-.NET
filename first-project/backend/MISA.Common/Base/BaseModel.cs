using System.ComponentModel.DataAnnotations.Schema;
using MISA.Common.Enum;

namespace MISA.Common.Base;

public class BaseModel
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    [NotMapped] public AppEnum.ModelState? State { get; set; }
}