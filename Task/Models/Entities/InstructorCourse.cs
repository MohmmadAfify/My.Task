namespace Task.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InstructorCourse")]
    public partial class InstructorCourse
    {
        public int Id { get; set; }

        [ForeignKey("Instructor")]
        public int? Fk_InstructorId { get; set; }

        [ForeignKey("Course")]
        public int? Fk_CourseID { get; set; }

        public virtual Course Course { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}
