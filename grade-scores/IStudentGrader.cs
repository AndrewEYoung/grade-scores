namespace grade_scores
{
    public interface IStudentGrader
    {
        void GradeStudentsInFile(string path);
        string GetOutputFilePath(string inFilePath);
    }
}
