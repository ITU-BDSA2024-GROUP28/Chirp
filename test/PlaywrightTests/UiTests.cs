using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace PlaywrightTests
{
    //This test class is made with the help of codegen
    [TestClass]
    public class UiTests
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IPage page;
        
        //Making a setup for the tests
        [TestInitialize]
        public async Task Setup()
        {
            playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var cts = await browser.NewContextAsync();
            page = await cts.NewPageAsync();
        }
        
        //Disposing of the tests enviorment after
        [TestCleanup]
        public async Task TearDown()
        {
            await browser.CloseAsync();
            playwright.Dispose();
        }
        
        //Public timeline without being logged in
        [TestMethod]
        public async Task PublicTimeline()
        {
            await page.GotoAsync("https://bdsagroup28chirpremotedb.azurewebsites.net/");

            await page.GetByRole(AriaRole.Heading, new() { Name = "Icon1Chirp!" }).IsVisibleAsync();
            await page.GetByRole(AriaRole.Heading, new() { Name = "Public Timeline" }).IsVisibleAsync();
        }
        
        //Can register
        [TestMethod]
        public async Task CanRegister()
        {
            //Filling the fields in the register form
            await page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
            await page.GetByPlaceholder("username").ClickAsync();
            await page.GetByPlaceholder("username").FillAsync("EndUser");
            await page.GetByPlaceholder("name@example.com").ClickAsync();
            await page.GetByPlaceholder("name@example.com").FillAsync("EndUser@itu.dk");
            await page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
            await page.GetByLabel("Password", new() { Exact = true }).FillAsync("Password1.");
            await page.GetByLabel("Confirm Password").ClickAsync();
            await page.GetByLabel("Confirm Password").FillAsync("Password1.");
            
            //Click register button
            await page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
            await page.GetByRole(AriaRole.Heading, new() { Name = "Public Timeline What's on" }).IsVisibleAsync();
            await page.GetByRole(AriaRole.Heading, new() { Name = "What's on your mind UserEndTest?", Exact = true }).IsVisibleAsync();
        }
        
        
        
        
    }
    
}