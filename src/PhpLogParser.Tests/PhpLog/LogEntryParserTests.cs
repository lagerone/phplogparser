using FakeItEasy;
using NUnit.Framework;
using PhpLogParser.PhpLog;

namespace PhpLogParser.Tests.PhpLog
{
    [TestFixture]
    public class LogEntryParserTests
    {
        [UnderTest]
#pragma warning disable 649
        private LogEntryParser _logEntryParser;
#pragma warning restore 649

        [SetUp]
        public void Setup()
        {
            Fake.InitializeFixture(this);
        }

        [Test]
        public void Should_parse_log_date()
        {
            //Arrange
            const string input = @"[09-Aug-2016 21:14:05 Europe/Berlin] PHP Fatal error:  Uncaught exception 'Exception' with message 'Error happened' in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php:90
Stack trace:
#0 /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/index.php(65): Posts_controller->test(Array, Array, Array)
#1 {main}
  thrown in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php on line 90";

            //Act
            var result = _logEntryParser.Parse(input);

            //Assert
            Assert.That(result.LogDate.ToString("yyyy-MM-dd HH:mm:ss"), Is.EqualTo("2016-08-09 21:14:05"));
        }

        [Test]
        public void Should_parse_log_type()
        {
            //Arrange
            const string input = @"[09-Aug-2016 21:14:05 Europe/Berlin] PHP Fatal error:  Uncaught exception 'Exception' with message 'Error happened' in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php:90
Stack trace:
#0 /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/index.php(65): Posts_controller->test(Array, Array, Array)
#1 {main}
  thrown in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php on line 90";

            //Act
            var result = _logEntryParser.Parse(input);

            //Assert
            Assert.That(result.LogType, Is.EqualTo("PHP Fatal error"));
        }

        [Test]
        public void Should_parse_log_message()
        {
            //Arrange
            const string input = @"[09-Aug-2016 21:14:05 Europe/Berlin] PHP Fatal error:  Uncaught exception 'Exception' with message 'Error happened' in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php:90
Stack trace:
#0 /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/index.php(65): Posts_controller->test(Array, Array, Array)
#1 {main}
  thrown in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php on line 90";

            const string expectedResult = @"Uncaught exception 'Exception' with message 'Error happened' in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php:90
Stack trace:
#0 /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/index.php(65): Posts_controller->test(Array, Array, Array)
#1 {main}
  thrown in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php on line 90";

            //Act
            var result = _logEntryParser.Parse(input);

            //Assert
            Assert.That(result.LogMessage, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Should_parse_404_entries()
        {
            // Arrange
            const string input =
                @"[28-Aug-2016 13:00:17 Europe/Berlin] Somesite Application Notice: 404 not found: GET: Array

(
    [c] => tags34535
    [a] => view
    [tag] => sometag
)
 POST: Array
(
)
 

";
            //Act
            var result = _logEntryParser.Parse(input);

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Should_parse_timeout_entry()
        {
            // Arrange
            const string input =
                @"[08-Sep-2016 00:10:09 Europe/Berlin] Somesite Application Error: Unable to connect to database [Operation timed out]";
            
            //Act
            var result = _logEntryParser.Parse(input);

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Should_parse_Somesite_timeout_entry()
        {
            // Arrange
            const string input =
                @"[08-Sep-2016 00:10:09 Europe/Berlin] Somesite Application Error: Unable to connect to database [Operation timed out] at ConnectionProvider::getConnection";

            //Act
            var result = _logEntryParser.Parse(input);

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Should_parse_mysql_entry()
        {
            // Arrange
            const string input =
                @"[23-Sep-2016 03:23:15 UTC] PHP Warning:  mysqli_query(): MySQL server has gone away in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/blogg/wp-includes/wp-db.php on line 1811";

            //Act
            var result = _logEntryParser.Parse(input);

            //Assert
            Assert.That(result, Is.Not.Null);
        }
    }
}
