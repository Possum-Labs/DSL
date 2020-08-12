---
layout: default
title: Core vs. Web
parent: Getting Started
nav_order: 7
---

## Core vs. Web vs. English

The Possum Labs DSL project is broken up into several different packages. This separation allows you to integrate the project at the level you prefer without needing to import the parts that you don't need. A big reason for this separation is that the Web project uses Selenium, which is a large dependency.

### Core

The core package has the logic for dealing with variables. It has the least number of dependencies.

### Web

The web package has a dependency on Selenium, which makes it possible to test webpages.

### English

The English project includes default implementations for simple steps and has a dependency on Specflow. 

### Which one should you use?

We recommend using the English project when getting started. 

If you are worried about dependencies or if there is a conflict, use the lower-level packages.

If you work in a language other than English, our advice is to start a prototype with the English package and then replace English with your desired language.

If there is already a package available in your preferred language, then we recommend starting with that.

<feedback>
