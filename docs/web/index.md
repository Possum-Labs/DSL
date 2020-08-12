---
layout: default
title: Web Tutorials
nav_order: 4
has_children: true
---

## DSL.Web Tutorials

The tutorials fall into three categories: Core for the parts dealing with variables, Web for interacting with webpages, and Advanced for modifying and replacing components of the framework. This section covers the tutorials for working with websites.

We will be relying on the English package for these tutorials. 

The tutorials will cover what is provided in the PossumLabs.DSL library for interacting with webpages. These are very generic low-level steps for interacting with websites, along the lines of clicking and entering data. The framework will act similarly to some frameworks that depend on AI, except we leverage xPaths that are evaluated behind the scenes. Chances are you won't need to worry about how it works, and you will be able to refer to elements on the page by the visible test without creating any page objects.

This way of referencing elements is a small time-saver upfront that becomes much larger when pages change and you have to maintain the page objects. It means that most changes to your webpages won't require you to write new tests. It also may allow you to work with webpages that weren't built for interaction with Selenium and automated testing. 

Even though you can reference elements, you should still try and limit doing so in scenarios to keep them concise and readable. A few lines of clicking and entering text is easy to follow, but once you have more than five lines of it, people get lost when trying to read and maintain the tests. In those cases, you can create steps that alias those entering and clicking steps behind the scenes.   

<feedback>
