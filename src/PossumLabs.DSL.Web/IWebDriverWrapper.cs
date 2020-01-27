using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Web
{
    public interface IWebDriverWrapper
    {
        IWebDriver IWebDriver { get; }
    }
}
