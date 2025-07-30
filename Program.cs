using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace student
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>()
            {
                new Student() { Name = "Muiz Aebayo", Age = 20, Department = "IT", Id = 100, Level = "200", Sex = "Male" },
               new Student() { Name = "Bright jimoh", Age = 24, Department = "zology", Id = 101, Level = "200", Sex = "Female" },
               new Student() { Name = "Zainab vasco", Age = 25, Department = "chemistry", Id = 102, Level = "200", Sex = "Female" },
               new Student() { Name = "Timex buhari", Age = 26, Department = "geology", Id = 103, Level = "300", Sex = "Male" },
               new Student() { Name = "Emperor John", Age = 28, Department = "English", Id = 104, Level = "400", Sex = "Female" },
               new Student() { Name = "Busola Shina ", Age = 27, Department = "Biology", Id = 105, Level = "500", Sex = "Female" }

            };
            List<Report> reports = new List<Report>()
             {
                 new Report(){ StudentId = 100, Course = "Mathematics", Score = 75.34m },
                 new Report(){ StudentId = 101, Course = "Mathematics", Score = 45.34m },
                 new Report(){ StudentId = 102, Course = "Mathematics", Score = 55.34m },
                 new Report(){ StudentId = 103, Course = "Mathematics", Score = 35.34m },
                 new Report(){ StudentId = 104, Course = "Mathematics", Score = 65.34m },
                 new Report(){ StudentId = 105, Course = "Mathematics", Score = 38.34m },
                 new Report(){ StudentId = 100, Course = "Yoruba", Score = 55.34m },
                 new Report(){ StudentId = 101, Course = "Yoruba", Score = 85.34m },
                 new Report(){ StudentId = 102, Course = "Yoruba", Score = 65.34m },
                 new Report(){ StudentId = 103, Course = "Yoruba", Score = 45.34m },
                 new Report(){ StudentId = 104, Course = "Yoruba", Score = 75.34m },
                 new Report(){ StudentId = 105, Course = "Yoruba", Score = 55.34m },
                 new Report(){ StudentId = 100, Course = "Civic", Score = 35.34m },
                 new Report(){ StudentId = 100, Course = "Civic", Score = 45.34m },
                new Report(){ StudentId = 100, Course = "Civic", Score = 55.34m },
                 new Report(){ StudentId =100, Course = "Civic", Score = 65.34m },
                   new Report(){ StudentId =100, Course = "Civic", Score = 75.34m },
                     new Report(){ StudentId =100, Course = "Civic", Score = 85.34m },
            };
            //select Student based on sex 
            //linq method 
            var maleStudents = from m in students
                               join r in reports on m.Id equals r.StudentId
                               where m.Sex == "Male" && r.Course == "Civic" && r.Score > 60
                               select new { m.Name, r.Course, r.Score };

            var maleStudentQuery = students.Where(x => x.Sex == "Male");

            foreach (var student in maleStudents)
                Console.WriteLine($"{student.Name}, {student.Course}, {student.Score}");

            // average student score of all student in a course
            var  studentsAverage   = from r in reports 
                                 join s in students on r.StudentId equals s.Id
                                 group r by r.StudentId into g
                                 select new
                                 {
                                     StudentId = g.Key,
                                     AverageeScore = Math.Round(g.Average(x => x.Score), 2).ToString("F2"),
                                     StudentName = students.FirstOrDefault(s => s.Id == g.Key)?.Name
                                 };
            //rank students based on their average scores 
            var rankStudents = from r in studentsAverage
                               orderby Convert.ToDecimal(r.AverageeScore) descending
                               select new
                               {
                                   r.StudentId,
                                   r.StudentName,
                                   r.AverageeScore,
                                   Rank = studentsAverage.ToList().IndexOf(r) + 1
                               };

            //rank students based on their average scores 
            var rankstudents = from r in studentsAverage
                               orderby Convert.ToDecimal(r.AverageeScore) descending
                               select new
                               {
                                   r.StudentId,
                                   r.StudentName,
                                   r.AverageeScore,
                                  // Rank = studentsAverage.ToList().IndexOf(r) + 1
                               };
            int rank = 1;
            foreach (var student in rankStudents)
            {
                Console.WriteLine($"student ID: {student.StudentId}, Name: {student.StudentName}, Average Score:{student.AverageeScore}, Rank: {rank++}");

                var studentReports = reports.Where(r => r.StudentId == student.StudentId).ToList();
                foreach (var report in studentReports)

                    Console.WriteLine($"Course: {report.Course}, score: {report.Score}, Grade: {ComputeGrade (report.Score)}");

                Console.WriteLine();
            }
            
            //Compute the grade of (each basedon their course score
              string ComputeGrade(decimal score)
            {
                if (score >= 60) return "A";
                else if (score >= 40 && score < 60) return "B";
                else return "C";
            }
            // Find the best student (higest average score)
            var bestStudent = rankStudents.FirstOrDefault();

            if (bestStudent != null)
            {
                Console.WriteLine("******** BEST STUDENT ********");
                Console.WriteLine($"Student ID: {bestStudent.StudentId}");
                Console.WriteLine($"Name: {bestStudent.StudentName}");
                Console.WriteLine($"Average Score: {bestStudent.AverageeScore}");
                Console.WriteLine($"Rank: 1");

                var bestStudentReports = reports.Where(r => r.StudentId == bestStudent.StudentId).ToList();
                foreach (var report in bestStudentReports)
                {
                    Console.WriteLine($"Course: {report.Course}, Score: {report.Score}, Grade:ComputeGrade{report.Score}");
                }

                Console.WriteLine(" keep it up , you can do better  ");

            }




        }
    }
}
