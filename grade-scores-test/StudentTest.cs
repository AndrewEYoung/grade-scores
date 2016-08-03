using Microsoft.VisualStudio.TestTools.UnitTesting;
using grade_scores;

namespace grade_scores_test
{
    [TestClass]
    public class StudentTest
    {
        [TestMethod]
        public void ShouldSuccessfullyRetrieveLastNameFromStudent()
        {
            var expectedLastName = "Swan";
            var firstName = "Black";
            var grade = 50;
            var student = new Student(expectedLastName, firstName, grade);
            Assert.AreEqual(expectedLastName, student.LastName);
        }

        [TestMethod]
        public void ShouldSuccessfullyRetrieveFirstNameFromStudent()
        {
            var lastName = "Swan";
            var expectedFirstName = "Black";
            var grade = 50;
            var student = new Student(lastName, expectedFirstName, grade);
            Assert.AreEqual(expectedFirstName, student.FirstName);
        }

        [TestMethod]
        public void ShouldSuccessfullyRetrieveGradeFromStudent()
        {
            var lastName = "Swan";
            var firstName = "Black";
            var expectedGrade = 50;
            var student = new Student(lastName, firstName, expectedGrade);
            Assert.AreEqual(expectedGrade, student.Grade);
        }

        [TestMethod]
        public void ShouldSuccessfullyIndicateAStudentWithALowerGradePreceedsAStudentWithAHigherGrade()
        {
            var lesserLastName = "Swan";
            var lesserFirstName = "Black";
            var lesserGrade = 50;

            var greaterLastName = "Swan";
            var greaterFirstName = "Black";
            var greaterGrade = 51;

            var lesserStudent = new Student(lesserLastName, lesserFirstName, lesserGrade);
            var greaterStudent = new Student(greaterLastName, greaterFirstName, greaterGrade);
            Assert.IsTrue(lesserStudent.CompareTo(greaterStudent) < 0);
        }

        [TestMethod]
        public void ShouldSuccessfullyIndicateAStudentWithAnEarlierLastNamePreceedsAStudentWithALaterLastNameIfBothGradesAreEqual()
        {
            var lesserLastName = "Swan";
            var lesserFirstName = "Black";
            var lesserGrade = 50;

            var greaterLastName = "Redwing"; 
            var greaterFirstName = "Black";
            var greaterGrade = 50;

            var lesserStudent = new Student(lesserLastName, lesserFirstName, lesserGrade);
            var greaterStudent = new Student(greaterLastName, greaterFirstName, greaterGrade);
            Assert.IsTrue(lesserStudent.CompareTo(greaterStudent) < 0);
        }

        [TestMethod]
        public void ShouldSuccessfullyIndicateAStudentWithAnEarlierFirstNamePreceedsAStudentWithALaterFirstNameIfBothGradesAndLastNamesAreEqual()
        {
            var lesserLastName = "Swan";
            var lesserFirstName = "White";
            var lesserGrade = 50;

            var greaterLastName = "Swan";
            var greaterFirstName = "Black";
            var greaterGrade = 50;

            var lesserStudent = new Student(lesserLastName, lesserFirstName, lesserGrade);
            var greaterStudent = new Student(greaterLastName, greaterFirstName, greaterGrade);
            Assert.IsTrue(lesserStudent.CompareTo(greaterStudent) < 0);
        }

        [TestMethod]
        public void ShouldSuccessfullyIndicateAStudentWithAHigherGradeProceedsAStudentWithALowerGrade()
        {
            var lesserLastName = "Swan";
            var lesserFirstName = "Black";
            var lesserGrade = 50;

            var greaterLastName = "Swan";
            var greaterFirstName = "Black";
            var greaterGrade = 51;

            var lesserStudent = new Student(lesserLastName, lesserFirstName, lesserGrade);
            var greaterStudent = new Student(greaterLastName, greaterFirstName, greaterGrade);
            Assert.IsTrue(greaterStudent.CompareTo(lesserStudent) > 0);
        }

        [TestMethod]
        public void ShouldSuccessfullyIndicateAStudentWithALaterLastNameProceedsAStudentWithAnEarlierLastNameIfBothGradesAreEqual()
        {
            var lesserLastName = "Toucan"; 
            var lesserFirstName = "Black";
            var lesserGrade = 50;

            var greaterLastName = "Swan";
            var greaterFirstName = "Black";
            var greaterGrade = 50;

            var lesserStudent = new Student(lesserLastName, lesserFirstName, lesserGrade);
            var greaterStudent = new Student(greaterLastName, greaterFirstName, greaterGrade);
            Assert.IsTrue(greaterStudent.CompareTo(lesserStudent) > 0);
        }

        [TestMethod]
        public void ShouldSuccessfullyIndicateAStudentWithALaterFirstNameProceedsAStudentWithAnEarlierFirstNameIfBothGradesAndLastNamesAreEqual()
        {
            var lesserLastName = "Swan";
            var lesserFirstName = "White";
            var lesserGrade = 50;

            var greaterLastName = "Swan";
            var greaterFirstName = "Black";
            var greaterGrade = 50;

            var lesserStudent = new Student(lesserLastName, lesserFirstName, lesserGrade);
            var greaterStudent = new Student(greaterLastName, greaterFirstName, greaterGrade);
            Assert.IsTrue(greaterStudent.CompareTo(lesserStudent) > 0);
        }

        [TestMethod]
        public void ShouldSuccessfullyIndicateStudentsAreEqualIfStudentsAreEqual()
        {
            var lesserLastName = "Swan";
            var lesserFirstName = "Black";
            var lesserGrade = 50;

            var lesserStudent = new Student(lesserLastName, lesserFirstName, lesserGrade);
            Assert.IsTrue(lesserStudent.CompareTo(lesserStudent) == 0);
        }
    }
}
