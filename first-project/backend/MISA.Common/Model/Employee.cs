using System.ComponentModel.DataAnnotations;
using MISA.Common.Attributes;
using MISA.Common.Base;
using MISA.Common.Resources;

namespace MISA.Common.Model;

public class Employee : BaseModel
{
    [Key]
    public Guid EmployeeId { get; set; }
    public required string Name { get; set; }
    [CheckDuplicate(ResourcesVN.DuplicatedPhone)]
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string HiringCampaign { get; set; }
    public required string HiringPosition { get; set; }
    public required string HiringRound { get; set; }
    public required string Review { get; set; }
    public DateTime HiringAt { get; set; }
    public bool Status { get; set; }
}