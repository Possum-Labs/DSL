using BoDi;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSL.Documentation.Example
{
    public class DriverSteps : PossumLabs.DSL.English.DriverSteps
    {
        public DriverSteps(IObjectContainer objectContainer, Pages pages) : base(objectContainer) {
            Pages = pages;
        }
        private Pages Pages { get; }

        protected override void GivenNavigatedTo(string page)
        {
            base.GivenNavigatedTo(Pages.MapUrl(page));
        }
    }
}
