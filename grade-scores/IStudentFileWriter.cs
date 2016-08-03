using System.Collections.Generic;

namespace grade_scores
{
    public interface IStudentFileWriter
    {
        void WriteStudentsToFile(string path, IEnumerable<Student> students);
    }
}
