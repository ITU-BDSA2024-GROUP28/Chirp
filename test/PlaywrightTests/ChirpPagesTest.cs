using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace PlaywrightTests;

//This has been made with the help from the website https://playwright.dev/docs/writing-tests
//This class is to test the Pages on our program Chirp that is launched on azure

[TestClass]
public class ChirpPagesTest : PageTest
{   
    
    //Test Azure page comes up
    [TestMethod]
    public async Task HomePageHasTitleChirp()
    {
        await Page.GotoAsync("https://bdsagroup28chirpremotedb.azurewebsites.net/");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Chirp!"));
    }
    
    
    [TestMethod]
    public async Task CanRegister()
    {
        await Page.GotoAsync("https://bdsagroup28chirpremotedb.azurewebsites.net/");

        // Click the get started link.
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        
        // Expects page to have a heading with the name of Installation.
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Create a new account" })).ToBeVisibleAsync();
        
        //Has the register info
        await Page.GetByLabel("Username").FillAsync("EndToEndTestUser");
        await Page.GetByLabel("Email").FillAsync("EndToEndTestUser@itu.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Password10.");
        await Page.GetByLabel("Confirm Password", new() { Exact = true }).FillAsync("Password10.");
        
        //Click register button
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
        //Verify register
        await Expect(Page).ToHaveURLAsync("https://bdsagroup28chirpremotedb.azurewebsites.net/");
        await Expect(Page).ToHaveTitleAsync(new Regex("Public Timeline"));
    } 
}