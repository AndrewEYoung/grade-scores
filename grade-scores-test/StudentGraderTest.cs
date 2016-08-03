using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using grade_scores;
using NSubstitute;

namespace grade_scores_test
{
    [TestClass]
    public class StudentGraderTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldFailToReturnOutputFilePathIfInputPathIsNull()
        {
            var substituteReader = Substitute.For<IStudentFileReader>();
            var substituteWriter = Substitute.For<IStudentFileWriter>();
            var studentGrader = new StudentGrader(substituteReader, substituteWriter);
            studentGrader.GetOutputFilePath(null);
        }

        [TestMethod]
        public void ShouldSuccessfullyReturnOutputFilePathIfInputPathIsShorterThanTxtExtension()
        {
            var substituteReader = Substitute.For<IStudentFileReader>();
            var substituteWriter = Substitute.For<IStudentFileWriter>();
            var studentGrader = new StudentGrader(substituteReader, substituteWriter);

            var input = "1";
            var expectedOutput = "1-graded.txt";
            var actualOutput = studentGrader.GetOutputFilePath(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShouldSuccessfullyReturnOutputFilePathIfInputPathDoesNotHaveTxtExtension()
        {
            var substituteReader = Substitute.For<IStudentFileReader>();
            var substituteWriter = Substitute.For<IStudentFileWriter>();
            var studentGrader = new StudentGrader(substituteReader, substituteWriter);

            var input = "foobar";
            var expectedOutput = "foobar-graded.txt";
            var actualOutput = studentGrader.GetOutputFilePath(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShouldSuccessfullyReturnOutputFilePathIfInputPathHasTxtExtension()
        {
            var substituteReader = Substitute.For<IStudentFileReader>();
            var substituteWriter = Substitute.For<IStudentFileWriter>();
            var studentGrader = new StudentGrader(substituteReader, substituteWriter);

            var input = "foobar.txt";
            var expectedOutput = "foobar-graded.txt";
            var actualOutput = studentGrader.GetOutputFilePath(input);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShouldSuccessfullyReturnNoStudentsIfInputHasNoStudents()
        {
            var substituteReader = Substitute.For<IStudentFileReader>();
            var substituteWriter = Substitute.For<IStudentFileWriter>();
            var studentGrader = new StudentGrader(substituteReader, substituteWriter);

            var subActualOutput = Enumerable.Empty<Student>().OrderBy( x => new Student("", "", 0));
            var expectedOutput = new List<Student>();
            
            substituteReader.ReadStudentsFromFile(Arg.Any<string>()).Returns(new List<Student> ());
            substituteWriter.WriteStudentsToFile(Arg.Any<string>(), Arg.Do<IOrderedEnumerable<Student>>(x => subActualOutput = x));

            studentGrader.GradeStudentsInFile(Path.GetTempFileName());

            var actualOutput = subActualOutput.ToList();

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShouldSuccessfullyReturnSortedStudentsIfInputStudentsAreNotSorted()
        {
            var substituteReader = Substitute.For<IStudentFileReader>();
            var substituteWriter = Substitute.For<IStudentFileWriter>();
            var studentGrader = new StudentGrader(substituteReader, substituteWriter);

            var studentFirst = new Student("XYZ", "ABC", 55);
            var studentSecond = new Student("XYZ", "AAA", 55);
            var studentThird = new Student("ABC", "ABC", 10);

            var expectedOutput = new List<Student> { studentSecond, studentFirst, studentThird };

            var subActualOutput = Enumerable.Empty<Student>().OrderByDescending(x => x);

            substituteReader.ReadStudentsFromFile(Arg.Any<string>()).Returns(new List<Student> { studentFirst, studentSecond, studentThird });
            substituteWriter.WriteStudentsToFile(Arg.Any<string>(), Arg.Do<IOrderedEnumerable<Student>>(x => subActualOutput = x));

            studentGrader.GradeStudentsInFile(Path.GetTempFileName());

            var actualOutput = subActualOutput.ToList();

            Assert.AreEqual(expectedOutput.Count, actualOutput.Count);
            for (var i = 0; i < actualOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].FirstName, actualOutput[i].FirstName);
                Assert.AreEqual(expectedOutput[i].LastName, actualOutput[i].LastName);
                Assert.AreEqual(expectedOutput[i].Grade, actualOutput[i].Grade);
            }
        }

        [TestMethod]
        public void ShouldSuccessfullyReturnSortedStudentsIfInputStudentsAreSorted()
        {
            var substituteReader = Substitute.For<IStudentFileReader>();
            var substituteWriter = Substitute.For<IStudentFileWriter>();
            var studentGrader = new StudentGrader(substituteReader, substituteWriter);

            var studentFirst = new Student("XYZ", "ABC", 80);
            var studentSecond = new Student("XYZ", "ABD", 80);
            var studentThird = new Student("ABC", "ABC", 60);

            var expectedOutput = new List<Student> { studentFirst, studentSecond, studentThird };

            var subActualOutput = Enumerable.Empty<Student>().OrderByDescending(x => x);

            substituteReader.ReadStudentsFromFile(Arg.Any<string>()).Returns(new List<Student> { studentFirst, studentSecond, studentThird });
            substituteWriter.WriteStudentsToFile(Arg.Any<string>(), Arg.Do<IOrderedEnumerable<Student>>(x => subActualOutput = x));

            studentGrader.GradeStudentsInFile(Path.GetTempFileName());

            var actualOutput = subActualOutput.ToList();

            Assert.AreEqual(expectedOutput.Count, actualOutput.Count);
            for (var i = 0; i < actualOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].FirstName, actualOutput[i].FirstName);
                Assert.AreEqual(expectedOutput[i].LastName, actualOutput[i].LastName);
                Assert.AreEqual(expectedOutput[i].Grade, actualOutput[i].Grade);
            }

        }
    }
}
