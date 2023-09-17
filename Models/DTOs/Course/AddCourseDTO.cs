﻿using System.ComponentModel.DataAnnotations;

namespace PortalNotas.Models.DTOs.Course
{
    public class AddCourseDTO
    {
        [Required(ErrorMessage = "Course name is required.")]
        public string CourseName { get; private set; } = string.Empty;
        public string Semester { get; set; }
        public int? ProfessorId { get; set; }
    }
}
