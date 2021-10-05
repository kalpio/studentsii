using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Model.Entities
{
    [Table("ENROLLMENT")]
    public class Enrollment
    {
        [Column("ID")]
        [Key]
        public long Id { get; set; }
       
        [Column("STUDENT_ID")]
        [ForeignKey("FK_ENROLLMENT_STUDENT_ID")]
        public long StudentId { get; set; }
        public virtual Student Student { get; set; }
        
        [Column("COURSE_ID")]
        [ForeignKey("FK_ENROLLMENT_COURSE_COURSE_ID")]
        public virtual long CourseId { get; set; }
        public virtual Course Course { get; set; }
        
        [Column("ENROLLMENT_DATE")]
        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}