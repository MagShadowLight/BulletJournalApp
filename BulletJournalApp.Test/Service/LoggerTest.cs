using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class LoggerTest
    {

        private string path = Path.Combine("../", "../", "../", "Temp", "Log.txt");
        private FileMode mode = FileMode.Open;

        [Fact]
        public void When_Task_Were_Added_Successfully_Then_Console_Logger_Should_Display_Log()
        {
            // Arrange
            var logger = new ConsoleLogger();
            var message = "Task added successfully";
            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                logger.Log(message);

                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
            ResetOutput();
        }
        [Fact]
        public void When_Task_Failed_To_Add_Then_Console_Logger_Should_Display_Error()
        {
            // Arrange
            var logger = new ConsoleLogger();
            var message = "Failed to add task";
            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                logger.Error(message);

                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
            ResetOutput();
        }
        [Fact]
        public void When_Console_Logger_Display_Warning_Message_Then_It_Should_Succeed()
        {
            // Arrange
            var logger = new ConsoleLogger();
            var message = "Test Warning";
            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                logger.Warn(message);

                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
        }
        [Fact]
        public void When_Task_Were_Added_Successfully_Then_File_Logger_Should_Create_Log_In_Log_File()
        {
            // Arrange
            var mockLogger = new Mock<IFileLogger>();
            var message = "Task added successfully";
            mockLogger.Setup(logger => logger.Log(message));
            var logger = new FileLogger();
            var path = Path.Combine("Temp", "Log.txt");
            CreateFile(path);
            // Act
            logger.Log(message);
            var fs = new FileStream(path, mode);
            using (StreamReader sr = new StreamReader(fs))
            {
                string outputMessage = sr.ReadToEnd();
                // Assert
                Assert.Contains(message, outputMessage);
                ResetStream(sr, fs);
            }
        }
        [Fact]
        public void When_There_Is_Warning_Then_File_Logger_Should_Create_Warning_In_Log_File()
        {
            // Arrange
            var mockLogger = new Mock<IFileLogger>();
            var message = "Test Warning";
            mockLogger.Setup(logger => logger.Warn(message));
            var logger = new FileLogger();
            var path = Path.Combine("Temp", "Log.txt");
            CreateFile(path);
            // Act
            logger.Warn(message);
            var fs = new FileStream(path, mode);
            using (StreamReader streamReader = new StreamReader(fs))
            {
                string outputMessage = streamReader.ReadToEnd();
                // Assert
                Assert.Contains(message, outputMessage);
                ResetStream(streamReader, fs);
            }
        }
        [Fact]
        public void When_Task_Failed_To_Add_Then_File_Logger_Should_Print_Error_In_Log_File()
        {
            // Arrange
            var mockLogger = new Mock<IFileLogger>();
            var message = "Failed to add task";
            mockLogger.Setup(logger => logger.Error(message));
            var logger = new FileLogger();
            var path = Path.Combine("Temp", "Log.txt");
            CreateFile(path);
            // Act
            logger.Error(message);
            var fs = new FileStream(path, mode);
            using (StreamReader sr = new StreamReader(fs))
            {
                string outputMessage = sr.ReadToEnd();
                // Assert
                Assert.Contains(message, outputMessage);
                ResetStream(sr, fs);
            }
        }
        public void ResetOutput()
        {
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
        public void ResetStream(StreamReader sr, FileStream fs)
        {
            sr.Close();
            fs.Close();
        }

        public void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                if (!Directory.Exists("Temp"))
                {
                    Directory.CreateDirectory("Temp");
                }
                File.Create(path).Close();
            }
        }
    }
}
