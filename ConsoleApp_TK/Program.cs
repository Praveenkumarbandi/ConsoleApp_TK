using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp_TK
{
    class Program
    {
        class Student
        {
            public int StudentID { get; set; }
            public string StudentName { get; set; }
            public string SubjectName { get; set; }
            public int Marks { get; set; }

            public int TotalMarks { get; set; }
        }

        static void Main(string[] args)
        {
            ShowDuplicateMobileNumbers();

            DisplayStudentMarks();
        }

        /// <summary>
        /// Consider a list <Student > of students having name and marks in each subject ordered by student name
        //        Class student
        //Student name
        //Student name
        //Marks
        //Complete the below function implementation to display the student name and total marks for each student on console(Console.Writeline)
        //Public Void Display Student Marks(List<Student> student list)
        /// </summary>
        public static void DisplayStudentMarks()
        {
            List<Student> studentlist = new List<Student>() {
                  new Student { StudentName = "Raju", SubjectName = "English", Marks = 50 } ,
                  new Student { StudentName = "Praveen", SubjectName = "English", Marks = 60 },
                  new Student { StudentName = "Raghu", SubjectName = "Maths", Marks = 80 },
                  new Student { StudentName = "Sathosh", SubjectName = "Maths", Marks = 70 },
                  new Student { StudentName = "Sathosh", SubjectName = "Maths", Marks = 80 },
                  new Student { StudentName = "Raju", SubjectName = "Maths", Marks =60 }
            };


            if (studentlist.Count > 0)
            {
                foreach (Student eachstudent in studentlist.GroupBy(obj => obj.StudentName).Select(obj => obj.First()).OrderBy(obj => obj.StudentName))
                {
                    List<Student> studentlistTEMP = new List<Student>();
                    studentlistTEMP = studentlist.Where(obj => obj.StudentName == eachstudent.StudentName).ToList();

                    if (studentlistTEMP.Count > 0)
                    {
                        studentlistTEMP.ForEach(obj =>
                        {
                            obj.TotalMarks = studentlistTEMP.OrderBy(obj => obj.StudentName).Sum(g => g.Marks);
                        });
                    }
                    Console.WriteLine("Student Name:" + eachstudent.StudentName + " " + "Total Marks:" + eachstudent.TotalMarks);
                }
            }
        }

        /// <summary>
        ///        //Write a program to find list of duplicate mobile no in list
        //Assume there is a list < string > populated with mobile no.Now in this list there can be some numbers repeated.Output of the program should have
        //list<Sting> of mobile no which is repeated at least once.
        /// </summary>
        private static void ShowDuplicateMobileNumbers()
        {
            List<string> numbersduplicate = new List<string>();
            List<string> numbers = new List<string>() { "9849298705", "7075028123", "9849298705", "9856985632", "9849298705" };
            numbersduplicate = numbers.GroupBy(x => x).Where(g => g.Count() > 1).Select(x => x.Key).ToList();
            //numbersduplicate = numbers.GroupBy(x => x).SelectMany(g => g.Skip(1)).ToList();

            var item2ItemCount = numbers.GroupBy(item => item).ToDictionary(x => x.Key, x => x.Count()).Select(x => x.Key).ToList();

        
            for (int i = 0; i < numbers.Count; i++)
            {
                bool duplicate = false;
                for (int z = 0; z < i; z++)
                {
                    if (numbers[z] == numbers[i])
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (duplicate)
                {
                    if (numbersduplicate.Count > 0)
                    {
                        for (int j = 0; i < numbersduplicate.Count; j++)
                        {
                            if (numbersduplicate[j] != numbers[i])
                            {
                                numbersduplicate.Add(numbers[i]);
                            }
                        }
                    }
                    else
                    {
                        numbersduplicate.Add(numbers[i]);
                    }
                }
            }


            var myList = new List<string>();
            var duplicates = new List<string>();

            foreach (var s in numbers)
            {
                if (!myList.Contains(s))
                    myList.Add(s);
                else
                    duplicates.Add(s);
            }

            // show list without duplicates 
            foreach (var s in myList)
                Console.WriteLine(s);

            // show duplicates list
            foreach (var s in duplicates)
                Console.WriteLine(s);
        }

        private static List<Student> GetStudentMarksDetails(List<Student> studentlist)
        {
            List<Student> studentlistTEMP = new List<Student>();
            foreach (Student eachstudent in studentlist.OrderBy(obj => obj.StudentName).GroupBy(obj => obj.StudentID).Select(obj1 => obj1.First()))
            {
                studentlistTEMP = studentlist.Where(obj => obj.StudentID == eachstudent.StudentID).ToList();
                studentlistTEMP.ForEach(obj =>
                {
                    obj.TotalMarks = studentlistTEMP.OrderBy(obj => obj.StudentID).Sum(g => g.Marks);
                });
                Console.WriteLine("Student Name:" + eachstudent.StudentName + " " + "Total Marks:" + eachstudent.TotalMarks);
            }
            return studentlist;
        }
    }
}
