using System.Collections.Generic;

namespace PossumLabs.DSL.Web.Selectors
{
    public interface IXpathProvider
    {
        string ActiveElements { get; }
        List<XpathProvider.TableCellLocator> ActiveInCell { get; }
        string CheckableElements { get; }
        string ClickableElements { get; }
        string ContentElements { get; }
        List<XpathProvider.TableCellLocator> ContentInCell { get; }
        string FilterHidden { get; }
        string MarkerElements { get; }
        string SelectableElements { get; }
        string SettableElements { get; }

        string TextMatch(string target);
    }
}