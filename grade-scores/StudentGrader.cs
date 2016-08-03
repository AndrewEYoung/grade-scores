using System;
using System.Linq;

namespace grade_scores
{
    public class StudentGrader : IStudentGrader
    {
        private readonly IStudentFileReader _reader;
        private readonly IStudentFileWriter _writer;

        public StudentGrader(IStudentFileReader reader, IStudentFileWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public void GradeStudentsInFile(string path)
        {
            var students = _reader.ReadStudentsFromFile(path);
            students = students.OrderByDescending(x => x);

            _writer.WriteStudentsToFile(GetOutputFilePath(path), students);
        }

        public string GetOutputFilePath(string inFilePath)
        {
            var expectedExtension = ".txt";
            var outputExtension = "-graded.txt";

            if (string.IsNullOrEmpty(inFilePath))
            {
                throw new ArgumentException();
            }

            if (!inFilePath.EndsWith(expectedExtension))
            {
                return inFilePath + outputExtension;
            }

            var inFilePathBare = inFilePath.Remove(inFilePath.Length - expectedExtension.Length);// Substring(inFilePath.Length - expectedExtension.Length);
            return inFilePathBare + outputExtension;
        }
    }

}
