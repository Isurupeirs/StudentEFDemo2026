using practice_for_exam_1__2026;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace practice_for_exam_1__2026
{
    public partial class MainWindow : Window
    {
        // Create ONE context for the whole window
        SchoolContext context = new SchoolContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        // INSERT STUDENTS
        private void InsertStudents_Click(object sender, RoutedEventArgs e)
        {
            // Check if students already exist
            if (context.Students.Count() > 0)
            {
                MessageBox.Show("Students already exist in database!");
                return; // Stop the method here
            }


            context.Students.Add(new Student
            { FirstName = "Emma", LastName = "Smith", Age = 20 });

            context.Students.Add(new Student
            { FirstName = "Isuru", LastName = "Peiris", Age = 23 });

            context.Students.Add(new Student
            { FirstName = "Liam", LastName = "Jones", Age = 22 });

            context.Students.Add(new Student
            { FirstName = "Olivia", LastName = "Brown", Age = 21 });

            context.Students.Add(new Student
            { FirstName = "Noah", LastName = "Wilson", Age = 24 });

            context.SaveChanges();
            MessageBox.Show("5 Students Added Successfully!");
        }

        // LOAD STUDENTS
        private void LoadStudents_Click(object sender, RoutedEventArgs e)
        {
            //Getting all students with LINQ and This gets ALL students from the database as a List.
            var students = context.Students.ToList();//Converts results to a list

            //  Display in DataGrid and The DataGrid name from your XAML was:
            StudentDataGrid.ItemsSource = students;
        }

        // SEARCH EMMA
        private void SearchEmma_Click(object sender, RoutedEventArgs e)
        {
            //Where() Lambda Syntax
            /*var results = context.Students
              .Where(s => s.FirstName == "Emma")
              .ToList();*/

            //Where() Query Syntax
            var results = (from s in context.Students
                          where s.FirstName == "Emma" //Filters records by condition //Search
                          select s).ToList();
            //Displays results in DataGrid
            StudentDataGrid.ItemsSource = results;



        }

        // SORT BY NAME
        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            //This button will display all students sorted alphabetically by their first name using LINQ
            var results = (from s in context.Students //Look at Students table
                           orderby s.FirstName  //Sort A to Z by FirstName
                           //Get all columns //Convert to list for DataGrid
                           select s).ToList();

            // Display sorted results in DataGrid
            StudentDataGrid.ItemsSource = results;

        }

        // COUNT STUDENTS
        private void CountStudents_Click(object sender, RoutedEventArgs e)
        {
            //Count() with Query Syntax
            var count = context.Students.Count();

            //Showing count in MessageBox
            MessageBox.Show("Total Students: " + count);
        }

        // FIRST STUDENT
        private void FirstStudent_Click(object sender, RoutedEventArgs e)
        {
            //FirstOrDefault()
            var student = context.Students.FirstOrDefault();

            if (student != null)
            {
                //Showing details in MessageBox
                MessageBox.Show("First Student: " + student.FirstName + " " + student.LastName + " Age: " + student.Age);
            }
            else
            {
                MessageBox.Show("No students found!");
            }

           
        }

        // UPDATE EMMA
        private void UpdateEmma_Click(object sender, RoutedEventArgs e)
        {
            //var student = context.Students .FirstOrDefault(s => s.FirstName == "Emma");
            // Query Syntax - works but longer ✅
            var student = (from s in context.Students
                           where s.FirstName == "Emma"
                           select s).FirstOrDefault();
            if (student != null)
            {
                student.FirstName = "Emily";
                context.SaveChanges();
                MessageBox.Show("Emma updated to Emily Successfully!");
            }
            else
            {
                MessageBox.Show("Emma not found!");
            }
        }

        // DELETE EMILY
        private void DeleteEmily_Click(object sender, RoutedEventArgs e)
        {
            // var student = context.Students .FirstOrDefault(s => s.FirstName == "Emily");

            // Query Syntax version
            var student = (from s in context.Students
                           where s.FirstName == "Emily"
                           select s).FirstOrDefault();

            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
                MessageBox.Show("Emily Deleted Successfully!");
            }
            else
            {
                MessageBox.Show("Emily not found!");
            }

        }

        private void ClearDatabase_Click(object sender, RoutedEventArgs e)
        {
            // Remove all students
            var allStudents = context.Students.ToList();
            foreach (var s in allStudents)
            {
                context.Students.Remove(s);
            }
            context.SaveChanges();
            MessageBox.Show("Database Cleared!");

        }
    }
}