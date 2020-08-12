---
layout: default
title: New Visual Studio Project
parent: Getting Started
nav_order: 5
---

## Set up a new project in Visual Studio

Before following these instructions, you will first need to complete the steps on the [Tools and Installation](tools-and-installation) page for your system.

### Create the project

1. Open Visual Studio and use the menu option File > New Solution to create a new project.
1. Select the project type `.NET Core > Tests > MSTest`. Select C# as the project language.

### Load the NuGet package

1. Load the <a href="https://www.nuget.org/packages/PossumLabs.DSL.English/1.0.0-CI-20200212-235618" target="_blank">PossumLabs.DSL.English Nuget package</a>. This package provides the core libraries and functionality to support the creation of domain-specific languages for test automation.

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