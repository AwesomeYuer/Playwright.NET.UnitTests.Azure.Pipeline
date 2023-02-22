namespace ConsoleApp1NUnitTests
{
    using PlaywrightEntry = Microsoft.Playwright.Program;
    using Microsoft.Playwright;
    using Microsoft.Playwright.NUnit;
    public class PlaywrightNUnitTests : PageTest
    {
        //[SetUp]
        //public void Setup()
        //{

        //    Console.WriteLine("Start download chromium");
        //    var exitCode = PlaywrightEntry.Main(new[] { "install", "chromium" });
        //    if (exitCode != 0)
        //    {
        //        throw new Exception($"Playwright exited with code {exitCode}");
        //    }
        //}

        [Test]
        public async Task ShouldHaveTheCorrectSlogan()
        {
            await Page.GotoAsync("https://playwright.dev");
            await Expect(Page.Locator("text=enables reliable end-to-end testing for modern web apps")).ToBeVisibleAsync();
        }

        [Test]
        public async Task ShouldHaveTheCorrectTitle()
        {
            await Page.GotoAsync("https://playwright.dev");
            var title = Page.Locator(".navbar__inner .navbar__title");
            //title = Page.Locator("tag=title");
            await Expect(title).ToContainTextAsync("Playwright");
        }

        [Test]
        public async Task VerifyDotNetLinkClickRedirectingToDotNetIntroPage()
        {
            using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://playwright.dev/dotnet/");
            await page.ClickAsync("section >> text=.NET");
            Assert.AreNotEqual("https://playwright.dev/dotnet/docs/intro/", page.Url);
        }


        [Test]
        public async Task BaiduSearch_Test()
        {

            Console.WriteLine("你好...");

            await Page.GotoAsync("https://www.baidu.com");

            var locator = Page.Locator("title");

            var c = locator.CountAsync().Result;

            var s = locator.InnerTextAsync().Result;

            s = "百度一下，你就知道";
            //await Expect(locator).ToContainTextAsync("百度");

            Console.WriteLine($"title: {s}");

            Assert.IsTrue(s.Contains("百度", StringComparison.OrdinalIgnoreCase));
                        
            await Page.Locator("id=kw").FillAsync(Guid.NewGuid().ToString());

            await Page.Locator("id=su").ClickAsync();

            await Task.Delay(2000);

            await Page.Locator("//*[@id=\"u\"]/a[2]").HoverAsync();

            await Page.Locator("//*[@id=\"u\"]/div/a[1]/span").ClickAsync();
            await Page.Locator("id=sh_1").CheckAsync();

            locator = Page.Locator("body");
            s = locator.InnerTextAsync().Result;

            await Expect(locator).ToContainTextAsync("百度为您找到相关结果");

            Assert.IsTrue(s.Contains("百度为您找到相关结果", StringComparison.OrdinalIgnoreCase));

        }
    }
}