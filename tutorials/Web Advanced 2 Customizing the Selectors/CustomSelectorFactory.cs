using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSL.Documentation.Example
{
    /// <summary>
    /// this is where you can make the selectors less or more permissive.
    /// </summary>
    ///
    ///Please make sure to look at FrameworkInitializationSteps to see where we register this class
    public class CustomSelectorFactory : SelectorFactory
    {
        public CustomSelectorFactory(ElementFactory elementFactory, XpathProvider xpathProvider) : base(elementFactory, xpathProvider)
        {
            /// To make the prefixes more permissable
            /// just append to the list
            Prefixes[PrefixNames.Error].AddRange(new List<Func<string, IEnumerable<string>>>
            {
                ModalError
            });

            /// To make the selectors more permissable for a type
            /// just append to the list
            Selectors[SelectorNames.Active].AddRange(new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>
            {
                NextCell(XpathProvider.ActiveElements),
                ByTTSelectable,
                PreviousCell(XpathProvider.ActiveElements)
            });

            /// To make the selectors less permissive use the following pattern.
            /// See the source to see what selectors are included by default.
            Selectors[SelectorNames.Settable].Clear();
            Selectors[SelectorNames.Settable].AddRange(new List<Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>>>
            {
                ByForAttribute,
                ByNestedInLabel(XpathProvider.SettableElements),
                ByNested(XpathProvider.SettableElements),
                ByText(XpathProvider.SettableElements),
                ByLabelledBy,
                RadioByName,
                ByFollowingMarker(XpathProvider.SettableElements),
                ByCellBelow(XpathProvider.SettableElements),
                ByLabelAncestor(XpathProvider.ActiveElements),
            });

            /// DO NOT ASSUME THAT THE SELECTOR AT POSITION 4 WILL STAY AT POSITION 4
            /// so clear and add them, don't try and snipe one or two from the list.
        }

        /// <summary>
        /// Element selectors
        /// </summary>

        //used for twitter type ahead select boxes
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> ByTTSelectable =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                $"{prefix}//div[contains(@class,'tt-selectable')]/*[{XpathProvider.TextMatch(target)}]");

        // look in next table cell, to the right, in a table layout
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> NextCell(string elementType) =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                $"({prefix}//td[{XpathProvider.TextMatch(target)} or *[{XpathProvider.TextMatch(target)}]]/following-sibling::td)[1]/*[{elementType}]");

        // look in previous cell, to the left in a table layout
        virtual protected Func<string, IEnumerable<SelectorPrefix>, IWebDriver, IEnumerable<Element>> PreviousCell(string elementType) =>
            (target, prefixes, driver) => Permutate(prefixes, driver, (prefix) =>
                $"({prefix}//td[{XpathProvider.TextMatch(target)} or *[{XpathProvider.TextMatch(target)}]]/preceding-sibling::td)[1]/*[{elementType}]");

        /// <summary>
        // the following are prefix customizations; 
        // these are used for looking on a row or under something, or in errors
        /// </summary>

        // Error messages often require custom selectors as most companies build their own error messaging structures
        virtual protected Func<string, IEnumerable<string>> ModalError =>
         (target) => new List<string>() {
                $"//div[@class='modal-content' and div/h4[{XpathProvider.TextMatch("ERROR")}]]/div/div[@class='bootbox-body']",
        };

        virtual protected Func<string, IEnumerable<string>> TextInCell =>
            (target) => new List<string>() {
                $"//td[{XpathProvider.TextMatch(target)}]",
        };


    }
}
