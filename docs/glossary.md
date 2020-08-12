---
layout: default
title: Glossary
nav_order: 6
---

# Glossary
{: .no_toc }

## Contents
{: .no_toc .text-delta }

1. TOC
{:toc}

### Anti-corruption layer

An anti-corruption layer is a form of insulation that protects tests from needing to change when changes are made to the application that is being tested. Most applications change, evolve, and are refactored over time. When the application changes, an anti-corruption layer means that the tests will need few (if any) changes.

With DSL testing, tests are written in a human-readable format that is resolved to code behind the scenes. The underlying test framework and the core application being tested can both change without needing to rewrite the tests themselves (in most cases).

### Application programming interface (API)

An API is a piece of software that handles communication coming from other programs. REST and GraphQL are technologies that are used to implement APIs.

### Attribute

Attributes are a C# concept similar to "tags" in Gherkin. They are non-functional documentation elements that other systems may interpret. 

It might help to think of them as hashtags in tweets, but for code.

### Behavior-Driven Development (BDD)

BDD is a development methodology that was introduced around 2008 to help developers know exactly what they need to build. It helps with the practice of creating a specification before implementation. These specifications are in a form that are both machine-parsable and human-readable, which means the specifications act as the acceptance tests for the development work.

### Bounded context

Words have meanings within a context. For instance, when you are talking about cars, "gas" refers to gasoline, whereas in a physics class, "gas" might mean a state like solid or liquid. This pattern also applies in large organizations, where the UI team has a clear idea of what an "account" is, but the CFO has a different meaning for "account".

A bounded context defines a set of related things. Within Domain-Driven Design (DDD), a bounded context is a pattern that helps deal with large models by breaking up them up into parts (contexts).

When creating a Domain-Specific Language, it's important to be clear about what is in or out of context to minimize confusion. Defining context may mean that in large organizations you will need multiple bounded contexts and multiple Domain-Specific Languages.

### Container

