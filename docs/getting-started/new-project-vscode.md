---
layout: default
title: New VS Code Project
parent: Getting Started
nav_order: 6
---

## Set up a new project in Visual Studio Code (VSCode)

Before following these instructions, you will first need to complete the steps on the [Tools and Installation](tools-and-installation) page for your system.

### Create the project

1. Open VSCode and open a terminal with the menu option `Terminal > New Terminal`. The terminal should open in a panel inside of VSCode. (This terminal is different from your system terminal.)
1. Within the terminal, type commands at the terminal prompt to navigate to the folder where you want to create the new project. You can navigate to folders (also called directories) using the following commands:
    - `cd /` moves you to the top level folder in your account.
    - `ls` lists the contents of a folder and can help you figure out where you are.
    - `cd foldername` moves you to a subfolder of your current folder location.
    - `cd /path/to/folder` moves you to a subfolder without you having to be in its parent folder first. Use the actual path to the folder instead of /path/to/folder.
    - `cd ..` moves you up to the parent folder from your current location.
    - `mkdir foldername` will create a new subfolder with the name `foldername` in your current folder location.
1. Once you are in the folder where you want to create the project, enter the command `dotnet new console`.
1. When the command completes, use the menu option `File > Open` to open your new project. Use the file browser dialog to find and select your project folder. You'll need to open the entire folder, not a file within it.
1. If a `Required assets to build and debug are missing. Add them?` notification appears, select Yes.
1. In the terminal, run the command `dotnet run`. The words "Hello World! should appear in the terminal.
1. For further help, including a setup video that covers the steps listed above, visit the [Visual Studio Code documentation for the Hello World app](https://code.visualstudio.com/docs/languages/dotnet).

### Load the NuGet package

1. With your new project open in VSCode, open the project file (with extension .csproj) in the editor.
1. Use the key command Cmd-Shift-p on Mac or Ctrl-Shift-p on Windows to open the NuGet search dialog. Search for `Nuget Package Manager: Add Package` and select it from the suggestions dropdown.
1. Enter the package name `PossumLabs.DSL.English` into the prompt. When it appears in the dropdown, select it and hit Enter/Return.
1. When the package versions appear in the dropdown, select the `Latest Version (Wildcard *)` option and hit Enter/Return.
1. The package reference should appear in your project file.

### Add the appsettings.json file

1. Create a new json file in the project with the name `appsettings.json`. This file will serve as a configuration file that defines where the Selenium grid is located.
1. Copy the following code block into the file and save the file:

```
{
  "seleniumGridUrl": "http://localhost:4444/wd/hub",
  "seleniumRetryMs": 10000,
  "logFolder": "TestResults",
  "seleniumGridUsername": "env var, should not be used",
  "seleniumGridAccessKey": "env var, should not be used"
}
```

### Add the ImportedSteps.cs file

1. Create a new C# file in the project with the name ImportedSteps.cs. This file will import different parts of the framework. 
1. Copy the following code block into the file and save the file:

```
using BoDi;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.English.Integration
{
    public class FrameworkInitializationSteps : PossumLabs.DSL.English.FrameworkInitializationSteps
    {
        public FrameworkInitializationSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class AlertSteps : PossumLabs.DSL.English.AlertSteps
    {
        public AlertSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class DriverSteps : PossumLabs.DSL.English.DriverSteps
    {
        public DriverSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class ErrorSteps : PossumLabs.DSL.English.ErrorSteps
    {
        public ErrorSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class LogSteps : PossumLabs.DSL.English.LogSteps
    {
        public LogSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class ResolvedStringSteps : PossumLabs.DSL.English.ResolvedStringSteps
    {
        public ResolvedStringSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class TableSteps : PossumLabs.DSL.English.TableSteps
    {
        public TableSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }

    public class ValidationSteps : PossumLabs.DSL.English.ValidationSteps
    {
        public ValidationSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }
    public class WebValidationSteps : PossumLabs.DSL.English.WebValidationSteps
    {
        public WebValidationSteps(IObjectContainer objectContainer) : base(objectContainer) { }
    }
}
```

The code block above imports every part of the framework. If you already know which parts you'll need, you can remove or comment out the others.

<feedback>