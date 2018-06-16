namespace Task.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentCourse")]
    public partial class StudentCourse
    {
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int? Fk_StudentId { get; set; }

        [ForeignKey("Course")]
        public int? Fk_CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }
    }
}
