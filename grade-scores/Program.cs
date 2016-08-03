using System;

namespace grade_scores
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0 || args.Length > 1 )
            {
                var appName = AppDomain.CurrentDomain.FriendlyName;
                Console.WriteLine("Incorrect usage");
                Console.WriteLine("Usage: {0} C:\\names.txt", appName);
                Console.WriteLine("Output: C:\\names-graded.txt");
                return;
            }

            var studentFileReaderWriter = new StudentFileReaderWriter();
            var grader = new StudentGrader(studentFileReaderWriter, studentFileReaderWriter);
            grader.GradeStudentsInFile(args[0]);
        }
    }
}
