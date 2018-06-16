namespace Task.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InstructorStudent")]
    public partial class InstructorStudent
    {
        public int Id { get; set; }

        [ForeignKey("Instructor")]
        public int? Fk_InstructorId { get; set; }

        [ForeignKey("Student")]
        public int? Fk_StudentId { get; set; }

        public virtual Instructor Instructor { get; set; }

        public virtual Student Student { get; set; }
    }
}
