namespace Task.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            InstructorCourses = new HashSet<InstructorCourse>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Code must be numeric")]
        public int Code { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }

        [Column("Hours") ]
        [Range(1,40,ErrorMessage ="40 Hrs Max")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hours must be numeric")]
        public int? Hours { get; set; }

        public Boolean HasInstructor { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorCourse> InstructorCourses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
