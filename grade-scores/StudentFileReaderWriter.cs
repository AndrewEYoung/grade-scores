using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace grade_scores
{
    public class StudentFileReaderWriter : IStudentFileReader, IStudentFileWriter
    {
        public IEnumerable<Student> ReadStudentsFromFile(string path)
        {
            return File.ReadAllLines(path).Select(ReadLineAsStudent).Where(s => s != null);
        }

        public void WriteStudentsToFile(string path, IEnumerable<Student> students)
        {
            File.WriteAllLines(path, students.Select(s => WriteStudentToString(s)));
            Console.WriteLine("Finished: created {0}", Path.GetFileName(path));
        }

        public string WriteStudentToString(Student student)
        {
            var output = string.Join(", ", student.LastName, student.FirstName, student.Grade);
            Console.WriteLine(output);
            return output;
        }

        public Student ReadLineAsStudent(string line)
        {
            try
            {
                var strings = line.Split(new [] { ',' }, 3, StringSplitOptions.None);

                var lastName = strings[0].Trim();
                var firstName = strings[1].Trim();
                var grade = int.Parse(strings[2]);

                return new Student(lastName, firstName, grade);
            }
            catch (Exception)
            {
                // If the line isn't valid, return a null student to be filtered out during sorting
                return null;
            }
        }
    }
}
