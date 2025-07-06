using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletJournalApp.Test.Service
{
    public class FileLoggerTest
    {
        // private FileMode mode = FileMode.Open;
        //[Fact]
        //public void When_Text_Were_Written_Into_File_Then_It_Should_Succeed()
        //{
        //    // Arrange
        //    var mockLogger = new Mock<IFileLogger>();
        //    var message = "Task added successfully";
        //    mockLogger.Setup(logger => logger.Log(message));
        //    var logger = new FileLogger();
        //    var path = Path.Combine("Temp", "Log.txt");
        //    CreateFile(path);
        //    // Act
        //    var testfs = new FileStream(path, mode);
        //    logger.WriteText(testfs, "LOG", message);
        //    testfs.Close();
        //    var fs = new FileStream(path, mode);
        //    using (StreamReader sr = new StreamReader(fs))
        //    {
        //        string outputMessage = sr.ReadToEnd();
        //        // Assert
        //        Assert.Contains(message, outputMessage);
        //        ResetStream(sr, fs);
        //    }
        //}
        //public void CreateFile(string path)
        //{
        //    if (!File.Exists(path))
        //    {
        //        if (!Directory.Exists("Temp"))
        //        {
        //            Directory.CreateDirectory("Temp");
        //        }
        //        File.Create(path).Close();
        //    }
        //}
        //public void ResetStream(StreamReader sr, FileStream fs)
        //{
        //    sr.Close();
        //    fs.Close();
        //}
    }
}
