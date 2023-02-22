namespace ConsoleApp1MSTests
{
    using PlaywrightEntry = Microsoft.Playwright.Program;
    using Microsoft.Playwright;
    using Microsoft.Playwright.MSTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass(), TestCategory(nameof(PlaywrightMsTests))]
    public class PlaywrightMsTests : PageTest
    {

        //[TestInitialize()]
        //public void TestInitializeProcess()
        //{
        //    Console.WriteLine($"nameof{TestInitializeProcess}");

        //    Console.WriteLine("Start download chromium");
        //    var exitCode = PlaywrightEntry.Main(new[] { "install", "chromium" });
        //    if (exitCode != 0)
        //    {
        //        throw new Exception($"Playwright exited with code {exitCode}");
        //    }
        //}

        [DataRow(true, "msedge")]
        [DataRow(true, "chrome")]
        [TestMethod()]
        public async Task Baidu_Test(bool browserHeadless, string browserChannel)
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true, Channel = browserChannel });
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.baidu.com");
            var title = await page.InnerTextAsync("title");
            await browser.CloseAsync();
            Console.WriteLine($"{nameof(PlaywrightMsTests)}Title:《{title}》");
            Assert.IsTrue(title.Contains("百度"));
        }

        [DataRow(true, "chrome")]
        [DataRow(true, "msedge")]
        [TestMethod()]
        public async Task BaiduSearch_Test(bool browserHeadless, string browserChannel)
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            //await using var browser = await playwright.Chromium.LaunchAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = browserHeadless, Channel = browserChannel });

            var page = await browser.NewPageAsync();
            
            await page.GotoAsync("https://www.baidu.com");

            await page.Locator("id=kw").FillAsync(Guid.NewGuid().ToString());

            await page.Locator("id=su").ClickAsync();

            await Task.Delay(2000);

            await page.Locator("//*[@id=\"u\"]/a[2]").HoverAsync();

            await page.Locator("//*[@id=\"u\"]/div/a[1]/span").ClickAsync();
            await page.Locator("id=sh_1").CheckAsync();
            var title = await page.InnerTextAsync("title");
            var s = page.InnerTextAsync("body").Result;
            Console.WriteLine($"{nameof(PlaywrightMsTests)} Title: <<{title}>>");
            await browser.CloseAsync();
            Assert.IsTrue(s!.Contains("百度为您找到相关结果"));
        }
    }
}
