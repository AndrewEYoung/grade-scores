using System;

namespace grade_scores
{
    public class Student : IComparable<Student>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Grade { get; private set; }

        public Student(string lastName, string firstName, int grade)
        {
            LastName = lastName;
            FirstName = firstName;
            Grade = grade;
        }

        public int CompareTo(Student other)
        {
            var comparator = Grade.CompareTo(other.Grade);

            if (comparator == 0)
            {
                comparator =  -1 * LastName.CompareTo(other.LastName);

                if (comparator == 0)
                {
                    return -1 * FirstName.CompareTo(other.FirstName);
                }

            }

            return comparator;
        }

    }
}
