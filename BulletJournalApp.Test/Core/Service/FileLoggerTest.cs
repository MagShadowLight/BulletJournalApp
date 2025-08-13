using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.Test.Core.Data;
using BulletJournalApp.Test.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Service
{
    [Collection("Sequential")]
    public class FileLoggerTest
    {
        private FileMode mode = FileMode.Open;
        private Mock<IFileLogger> _fileloggerMock = new Mock<IFileLogger>();
        private FileLogger _fileLogger = new FileLogger();
        private string path = Path.Combine("Temp", "Log.txt");
        private FileInputOutput _file = new FileInputOutput();
        [Theory]
        [MemberData(nameof(LogMessageData.GetMessage), MemberType = typeof(LogMessageData))]
        public void Given_There_Are_Messages_When_Writing_Log_Messages_Into_A_File_Then_It_Should_Display_In_A_File(string message)
        {
            // Arrange
            _fileloggerMock.Setup(logger => logger.Log(message));
            var logger = new FileLogger();
            _file.DeleteFile(path);
            _file.CreateFile(path);
            // Act
            _fileLogger.Log(message);
            var fs = new FileStream(path, mode);
            using (StreamReader sr = new StreamReader(fs))
            {
                string outputMessage = sr.ReadToEnd();
                // Assert
                Assert.Contains(message, outputMessage);
                _file.ResetStream(sr, fs);
            }
        }
        [Theory]
        [MemberData(nameof(LogMessageData.GetMessage), MemberType = typeof(LogMessageData))]
        public void Given_There_Are_Messages_When_Writing_Warning_Messages_Into_A_File_Then_It_Should_Display_In_A_File(string message)
        {
            // Arrange
            _fileloggerMock.Setup(logger => logger.Warn(message));
            var logger = new FileLogger();
            _file.DeleteFile(path);
            _file.CreateFile(path);
            // Act
            _fileLogger.Warn(message);
            var fs = new FileStream(path, mode);
            using (StreamReader sr = new StreamReader(fs))
            {
                string outputMessage = sr.ReadToEnd();
                // Assert
                Assert.Contains(message, outputMessage);
                _file.ResetStream(sr, fs);
            }
        }
        [Theory]
        [MemberData(nameof(LogMessageData.GetMessage), MemberType = typeof(LogMessageData))]
        public void Given_There_Are_Messages_When_Writing_Error_Messages_Into_A_File_Then_It_Should_Display_In_A_File(string message)
        {
            // Arrange
            _fileloggerMock.Setup(logger => logger.Error(message));
            var logger = new FileLogger();
            _file.DeleteFile(path);
            _file.CreateFile(path);
            // Act
            _fileLogger.Error(message);
            var fs = new FileStream(path, mode);
            using (StreamReader sr = new StreamReader(fs))
            {
                string outputMessage = sr.ReadToEnd();
                // Assert
                Assert.Contains(message, outputMessage);
                _file.ResetStream(sr, fs);
            }
        }
    }
}
