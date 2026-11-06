# CloudQA Task 1 – Selenium Automation

A C# and Selenium WebDriver project to automate the CloudQA Practice Form.

## Features
- Enter First Name.
- Select Gender (Male).
- Select India from the State dropdown.
- Uses label-based locators instead of relying only on fixed IDs or classes.
- Supports configurable website URLs.
- Verifies results using PASS/FAIL messages.

## Tech Stack
- C#
- .NET
- Selenium WebDriver
- Google Chrome

## Setup & Run

Clone the repository:

```bash
git clone YOUR_REPOSITORY_URL
cd CloudQA-Task1
```

Restore dependencies and build:

```bash
dotnet restore
dotnet build
```

Run the automation:

```bash
dotnet run
```

Custom URL:

```bash
dotnet run -- "https://app.cloudqa.io/home/AutomationPracticeForm"
```

## Locator Strategy

Uses visible labels, associated form controls, and nearby form structure to identify elements. This reduces dependency on fixed IDs, names, CSS classes, and field positions.

## Test Scenarios

- First Name input – PASS
- Gender selection – PASS
- India dropdown selection – PASS
- Simulated attribute changes – PASS
- Simulated field reordering – PASS
- Similar-looking fields – PASS

## Limitations
- Major HTML structure or label changes may require locator updates.
- Ambiguous or duplicate labels may need additional handling.
- Chrome and .NET SDK are required.

## Author
**Pratik Makvana**

GitHub: https://github.com/Pratik-makvanaa