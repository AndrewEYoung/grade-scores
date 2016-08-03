using System.Collections.Generic;

namespace grade_scores
{
    public interface IStudentFileReader
    {
        IEnumerable<Student> ReadStudentsFromFile(string path);
    }
}
