using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagementFluentAPI.Data;
using StudentCourseManagementFluentAPI.Models;

namespace StudentCourseManagementFluentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(int studentId, int courseId)
        {
            var enrollment = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.StudentCourses.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok("Enrolled Successfully");
        }
    }
}
