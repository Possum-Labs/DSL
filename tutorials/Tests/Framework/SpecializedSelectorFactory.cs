using OpenQA.Selenium;
using PossumLabs.Specflow.Selenium;
using PossumLabs.Specflow.Selenium.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyTest.Framework
{
    public class SpecializedSelectorFactory : SelectorFactory
    {
        public SpecializedSelectorFactory(ElementFactory elementFactory, XpathProvider xpathProvider) :base(elementFactory, xpathProvider)
        {
            Prefixes[PrefixNames.Under].AddRange(new List<Func<string, IEnumerable<string>>>
            {
                LooseFollowingRow,
                ParrentRowTableLayout
            });
        }



        protected Func<string, IEnumerable<string>> LooseFollowingRow =>
            (target) => TableRow(target).Select(x => $"{x}/ancestor::tr/following-sibling::tr[1]").ToList();

        virtual protected Func<string, IEnumerable<string>> ParrentRowTableLayout =>
            (target) => new List<string>() {
                $"//*[{XpathProvider.MarkerElements} and {XpathProvider.TextMatch(target)}]/ancestor::td[1]",
        };

    }
}
