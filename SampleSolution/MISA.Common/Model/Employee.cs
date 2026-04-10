using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Attributes;
using Model.Base;
using Model.Enum;

namespace Model.Model
{
    [ConfigTable("Employees")]
    public class Employee : BaseModel
    {
        /// <summary>
        /// Id của nhân viên
        /// </summary>
        [Key]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [CheckDuplicate("Ma nhan vien da bi trung")]
        public required string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên nhân viên
        /// </summary>
        public required string FullName { get; set; }

        /// <summary>
        /// Gioi tinh
        /// </summary>
        public EnumGender Gender { get; set; }

        /// <summary>
        /// Email nhan vien
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Tuoi cua nhan vien
        /// </summary>
        public int Age { get; set; }
    }
}