---
layout: default
title: The Slipka Proxy
parent: Advanced Customizations
nav_order: 5
---

**Don't use Slipka unless it solves a problem you have**

# The Slipka Proxy

Slipka is a proxy that makes files downloaded by the browser accessible to your tests. If you’re downloading a report (for example, a PDF file) as part of a test, that file will be downloaded by the browser. Since the browser is running in a container, you don’t have direct access to the browser or the operating system it's running in, and you don’t know how long downloaded files will persist. Selenium doesn’t natively support a way to access downloaded files.

Slipka is unique in that it provides a short lives proxy that can be created on a per test basis. It also provides the ability to record and tamper with trafic. 

The Slipka proxy sits in between the browser and the website that is being tested, and is configured to look for certain types of traffic. 

It can also be configured to inject a response, augment traffic by adding additional headers, and record files and traffic.

After calling Slipka to set up a proxy, Slipka returns the proxy and its ports. You can then talk to the Slipka proxy server on that port, and any traffic Slipka records will be available later.

Don't use Slipka unless it solves a problem you have. There are known limitations to how many concurrent proxies a single Slipka can support without creating noise (between 10 and 20). It also is unable to process the PATCH verb.

[Jump to video](#video-tutorial)

### Open the example project

1. Open the `Tutorials > Advanced` folder in the Possum Labs DSL project and expand the project `4 Using Slipka`.

### Configure Slipka to intercept files

1. Open `English > SlipkaSteps.cs` in the example project. This file gives a sample implementation for configuring Slipka to intercept files.
1. Look for the following line: `public void GivenUsingAProxy()
            => WebDriverManager.BaseUrl =
            new Uri($"http://slipka:{Proxy.Value.ProxyUri.Port}");` This statement is necessary to override the URL with the one from Slipka.
1. Next, configure the proxy to intercept certain files types. The `ReportAttribute()` function in `SlipkaSteps.cs` shows an example for how to intercept three file types:

```
public void ReportAttribute()
        {
            GivenProxyLogsResponsesOfType("Content-Type", "application/pdf");
            GivenProxyLogsResponsesOfType("Content-Type", "application/vnd.ms-excel");
            GivenProxyLogsResponsesOfType("Content-Type", "application/vnd.openxml");
        }
```

`SlipkaSteps.cs` in not included in any package and there is no default implementaiton.

### Docker-compose

1. Open `docker-compose.yml`.
1. This sample docker-compose file configures a grid with Slipka and shows how to wire up a sample test environment.

### Using Slipka in a feature file

1. Open `Features > Slipka.feature`

### Video Tutorial

<iframe width="560" height="315" src="https://www.youtube.com/embed/gjraFjBDHZ4" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

<feedback>