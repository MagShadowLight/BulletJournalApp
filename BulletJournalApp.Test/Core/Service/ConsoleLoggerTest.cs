using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.Test.Core.Data;
using BulletJournalApp.Test.Util;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Core.Service
{
    [Collection("Sequential")]
    public class ConsoleLoggerTest
    {
        private ConsoleLogger _logger = new ConsoleLogger();
        private ConsoleInputOutput _console = new ConsoleInputOutput();

        [Theory]
        [MemberData(nameof(LogMessageData.GetMessage), MemberType =typeof(LogMessageData))]
        public void Given_There_Are_Message_When_Writing_Log_Message_To_Console_Then_It_Should_Display_Log_Message_To_The_Console(string message)
        {
            // Arrange // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _logger.Log(message);
                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
            _console.ResetOutput();
        }
        [Theory]
        [MemberData(nameof(LogMessageData.GetMessage), MemberType =typeof(LogMessageData))]
        public void Given_There_Are_Message_When_Writing_Warning_Message_To_Console_Then_It_Should_Display_Warning_Message_To_The_Console(string message)
        {
            // Arrange // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _logger.Warn(message);
                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
            _console.ResetOutput();
        }
        [Theory]
        [MemberData(nameof(LogMessageData.GetMessage), MemberType =typeof(LogMessageData))]
        public void Given_There_Are_Message_When_Writing_Error_Message_To_Console_Then_It_Should_Display_Warning_Message_To_The_Console(string message)
        {
            // Arrange // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _logger.Error(message);
                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
            _console.ResetOutput();
        }
    }
}
