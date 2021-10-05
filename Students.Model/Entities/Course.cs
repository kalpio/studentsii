using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Model.Entities
{
    [Table("COURSE")]
    public class Course
    {
        [Column("ID")]
        [Key]
        public long Id {  get; set; }

        [Column("NUMBER")]
        [Required]
        [MaxLength(64)]
        public long Number { get; set; }
        
        [Column("NAME")]
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        
        [Browsable(false)]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}