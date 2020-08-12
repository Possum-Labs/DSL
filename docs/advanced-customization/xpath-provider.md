---
layout: default
title: XPath Provider
parent: Advanced Customizations
nav_order: 1
---

# Customizing the XPath Provider

<a href="https://www.w3schools.com/xml/xpath_intro.asp" target="_blank">XPath</a> is a query language that is used to find and select XML nodes. In a Web page, XPath can be used to find and identify HTML elements.

During an automated test, the Possum Labs framework searches for elements on Web pages that match the elements defined in the test steps. The XPath provider lists which types of elements the framework should look for, categorized by function. These categories include content elements, clickable elements, settable elements, and more. The full list can be found in `XpathProvider.cs` in the `PossumLabs.DSL.Web > Selectors` folder. 

If your tests need to follow a style guide, or if you are using different HTML tags than those defined in the framework, you can create a custom XPath provider to add and remove HTML element types within each category. You can also change how content is matched and replace or trim non-breaking white spaces.

[Jump to video](#video-tutorial)

### Open the example project

1. Open the `Tutorials > Advanced` folder in the Possum Labs DSL project and expand the project called `1 Customizing the XpathProvider`.

### Create and Register the Custom XPath Provider

1. Open the file in the project called `CustomXpathProvider.cs`. This file defines a new XpathProvider called CustomXpathProvider.
1. Expand the `English` folder and open `FrameworkInitializationSteps.cs`. 
1. Find the line in the file that reads `Register<XpathProvider>(new CustomXpathProvider());`. This line registers the CustomXpathProvider that is defined in `CustomXpathProvider.cs`. There can only be one XPath provider, so all overrides must go in a single file. 

### Create XPath Overrides

1. Return to `CustomXpathProvider.cs`. 
1. Several example overrides have been included to show syntax. To override an element category, copy in the corresponding statement from `XpathProvider.cs` (found in the `PossumLabs.DSL.Web > Selectors`). Add or remove element types as needed, and leave the rest of the list as part of the override. The lists are not prioritized, so you can add element types in any order.

### XPath Tutorials

The [W3C](https://www.w3.org/) maintains the [XPath standards](https://www.w3.org/TR/xpath/all/). If you would like to learn more about XPath syntax, there is an [XPath tutorial](https://www.w3schools.com/xml/xpath_intro.asp) available as a part of [W3schools](https://www.w3schools.com/).

### Video Tutorial

<iframe width="560" height="315" src="https://www.youtube.com/embed/VROYhuw2w0A" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

<feedback>
