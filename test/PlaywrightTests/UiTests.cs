using System.Diagnostics;
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
        
        private const string url = "https://bdsagroup28chirpremotedb.azurewebsites.net";
        
        //Making a setup for the tests
        [TestInitialize]
        public async Task Setup()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var cts = await browser.NewContextAsync();
            page = await cts.NewPageAsync();
        }
        
        //Disposing of the tests enviorement after
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
            
            await page.GetByRole(AriaRole.Button, new() { Name = ">" }).ClickAsync();
            Assert.AreEqual("https://bdsagroup28chirpremotedb.azurewebsites.net/?page=1", page.Url);
            await page.GetByRole(AriaRole.Button, new() { Name = ">" }).ClickAsync();
            Assert.AreEqual("https://bdsagroup28chirpremotedb.azurewebsites.net/?page=2", page.Url);
            await page.GetByRole(AriaRole.Button, new() { Name = "<" }).ClickAsync();
            Assert.AreEqual("https://bdsagroup28chirpremotedb.azurewebsites.net/?page=1", page.Url);
        }
        
        //Can register
        [TestMethod]
        public async Task CanRegister()
        {
            await page.GotoAsync("https://bdsagroup28chirpremotedb.azurewebsites.net/");
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
            await page.GetByRole(AriaRole.Heading, new() { Name = "What's on your mind EndUser?", Exact = true }).IsVisibleAsync();
        }

        [TestMethod]
        public async Task AboutMePage()
        {
            logIn();
            
            await page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
            await page.GetByRole(AriaRole.Heading, new() { Name = "Personal Information" }).IsVisibleAsync();
            await page.GetByText("Username: EndUser").IsVisibleAsync();
            await page.GetByText("Email: EndUser@itu.dk").IsVisibleAsync();

        }
        
        [TestMethod]
        public async Task CanCheep()
        {
            logIn();
            
            await page.GetByRole(AriaRole.Link, new() { Name = "Public timeline" }).ClickAsync();
            await page.GetByPlaceholder("Share your thoughts...").ClickAsync();
            await page.GetByPlaceholder("Share your thoughts...").FillAsync("Hello this is my Cheep!!!");
            await page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
            
            await page.Locator("li").Filter(new() { HasText = "EndUser"}).GetByRole(AriaRole.Link).IsVisibleAsync();
        }

        [TestMethod]
        public async Task CanFollowAndSeeList()
        {
            logIn();
            //Not following anyone yet
            await page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Following" }).ClickAsync();
            await page.GetByText("You aren't following anyone").IsVisibleAsync();
            
            //Follow Mellie Yost
            await page.GetByRole(AriaRole.Link, new() { Name = "Public timeline" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Mellie Yost" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Follow" }).ClickAsync();
            
            //Mellie is visible
            await page.GetByRole(AriaRole.Link, new() { Name = "My timeline" }).ClickAsync();
            await page.Locator("li").Filter(new() { HasText = "Mellie Yost But what was" }).GetByRole(AriaRole.Link).IsVisibleAsync();
            
            //The following list has Mellie included
            await page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Following" }).ClickAsync();
            await page.GetByRole(AriaRole.Heading, new() { Name = "Following List" }).IsVisibleAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Mellie Yost" }).IsVisibleAsync();
        }

        [TestMethod]
        public async Task Unfollow()
        {
            logIn();
            
            await page.GetByRole(AriaRole.Link, new() { Name = "My timeline" }).ClickAsync();
            await page.Locator("li").Filter(new() { HasText = "Mellie Yost But what was" }).GetByRole(AriaRole.Link).ClickAsync();
            
            await page.GetByRole(AriaRole.Button, new() { Name = "Unfollow" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Following" }).ClickAsync();
            await page.GetByText("You aren't following anyone").IsVisibleAsync();
        }

        [TestMethod]
        public async Task Logout()
        {
            logIn();
            
            await page.GetByRole(AriaRole.Link, new() { Name = "Logout" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" }).ClickAsync();
            
            await page.GetByRole(AriaRole.Heading, new() { Name = "Log out" }).IsVisibleAsync();
            await page.GetByText("You have successfully logged").IsVisibleAsync();
        }
        
        [TestMethod]
        public async Task CanLogin()
        {
            await page.GotoAsync("https://bdsagroup28chirpremotedb.azurewebsites.net/");
            await page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
            
            //Fill in login form
            await page.GetByPlaceholder("username").ClickAsync();
            await page.GetByPlaceholder("username").FillAsync("EndUser");
            await page.GetByPlaceholder("password").ClickAsync();
            await page.GetByPlaceholder("password").FillAsync("Password1.");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            
            await page.GetByRole(AriaRole.Heading, new() { Name = "Public Timeline What's on" }).IsVisibleAsync();
            await page.GetByRole(AriaRole.Heading, new() { Name = "What's on your mind EndUser?", Exact = true }).IsVisibleAsync();
        }

        [TestMethod]
        public async Task DeleteCheeep()
        {
            logIn();
            
            await page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
            void page_Dialog_EventHandler(object sender, IDialog dialog)
            {
                Console.WriteLine($"Dialog message: {dialog.Message}");
                dialog.DismissAsync();
                page.Dialog -= page_Dialog_EventHandler;
            }
            page.Dialog += page_Dialog_EventHandler;
            await page.GetByText("There are no cheeps so far.").IsVisibleAsync();
        }
        
        [TestMethod]
        public async Task ForgetMe()
        {
            logIn();
            
            await page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
            void page_Dialog_EventHandler(object sender, IDialog dialog)
            {
                Console.WriteLine($"Dialog message: {dialog.Message}");
                dialog.DismissAsync();
                page.Dialog -= page_Dialog_EventHandler;
            }
            page.Dialog += page_Dialog_EventHandler;
            await page.GetByRole(AriaRole.Button, new() { Name = "Forget Me" }).ClickAsync();
            
            await page.GetByRole(AriaRole.Heading, new() { Name = "Icon1Chirp!" }).IsVisibleAsync();
            await page.GetByRole(AriaRole.Heading, new() { Name = "Public Timeline" }).IsVisibleAsync();
        }

        [TestMethod]
        public async Task LogInAfterForgetMe()
        {
            await page.GotoAsync("https://bdsagroup28chirpremotedb.azurewebsites.net/");
            await page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
            await page.GetByPlaceholder("username").ClickAsync();
            await page.GetByPlaceholder("username").FillAsync("EndUser");
            await page.GetByPlaceholder("password").ClickAsync();
            await page.GetByPlaceholder("password").FillAsync("Password1.");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
            await page.GetByText("Invalid login attempt.").IsVisibleAsync();
        }

        public async Task logIn()
        {
            await page.GotoAsync("https://bdsagroup28chirpremotedb.azurewebsites.net/");
            
            await page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
            
            //Fill in login form
            await page.GetByPlaceholder("username").ClickAsync();
            await page.GetByPlaceholder("username").FillAsync("EndUser");
            await page.GetByPlaceholder("password").ClickAsync();
            await page.GetByPlaceholder("password").FillAsync("Password1.");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        }

    }
    
}