A container is similar to a virtual machine. It bundles up code with its runtime and with everything else needed to run an application. Within the Possum Labs DSL project, containers make it possible to run multiple instances of browsers at the same time, which speeds up testing. (See the [Docker entry](#docker) for more on containers.)

### Context Map

A Context Map is produced through context mapping, which is a method that allows you to identify the relationships between bounded contexts and the relationships between the teams that are responsible for them.

### Cucumber

Cucumber is an interpreter implementation of Gherkin. (Specflow is also an interpreter of Gherkin.) The interpreter is the part that reads the feature files and triggers the code for scenario execution.  

### Docker

Docker is a tool that makes it possible to run multiple, isolated copies of applications at the same time. It has less overhead than running individual virtual machines for each application, because all the containers running the applications share a single operating system kernel. Automated tests for websites will run faster if you can run them in multiple instances of browsers at the same. Docker makes that possible. When running browsers in Docker for the purpose of testing, the browsers are often run in a headless state. (See the [Headless entry](#headless) for more.)

### Domain

A domain is an organizational construct that helps to group concepts. In our context, it refers to ideas, processes, and concepts.

### Domain-Driven Design (DDD)

DDD is a set of practices that focus on placing the problem that the business solves at the center of the software. Using the language and concepts of the business in the implementation helps to reduce the risk of mistakes.

This practice is in contrast to implementations that focus on the technology and which subsequently may collapse multiple business concepts into single entity for efficiency. 

### Domain-Specific Language (DSL)

A domain-specific language is a limited-use language made up of words that have meaning within a certain context. That context, or domain, is often a product, business, or workflow. The words in a domain-specific language can be combined and used to accomplish specific tasks. Alexa, Cortana, and Siri are examples of domain-specific languages: you can interact with them in simple, scripted ways to accomplish tasks.

### Dot notation

Dot notation is a common way of accessing variables in programming languages. Let's say we have a dog called "Loebas", and we would like to get the color of his coat, which is stored as an attribute. This information would read as `Loabas.Coat.Color` in dot notation.  

### Enterprise service bus (ESB)

An ESB is a system that uses messages that are broadcast and subscribed to by software components. This is in contrast to sending information to specific locations via API calls.

### Entity/Entities

Entities are the nouns that are the most meaningful in your organization. They can be things like Customer, Order, Shipment, Widget, and so on.

### Feature

A features is an organizational unit for scenarios that wish to share a background. It is also the unit at which scenarios are persisted on disk. Your feature files will contain your scenarios. 

### Gherkin

Gherkin is a set of structured grammar rules that can be interpreted by Cucumber. In English, the grammar consists of the Given, When, Then syntax.

### Git

Git is a free, open source tool for source control management. It is primarily used in software development for tracking changes to source code, and it can be used and managed via the command line or any number of third-party applications that provide graphical user interfaces.

### Headless

Running software headless (e.g. running a browser headless) means running it without its graphical user interface. When running browsers in containers for automated testing, the graphical user interface isn't usually necessary, because there isn't a person watching every single test play out. Despite running headless, it's still possible to record screenshots of the tests that can be reviewed after the test is over. The screenshots can also be compiled into a video.

### Keywords

Keywords are reserved words in programming languages that have a specific meaning and function within the language. For instance, "Give," "When," and "Then" are keywords in Gherkin. When these words are used in specific contexts they will be interpreted to trigger functionality. 

### Model

A model is an abstraction of data and processes that is useful for communicating with people or software. 

### Model-Driven Design (MDD)

MDD is an analytical process that involves describing software using many models. It focuses less on the business compared to Domain Driven Design, but there is overlap.  

### Mono

[Mono](https://www.mono-project.com/) is an open source implementation of Microsoft's .NET Framework that makes it possible to develop cross-platform applications. Supported platforms include Windows, MacOS, Linux, and [others](https://www.mono-project.com/docs/about-mono/supported-platforms/).

### .NET, .NET Standard, and .NET Core

.NET is a framework that is used to create Windows applications. .NET Standard is a set of APIs that all .NET implementations must use. .NET Core is an open source, cross-platform implementation of .NET Standard. While .NET is only used to develop Windows applications, .NET Core can be used to develop applications for all major operating systems, including Windows, MacOS, and Linux. The PossumLabs.DSL example project is compatible with .NET Core.

### Open Source

Open Source refers to software where the source code is available for inspection and compilation. This access allows developers to dive in and understand exactly how the code works even if they work for a different organization. It usually also allows people to contribute to these projects.

Open Source is a bit like Wikipedia, except that there is an approval process for changes that is managed by the owners of each individual project.

### Property

A property is a data attribute of an object that is represented in code. For instance, a drivers license object would have an expiration date attribute as well as name, date of birth, and many others. These attributes don't have to be simple data types and can be complex objects that have their own properties.  

### Quality Assurance (QA)

Quality Assurance is the implementation of structured methods of testing products to ensure that they maintain certain standards of quality. In the software industry, QA, testing, and QA testing are sometimes (though not always) used interchangeably.

### Selenium

Selenium is an open source suite of software that provides a standardized API to interact with different browsers. It is very popular for testing websites, and many other tools have been build on top of Selenium.

### Selenium grid

The Selenium grid is a configuration that includes a Selenium controller and Selenium Nodes, which provide a collection of browsers that can run tests for you. The controller provides the best matching browser for tests depending on what it has available. 

Although setting up the Selenium grid can be complex, it can also be as brief as a single Docker compose file.

### Source Control

Source control, also known as version control, is change management for sets of information like source code and documents. Source control tools like [Git](#git) allow individuals or groups of people to make and keep track of changes to data and resources, merge and resolve conflicting changes, and reverse or revert changes if needed.

Distributed version control is a kind of source control where everyone working on a project has a complete copy of it mirrored on their own computer as well as a complete history of its changes.

### SpecFlow

[SpecFlow](https://specflow.org/) is a testing framework that supports a domain-specific language called Gherkin. SpecFlow integrates with Visual Studio and provides testing solutions for .NET platforms, including the .NET Desktop Framework, .NET Core, and Mono. It is an open source port of Cucumber.

### Test-Driven Development

The practice of Test-Driven Development was introduced around the year 2000 to prevent developers from building incorrect code. The idea is that the developer writes tests before they write code in an effort to ensure that the code works according the developer's expectations. 

Test-Driven Development is different from Behavior-Driven Development, because the tests in TDD are written by the developer and not in collaboration with the team like in BDD.

### Ubiquitous language

A ubiquitous language is a subset of English (or any language) where all the words have a clear meaning understood by the people who work on the domain. In practice, a ubiquitous language will contain a tiny set of words relative to English, without synonyms and with few adjectives. 

The goal of a ubiquitous language is to ensure that the people using it have clarity in their communications, without any ambiguity. Some examples where you might find ubiquitous language in the workplace are rules, regulations, and work orders.

### Variable

In code, a variable is a named piece of data. Let's say there are two cats: Whiskers and Purrcival. If we want to communicate about these specific cats, we don't want to keep saying first cat and second cat, so instead we refer to them by their names. This will allow us to refer to Whiskers.Coat.Color (for example) to retrieve data attributes about Whiskers without any ambiguity as to which cat we are referencing.   

### Visual Studio

[Visual Studio](https://visualstudio.microsoft.com/) is a full-featured integrated development environment (IDE) from Microsoft. It can be used to develop and debug software, apps, and websites. The [Visual Studio Community edition](https://visualstudio.microsoft.com/free-developer-offers/) is free, while the Professional and Enterprise editions are subscription-based. Visual Studio is primarily available for Windows. There is also a version available for MacOS, but it can only be used with .NET Core.

### Visual Studio Code (VSCode)

[Visual Studio Code](https://code.visualstudio.com/) (VSCode for short) is a free, cross-platform text editor for managing source code. It includes developer tools and optional extensions that help with language support, testing, and debugging, and that make it a viable alternative to Visual Studio for many projects. It is available for Windows, MacOS, and some Linux distributions.

### Web driver

A web driver is a piece of middleware used in Selenium to interact with a specific web browser like Chrome or Edge.

### XPath (XML Path Language)

XPath is a query language for XML documents. You can use XPath queries to select XML nodes and compute values. In the PossumLabs.Specflow.Selenium project, XPaths work behind the scenes to find and select HTML elements on a webpage. The [XPath standards](https://www.w3.org/TR/xpath/all/) are maintained by the World Wide Web Consortium.


<feedback>
