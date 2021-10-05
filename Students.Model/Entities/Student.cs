using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Model.Entities
{
    [Table("STUDENT")]
    public class Student
    {
        [Column("ID")]
        [Key]
        [DisplayName("#")]
        public long Id { get; set; }
        
        [Column("FIRST_NAME")]
        [Required]
        [MaxLength(64)]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        
        [Column("LAST_NAME")]
        [Required]
        [MaxLength(128)]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        
        [Column("DATE_OF_BIRTH")]
        [Required]
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }
        
        [Column("PERSONAL_ID")]
        [Required]
        [DisplayName("Personal ID")]
        public string PersonalId { get; set; }
    }
}