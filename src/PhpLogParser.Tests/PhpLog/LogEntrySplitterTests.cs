using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using PhpLogParser.PhpLog;

namespace PhpLogParser.Tests.PhpLog
{
    [TestFixture]
    public class LogEntrySplitterTests
    {
        [UnderTest]
#pragma warning disable 649
        private readonly LogEntrySplitter _logEntrySplitter;
#pragma warning restore 649

        [SetUp]
        public void Setup()
        {
            Fake.InitializeFixture(this);
        }

        [TestCase(TestData1, 7)]
        [TestCase(TestData2, 4)]
        [TestCase(TestData3, 3)]
        public void Should_split_entries(string input, int expectedCount)
        {
            //Act
            var result = _logEntrySplitter.Split(input);

            //Assert
            Assert.That(result.Count, Is.EqualTo(expectedCount));
        }
        
        private const string TestData1 = 
            @"[04-Oct-2016 05:37:31 Europe/Berlin] PHP Notice:  Undefined variable: array_smyears in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/admin/views/_shared/inc_sales_header_view.php on line 15
[04-Oct-2016 05:37:31 Europe/Berlin] PHP Warning:  Invalid argument supplied for foreach() in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/admin/views/_shared/inc_sales_header_view.php on line 15
[04-Oct-2016 02:04:15 Europe/Berlin] Somesite Application Notice: 404 not found: GET: Array
(
    [c] => posts
    [amp%2525253ba] => frakt
    [amp%2525253bfrakt_id] => 1
)
 POST: Array
(
)
 

[04-Oct-2016 04:40:56 Europe/Berlin] Somesite Application Notice: 404 not found: GET: Array
(
    [c] => posts
    [a] => frakt
    [frakt_id] => 4
)
 POST: Array
(
)
 

[04-Oct-2016 04:40:58 Europe/Berlin] Somesite Application Notice: 404 not found: GET: Array
(
    [c] => posts
    [a] => frakt
    [frakt_id] => 5
)
 POST: Array
(
)
 

[04-Oct-2016 04:41:37 Europe/Berlin] Somesite Application Notice: 404 not found: GET: Array
(
    [c] => tags
    [amp%253ba] => view
    [amp%253btag] => sometag
)
 POST: Array
(
)

[04-Oct-2016 05:37:31 Europe/Berlin] PHP Warning:  Invalid argument supplied for foreach() in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/admin2/views/_shared/inc_sales_header_view.php on line 15";

        private const string TestData2 = 
            @"[09-Aug-2016 21:33:38 Europe/Stockholm] PHP Warning:  Invalid argument supplied for foreach() in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/admin/logs.php on line 39
[09-Aug-2016 21:33:40 Europe/Stockholm] PHP Notice:  Undefined variable: dirs_list in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/admin/logs.php on line 39
[09-Aug-2016 21:33:40 Europe/Stockholm] PHP Warning:  Invalid argument supplied for foreach() in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/admin/logs.php on line 39
[09-Aug-2016 21:33:52 Europe/Berlin] PHP Fatal error:  Uncaught exception 'Exception' with message 'Error happened' in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php:90
Stack trace:
#0 /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/index.php(65): Posts_controller->test(Array, Array, Array)
#1 {main}
  thrown in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/c/Posts_controller.php on line 90";

        private const string TestData3 =
            @"[28-Aug-2016 12:25:33 Europe/Berlin] PHP Fatal error:  Call to undefined function getPackPrice() in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/public_html/views/products/mobile/product_single_view_grid.php on line 87
[28-Aug-2016 13:00:17 Europe/Berlin] Somesite Application Notice: 404 not found: GET: Array
(
    [c] => tags34535
    [a] => view
    [tag] => utomhus
)
 POST: Array
(
)
 

[28-Aug-2016 13:00:47 Europe/Berlin] PHP Fatal error:  Cannot use object of type Klarna\XMLRPC\PClass as array in /www/webvol6/xd/6mv9sy69c36je62/somesite.se/app/Services/KlarnaPClassService.php on line 63";

    }
}