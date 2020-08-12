---
layout: default
title: Selector Factory
parent: Advanced Customizations
nav_order: 2
---

# Customizing the Selector Factory

The selector factory builds a composite XPath to find elements on web pages. It uses the XPath provider to identify and combine individual elements.

During an automated test, the Possum Labs framework searches for elements on Web pages that match the element name or content defined in the test step. The framework cycles through a prioritized list of XPath expressions to find the element in the site. This list of XPath expressions helps make tests flexible so that they keep passing when stylistic changes are made to the site. Although the Selector Factory in the framework attempts to cover the most common scenarios for identifying elements, you can also customize it and change how XPaths are built and how they are prioritized.

[Jump to video](#video-tutorial)

### Open the example project

1. Open the `Tutorials > Advanced` folder in the Possum Labs DSL project and expand the project `2 Customizing the Selectors`.

### Create and Register the Custom Selector Factory

1. Open the file in the project called `CustomSelectorFactory.cs`. This file defines a new SelectorFactory called CustomSelectorFactory.
1. Expand the `English` folder and open `FrameworkInitializationSteps.cs`. 
1. Find the line in the file that reads `Register<SelectorFactory>(new CustomSelectorFactory(ElementFactory, XpathProvider).UseBootstrap());`. This line registers the CustomSelectorFactory that is defined in `CustomSelectorFactory.cs`.

### Add and Remove

### Video Tutorial

<iframe width="560" height="315" src="https://www.youtube.com/embed/Xw6ZRSDal4E" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

<feedback>
