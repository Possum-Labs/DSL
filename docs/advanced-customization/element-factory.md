---
layout: default
title: Element Factory
parent: Advanced Customizations
nav_order: 3
---

# Customizing the Element Factory

The element factory is the part of the Possum Labs framework that creates elements, which are wrappers around Selenium web element objects. These wrappers make it easier to interact with web element objects and to set values. For instance, to set the value of a standard Selenium element, you would typically use the sendkeys() method, which simulates rapid keyboard strokes. For a settable element in the Possum Labs framework, you can specify the value you would like an element to have, and it will try a number of methods behind the scenes to set the value.

This functionality makes the Possum Labs framework tolerant of HTML, style, and input changes, so that tests will usually still pass after code base updates. During a test, the framework will clear out any existing values from an element, set the new value, wait and try to set it again if necessary, and use JavaScript to set the value if needed. For example, some options in drop-downs can be hard for the framework to find or identify. By customizing the element factory, you can identify elements with your tests that may be unique to your product's implementation.

With some customizations, you may need to leverage the custom helpers that are present in your client-side HTML.

[Jump to video](#video-tutorial)

### Open the example project

1. Open the `Tutorials > Advanced` folder in the Possum Labs DSL project and expand the project `3 Customizing the ElementFactory`.

### Create an Element Override

1. Open the CustomSloppySelectElement.cs file in the example project.
1. To customize the element factory, you will need to create an override for each element you would like to modify. Let's say that you would like to look for options in a drop-down regardless of case sensitivity. CustomSloppySelectElement.cs contains two overrides which transform the values of an HTML `<select>` drop-down list to all capital letters.
```
protected override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindByContains(string id, string key)
            => base.WebDriver.FindElements(
                By.XPath($"//select[@id='{id}']/option[contains(" +
                $"translate(@value,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key}') or contains(" +
                $"translate(@custom-option-identifier,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key}') or contains(" +
                $"translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'), '{key}')]"));
```
With this customization, you can write your tests in any case, and behind the scenes it will uppercase both the input and the options, which effectively makes it case insensitive. Any options in lowercase or camelcase will still match the search string after they've been transformed to all caps.


### Add the Element Override to the Element Factory

1. Open the CustomElementFactory.cs file in the example project.
1. In this example, the CustomSloppySelectElement rule has been added to the Element Factory with the following override:

```
override public Element Create(IWebDriver driver, IWebElement e)
        {
            var list = e.GetAttribute("list");
            var id = e.GetAttribute("id");
            var tag = e.TagName;
            var location = e.Location;
            if (e.TagName == "select" || (e.TagName == "input" && !string.IsNullOrEmpty(list)))
                return new CustomSloppySelectElement(e, driver);
            return base.Create(driver, e);
        }
```
This override replaces the framework's default handling of `<select>` elements with the CustomSloppySelectElement behavior. You can follow this pattern for all of your custom element overrides.

### Video Tutorial

<iframe width="560" height="315" src="https://www.youtube.com/embed/wFkSraKdXmo" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

<feedback>
