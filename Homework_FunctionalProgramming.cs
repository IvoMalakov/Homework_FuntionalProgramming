using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Homework_FunctionalProgramming
{
    public static class Homework_FunctionalProgramming
    {
        public static void Main()
        {
            // Problem 1 - Class Student:

            var students = new[]
            {
                new Student("Sara", "Mills", 20, "045217", "+359888888888", "smills0@marketwatch.com",
                    new List<int>(), 22),
                new Student("Daniel", "Carter", 22, "045614", "+359886999999", "dcarter1@buzzfeed.com",
                    new List<int>(), 22),
                new Student("Aaron", "Gibson", 30, "045915", "+359111111111", "agibson2@house.gov",
                    new List<int>(), 3),
                new Student("Susan", "Boyd", 18, "089916", "+359222222222", "sboydi@angelfire.com",
                    new List<int>(), 4),
                new Student("Andrea", "Harper", 20, "011114", "+359333333333", "aharper9@facebook.com",
                    new List<int>(), 2),
                new Student("Edward", "Rose", 15, "000018", "None", "eroseg@deliciousdays.com",
                    new List<int>(), 4),
                new Student("Boiko", "Borisov", 55, "999920", "+35926669991", "bate_boiko@abv.bg",
                    new List<int>(), 2),
                new Student("Ivan", "Ivanov", 30, "065214", "029876543", "ivan_ivanov@abv.bg",
                    new List<int>(), 1)
            };

            // Problem 2 Studets by group:

            var studentsbyGropup =
                from student in students
                where student.GroupNumber == 2
                orderby student.FirstName
                select student;

            Console.WriteLine("Studenst by group: ");
            Console.WriteLine();

            PrintStudents(studentsbyGropup.ToList());

            // Problem 3 Studets by First and Last Name:

            var studentsByFirstNameAndLastName =
                from student in students
                where string.Compare(student.FirstName, student.LastName) < 0
                select student;

            Console.WriteLine("Studenst by First and Last name: ");
            Console.WriteLine();

            PrintStudents(studentsByFirstNameAndLastName.ToList());

            // Problem 4 Students by Age:

            var studentsByAge =
                from student in students
                where student.Age >= 18 && student.Age <= 24
                select new
                {
                    student.FirstName,
                    student.LastName,
                    student.Age
                };

            Console.WriteLine("Studenst by Age: ");
            Console.WriteLine();

            foreach (var student in studentsByAge)
            {
                Console.WriteLine("{0} {1} Age: {2}", student.FirstName, student.LastName, student.Age);
            }

            Console.WriteLine();

            //Problem 5 - Sort Students:
            //With LAMBDA expressions:

            var sortedStudentsLAMDA = students.OrderByDescending(firstName => firstName.FirstName).ThenByDescending(
                lastName => lastName.LastName);

            Console.WriteLine("Students sorted with LAMDA expressions: ");
            Console.WriteLine();

            PrintStudents(sortedStudentsLAMDA.ToList());

            //with LINQ query syntax:

            var sortedStudentsLINQ =
                from student in students
                orderby student.LastName descending
                orderby student.FirstName descending
                select student;

            Console.WriteLine("Students sorted with LINQ expressions: ");
            Console.WriteLine();

            PrintStudents(sortedStudentsLINQ.ToList());

            //Problem 6 - Filter Students by Email Domains:

            var studentsFiltredByEmail =
                from student in students
                where student.Email.Contains("@abv.bg")
                select student;

            Console.WriteLine("Students filtred by email adress: ");
            Console.WriteLine();

            PrintStudents(studentsFiltredByEmail.ToList());

            //Problem 7 - Filter Students by Phone:

            var studentsFiltredByPhone =
                from student in students
                where student.Phnone.Contains("02") || student.Phnone.Contains("+3592") ||
                      student.Phnone.Contains("+359 2")
                select student;

            Console.WriteLine("Students filtred by Phone: ");
            Console.WriteLine();

            PrintStudents(studentsFiltredByPhone.ToList());

            //Problem 8 - Excellent Students:

            foreach (var student in students)
            {
                student.Marks = GenerateResults(student.FirstName);
            }


            var excellentStudents =
                from student in students
                where student.Marks.Contains(6)
                select new
                {
                    student.FirstName,
                    student.LastName,
                    student.Marks
                };

            Console.WriteLine("Excellent students are: ");
            Console.WriteLine();

            foreach (var student in excellentStudents)
            {
                Console.Write(student.FirstName + " " + student.LastName + " ");

                foreach (var mark in student.Marks)
                {
                    Console.Write(mark + " ");
                }

                Console.WriteLine();
            }

            //Problem 9 - Weak Students:

            List<Student> weakStudents = new List<Student>();

            foreach (var student in students)
            {
                bool weakStudent = ContainsTimes(student.Marks.ToList(), 2, 2);

                if (weakStudent)
                {
                    weakStudents.Add(student);
                }
            }

            Console.WriteLine("Weak students are: ");
            Console.WriteLine();

            foreach (var student in weakStudents)
            {
                Console.Write(student.FirstName + " " + student.LastName + " ");

                foreach (var mark in student.Marks)
                {
                    Console.Write(mark + " ");
                }

                Console.WriteLine();
            }

            //Problem 10 - Students Enrolled in 2014:

            List<Student> enrolled2014Students = new List<Student>();

            foreach (var student in students)
            {
                bool enroledStudent = StudentEnroll2014(student.FactNumber);

                if (enroledStudent)
                {
                    enrolled2014Students.Add(student);
                }
            }

            Console.WriteLine("Students enrolled in 2014 are: ");
            Console.WriteLine();

            foreach (var student in enrolled2014Students)
            {
                Console.Write("{0} {1} Fakt.Numer: {2}; Results: ", student.FirstName, student.LastName, student.FactNumber);

                foreach (var mark in student.Marks)
                {
                    Console.Write(mark + " ");
                }

                Console.WriteLine();
            }
        }

        private static void PrintStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine("{0} {1} Age: {2}, FacultyNumber: {3}, Phone: {4},\n Email: {5}, GroupNumber: {6}",
                    student.FirstName, student.LastName, student.Age, student.FactNumber, student.Phnone, student.Email,
                    student.GroupNumber);
                Console.WriteLine();
            }
        }

        private static List<int> GenerateResults(string firstName)
        {
            List<int> results = new List<int>();
            int[] examResults = new int[5];
            Random rnd = new Random();

            for (int i = 0; i < examResults.Length; i++)
            {
                examResults[i] = rnd.Next(2, 7);
            }

            results.AddRange(examResults);
            return results;
        }

        public static bool ContainsTimes(this List<int> studentMarks , int result, int times)
        {
            bool weakStudent = false;
            int countWeak = 0;

            for (int i = 0; i < studentMarks.Count; i++)
            {
                if (studentMarks[i] == result)
                {
                    countWeak++;
                }
            }

            if (countWeak == times)
            {
                weakStudent = true;
            }

            return weakStudent;
        }

        public static bool StudentEnroll2014(this string faktNumber)
        {
            bool enroledStudent = false;
            char[] faktNumberAsCharArray = faktNumber.ToCharArray();

            if (faktNumberAsCharArray[4] == '1' && faktNumberAsCharArray[5] == '4')
            {
                enroledStudent = true;
            }

            return enroledStudent;
        }
    }
}
