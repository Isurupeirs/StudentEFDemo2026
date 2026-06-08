using System;

//import this
using System.Data.Entity;

namespace practice_for_exam_1__2026
{
    //class inherits from DbContext:
    class SchoolContext : DbContext
    {
        //DbSet represents your Students table
        public DbSet<Student> Students { get; set; }
    }
}
