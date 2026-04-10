using System.ComponentModel.DataAnnotations.Schema;
using Model.Enum;

namespace Model.Base
{
    public class BaseModel
    {
        public string? CreateBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [NotMapped] public ModelState? State { get; set; }
    }
}