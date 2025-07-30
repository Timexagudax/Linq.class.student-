using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace student
{
    public class Student

    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
        public string Level { get; set; }
        public string Department { get; set; }
        public string Sex { get; set; }

    }
   
    public class Report
    {
        public int StudentId { get; set; }
        public string Course { get; set; }

        public decimal Score { get; set; }
    
        }
    }




