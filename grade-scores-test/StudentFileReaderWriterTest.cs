using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using grade_scores;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace grade_scores_test
{
    [TestClass]
    public class StudentFileReaderWriterTest
    {
        private readonly StudentFileReaderWriter _studentFileReaderWriter;

        public StudentFileReaderWriterTest()
        {
            _studentFileReaderWriter = new StudentFileReaderWriter();
        }

        [TestMethod]
        public void ShouldSuccessfullyWriteCompleteStudentToString()
        {
            var student = new Student("Black", "Swan", 50);
            var expectedOutput = "Black, Swan, 50";
            var actualOutput = _studentFileReaderWriter.WriteStudentToString(student);

            Assert.AreEqual(expectedOutput, actualOutput);   
        }

        [TestMethod]
        public void ShouldSuccessfullyWriteStudentWithEmptyStringsToString()
        {
            var student = new Student("", "", 50);
            var expectedOutput = ", , 50";
            var actualOutput = _studentFileReaderWriter.WriteStudentToString(student);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ShouldSuccessfullyReadCompleteStudentFromString()
        {
            var studentText = "Black, Swan, 50";
            var expectedOutput = new Student("Black", "Swan", 50);
            var actualOutput = _studentFileReaderWriter.ReadLineAsStudent(studentText);

            Assert.AreEqual(expectedOutput.FirstName, actualOutput.FirstName);
            Assert.AreEqual(expectedOutput.LastName, actualOutput.LastName);
            Assert.AreEqual(expectedOutput.Grade, actualOutput.Grade);
        }

        [TestMethod]
        public void ShouldSuccessfullyReadStudentWithEmptyStringsFromString()
        {
            var studentText = ", , 50";
            var expectedOutput = new Student("", "", 50);
            var actualOutput = _studentFileReaderWriter.ReadLineAsStudent(studentText);

            Assert.AreEqual(expectedOutput.FirstName, actualOutput.FirstName);
            Assert.AreEqual(expectedOutput.LastName, actualOutput.LastName);
            Assert.AreEqual(expectedOutput.Grade, actualOutput.Grade);
        }

        [TestMethod]
        public void ShouldFailToReadLineAsStudentIfGradeIsNotAnInt()
        {
            var studentText = "Swan, Black, Purple";
            var output = _studentFileReaderWriter.ReadLineAsStudent(studentText);

            Assert.IsNull(output);
        }

        [TestMethod]
        public void ShouldFailToReadLineAsStudentIfGradeIsNotAnInt2()
        {
            var studentText = "Swan, Black, fifty";
            var output = _studentFileReaderWriter.ReadLineAsStudent(studentText);

            Assert.IsNull(output);
        }

        [TestMethod]
        public void ShouldFailToReadLineAsStudentIfGradeIsMissing()
        {
            var studentText = "Swan, Black,";
            var output =_studentFileReaderWriter.ReadLineAsStudent(studentText);

            Assert.IsNull(output);
        }

        [TestMethod]
        public void ShouldFailToReadLineAsStudentIfStringIsIncomplete()
        {
            var studentText = "Black, Swan";
            var output = _studentFileReaderWriter.ReadLineAsStudent(studentText);

            Assert.IsNull(output);
        }

        [TestMethod]
        public void ShouldFailToReadLineAsStudentIfStringHasTooManySeparators()
        {
            var studentText = "Black, Swan, 50, Purple";
            var output = _studentFileReaderWriter.ReadLineAsStudent(studentText);

            Assert.IsNull(output);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToReadStudentsFromFileIfFileIsNull()
        {
            _studentFileReaderWriter.ReadStudentsFromFile(null);
        }

        [TestMethod]
        public void ShouldSuccessfullyReadStudentsFromFileAndSortedOrderShouldBePreserved()
        {
            var tempFileName = Path.GetTempFileName();
            var students = "Swan, Black, 50\r\nSwan, Black, 45\r\nPigeon, Yellow, 5";
            File.WriteAllText(tempFileName, students);

            var studentFirst = new Student("Swan", "Black", 50);
            var studentSecond = new Student("Swan", "Black", 45);
            var studentThird = new Student("Pigeon", "Yellow", 5);

            var expectedOutput = new List<Student> {studentFirst, studentSecond, studentThird};
            var actualOutput = _studentFileReaderWriter.ReadStudentsFromFile(tempFileName).ToList();

            File.Delete(tempFileName);

            Assert.AreEqual(expectedOutput.Count, actualOutput.Count);
            for (int i = 0; i < actualOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].FirstName, actualOutput[i].FirstName);
                Assert.AreEqual(expectedOutput[i].LastName, actualOutput[i].LastName);
                Assert.AreEqual(expectedOutput[i].Grade, actualOutput[i].Grade);
            }
            
        }

        [TestMethod]
        public void ShouldSuccessfullyReadStudentsFromFileAndUnsortedOrderShouldBePreserved()
        {
            var tempFileName = Path.GetTempFileName();
            var students = "Swan, Black, 55\r\nGull, White, 55\r\nPigeon, Yellow, 5";
            File.WriteAllText(tempFileName, students);

            var studentFirst = new Student("Swan", "Black", 55);
            var studentSecond = new Student("Gull", "White", 55); 
            var studentThird = new Student("Pigeon", "Yellow", 5);

            var expectedOutput = new List<Student> { studentFirst, studentSecond, studentThird };
            var actualOutput = _studentFileReaderWriter.ReadStudentsFromFile(tempFileName).ToList();

            File.Delete(tempFileName);

            Assert.AreEqual(expectedOutput.Count, actualOutput.Count);
            for (int i = 0; i < actualOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].FirstName, actualOutput[i].FirstName);
                Assert.AreEqual(expectedOutput[i].LastName, actualOutput[i].LastName);
                Assert.AreEqual(expectedOutput[i].Grade, actualOutput[i].Grade);
            }
        }

        [TestMethod]
        public void ShouldSuccessfullyReadStudentsFromFileAndEmptyStringStudentShouldBePreserved()
        {
            var tempFileName = Path.GetTempFileName();
            var students = ", , 50\r\nSwan, Black, 45\r\nPigeon, Yellow, 5";
            File.WriteAllText(tempFileName, students);

            var studentFirst = new Student("", "", 50);
            var studentSecond = new Student("Swan", "Black", 45);
            var studentThird = new Student("Pigeon", "Yellow", 5);

            var expectedOutput = new List<Student> { studentFirst, studentSecond, studentThird };
            var actualOutput = _studentFileReaderWriter.ReadStudentsFromFile(tempFileName).ToList();

            File.Delete(tempFileName);

            Assert.AreEqual(expectedOutput.Count, actualOutput.Count);
            for (int i = 0; i < actualOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].FirstName, actualOutput[i].FirstName);
                Assert.AreEqual(expectedOutput[i].LastName, actualOutput[i].LastName);
                Assert.AreEqual(expectedOutput[i].Grade, actualOutput[i].Grade);
            }
        }

        [TestMethod]
        public void ShouldFailToReadStudentsFromFileIfStudentHasGradeThatIsNotAnInt()
        {
            var tempFileName = Path.GetTempFileName();
            var students = "Swan, Black, 50\r\nSwan, Black, Purple\r\nPigeon, Yellow, 5";
            File.WriteAllText(tempFileName, students);

            var studentFirst = new Student("Swan", "Black", 50);
            var studentSecond = new Student("Pigeon", "Yellow", 5);

            var expectedOutput = new List<Student> { studentFirst, studentSecond };
            var actualOutput = _studentFileReaderWriter.ReadStudentsFromFile(tempFileName).ToList();

            File.Delete(tempFileName);

            Assert.AreEqual(expectedOutput.Count, actualOutput.Count);
            for (int i = 0; i < actualOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].FirstName, actualOutput[i].FirstName);
                Assert.AreEqual(expectedOutput[i].LastName, actualOutput[i].LastName);
                Assert.AreEqual(expectedOutput[i].Grade, actualOutput[i].Grade);
            }
        }

        [TestMethod]
        public void ShouldSuccessfullyReadNoStudentsFromFileWithNoStudents()
        {
            var tempFileName = Path.GetTempFileName();
            var expectedOutput = new List<Student>();
            var actualOutput = _studentFileReaderWriter.ReadStudentsFromFile(tempFileName).ToList();
            File.Delete(tempFileName);

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldFailToWriteStudentsToFileIfThereIsNoPath()
        {
            var student = new Student("Gull", "Red", 19);
            _studentFileReaderWriter.WriteStudentsToFile(null, new List<Student> { student });
        }

        [TestMethod]
        public void ShouldSuccessfullyWriteNoStudentsToFileIfThereAreNoStudents()
        {
            var tempFileName = Path.GetTempFileName();
            _studentFileReaderWriter.WriteStudentsToFile(tempFileName, new List<Student>());
            var text = File.ReadAllText(tempFileName);
            File.Delete(tempFileName);

            Assert.AreEqual(string.Empty, text);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldFailToWriteStudentsToFileIfThereIsANullStudent()
        {
            var tempFileName = Path.GetTempFileName();
            _studentFileReaderWriter.WriteStudentsToFile(tempFileName, new Student[] { null } );
            File.ReadAllText(tempFileName);
            File.Delete(tempFileName);
        }

        [TestMethod]
        public void ShouldSuccessfullyWriteStudentToFileIfStudentIsValid()
        {
            var tempFileName = Path.GetTempFileName();
            var student = new Student("Swan", "Black", 50);
            var input = new List<Student> {student};
            _studentFileReaderWriter.WriteStudentsToFile(tempFileName, input);
            var text = File.ReadAllLines(tempFileName);
            File.Delete(tempFileName);

            Assert.AreEqual(1, text.Length);
            Assert.AreEqual("Swan, Black, 50", text[0]);
        }

        [TestMethod]
        public void ShouldSuccessfullyWriteStudentsToFileIfStudentsAreValidAndOrderShouldBePreserved()
        {
            var tempFileName = Path.GetTempFileName();
            var studentFirst = new Student("Swan", "Black", 50);
            var studentSecond = new Student("Swan", "Black", 100);
            var input = new List<Student> {studentFirst, studentSecond};
            _studentFileReaderWriter.WriteStudentsToFile(tempFileName, input);

            var text = File.ReadAllLines(tempFileName);
            File.Delete(tempFileName);

            Assert.AreEqual(2, text.Length);
            Assert.AreEqual("Swan, Black, 50", text[0]);
            Assert.AreEqual("Swan, Black, 100", text[1]);
        }
    }
}
