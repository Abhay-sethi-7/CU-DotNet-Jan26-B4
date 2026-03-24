using StudentManagementSystemLayeredArch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentManagementSystemLayeredArch.Repositories
{
    
        public interface IStudentRepository
        {
            void Add(Student student);
            List<Student> GetAll();
            Student GetById(int id);
            void Update(Student student);
            void Delete(int id);
        }
    
}
