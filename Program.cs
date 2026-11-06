
using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        string url = args.Length > 0
            ? args[0]
            : "https://app.cloudqa.io/home/AutomationPracticeForm";

        using IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Navigate().GoToUrl(url);
 
            WebDriverWait wait =
                new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // 1. FIRST NAME
            IWebElement firstName = wait.Until(d =>
                FindInputByLabel(d, "First Name"));

            firstName.Clear();
            firstName.SendKeys("Pratik");

            bool firstNamePassed =
                firstName.GetAttribute("value") == "Pratik";

            Console.WriteLine(firstNamePassed
                ? "PASS: First Name"
                : "FAIL: First Name");


            // 2. GENDER
            IWebElement genderRadio = wait.Until(d =>
                FindGenderRadio(d, "Male"));

            genderRadio.Click();

            bool genderPassed = genderRadio.Selected;

            Console.WriteLine(genderPassed
                ? "PASS: Gender - Male"
                : "FAIL: Gender");


            // 3. STATE
            IWebElement stateDropdown = wait.Until(d =>
                FindSelectByLabel(d, "State"));

            SelectElement stateSelect =
                new SelectElement(stateDropdown);


            stateSelect.SelectByText("India");

            bool statePassed =
        stateSelect.SelectedOption.Text.Trim()
            .Equals("India",
                StringComparison.OrdinalIgnoreCase);

            Console.WriteLine(statePassed
                ? "PASS: State - India"
                : "FAIL: State");


            Console.WriteLine("\nAutomation completed!");

        }
        catch (Exception ex)
        {
            Console.WriteLine("AUTOMATION FAILED: " + ex.Message);
        }

        Console.WriteLine("Press ENTER to close browser...");
        Console.ReadLine();
    }


    // Find text input using its label
    static IWebElement? FindInputByLabel(
        IWebDriver driver, string labelText)
    {
        var labels = driver.FindElements(By.TagName("label"));

        foreach (var label in labels)
        {
            if (!label.Text.Trim().Equals(
                labelText,
                StringComparison.OrdinalIgnoreCase))
                continue;

            string forId = label.GetAttribute("for") ?? "";

            if (!string.IsNullOrEmpty(forId))
            {
                var associated =
                    driver.FindElements(By.Id(forId));

                foreach (var element in associated)
                {
                    if (IsTextInput(element))
                        return element;
                }
            }

            var containers = label.FindElements(
                By.XPath("ancestor::*[self::div or self::fieldset]"));

            foreach (var container in containers)
            {
                var inputs = container.FindElements(
                    By.CssSelector("input"));

                var matches = inputs
                    .Where(IsTextInput)
                    .ToList();

                if (matches.Count == 1)
                    return matches[0];
            }
        }

        return null;
    }


    // Check whether an element is a text input
    static bool IsTextInput(IWebElement element)
    {
        string type = element.GetAttribute("type") ?? "";

        return element.TagName.Equals(
                   "input",
                   StringComparison.OrdinalIgnoreCase)
               && (type.Equals("text",
                   StringComparison.OrdinalIgnoreCase)
                   || type == "")
               && element.Displayed
               && element.Enabled;
    }


    // Find a radio button using its group label and option value
    static IWebElement? FindGenderRadio(
        IWebDriver driver, string option)
    {
        var labels = driver.FindElements(By.TagName("label"));

        foreach (var label in labels)
        {
            if (!label.Text.Trim().Equals(
                "Gender",
                StringComparison.OrdinalIgnoreCase))
                continue;

            var groups = label.FindElements(
                By.XPath("following-sibling::*[1]"));

            foreach (var group in groups)
            {
                var radios = group.FindElements(
                    By.CssSelector("input[type='radio']"));

                var matches = radios.Where(r =>
                    (r.GetAttribute("value") ?? "")
                    .Equals(option,
                        StringComparison.OrdinalIgnoreCase)
                    && r.Displayed
                    && r.Enabled).ToList();

                if (matches.Count == 1)
                    return matches[0];
            }
        }

        return null;
    }


    // Find dropdown using its label
    static IWebElement? FindSelectByLabel(
        IWebDriver driver, string labelText)
    {
        var labels = driver.FindElements(By.TagName("label"));

        foreach (var label in labels)
        {
            if (!label.Text.Trim().Equals(
                labelText,
                StringComparison.OrdinalIgnoreCase))
                continue;

            string forId = label.GetAttribute("for") ?? "";

            if (!string.IsNullOrEmpty(forId))
            {
                var associated =
                    driver.FindElements(By.Id(forId));

                foreach (var element in associated)
                {
                    if (element.TagName.Equals(
                        "select",
                        StringComparison.OrdinalIgnoreCase))
                        return element;
                }
            }

            var containers = label.FindElements(
                By.XPath("ancestor::*[self::div or self::fieldset]"));

            foreach (var container in containers)
            {
                var selects = container.FindElements(
                    By.TagName("select"));

                if (selects.Count == 1 &&
                    selects[0].Displayed &&
                    selects[0].Enabled)
                    return selects[0];
            }
        }

        return null;
    }
}
