using StudentManagementSystemLayeredArch.Models;
using StudentManagementSystemLayeredArch.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystemLayeredArch.Services
{
    
        public class StudentService
        {
            private readonly IStudentRepository repository;

            public StudentService(IStudentRepository repository)
            {
                this.repository = repository;
            }

            public void AddStudent(Student student)
            {
                if (student.Grade < 0 || student.Grade > 100)
                    throw new Exception("Grade must be between 0 and 100");

                repository.Add(student);
            }

            public List<Student> GetStudents()
            {
                return repository.GetAll();
            }

            public Student GetStudent(int id)
            {
                return repository.GetById(id);
            }

            public void UpdateStudent(Student student)
            {
                repository.Update(student);
            }

            public void DeleteStudent(int id)
            {
                repository.Delete(id);
            }
            }
}
