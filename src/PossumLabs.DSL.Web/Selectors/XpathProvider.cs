using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.DSL.Web.Selectors
{
    public class XpathProvider
    {
        public XpathProvider()
        {
            ActiveInCell = new List<TableCellLocator>
            {
                (prefix, row, column)=>$"{prefix}/tr[{row}]/td[{column}]/*[{ActiveElements}]",
                (prefix, row, column)=>$"{prefix}/tr[{row}]/td[{column}]/div/*[{ActiveElements}]",
                (prefix, row, column)=>$"{prefix}/tr[{row}]/td[{column}]/div/div/*[{ActiveElements}]",
                (prefix, row, column)=>$"{prefix}/tr[{row}]/td[{column}]//*[{ActiveElements}]",
                (prefix, row, column)=>$"{prefix}/tr[{row}]/td[{column}]"
            };

        }

        public delegate string TableCellLocator(string prefix, int row, int column);

        public List<TableCellLocator> ActiveInCell { get; }

        public List<TableCellLocator> ContentInCell { get; }

        virtual public string TextMatch(string target)
            => $"text()[normalize-space(.)={target.XpathEncode()} or normalize-space(.)={$"{target}:".XpathEncode()}]";

        virtual public string MarkerElements
            => "( self::label or self::b or self::h1 or self::h2 or self::h3 or self::h4 or self::h5 or self::h6 or self::span or " +
            "self::em or self::strong or self::code or self::samp)";

        virtual public string ContentElements
            => "( self::label or self::a or self::b or self::h1 or self::h2 or self::h3 or self::h4 or self::h5 or self::h6 or self::span " +
            "or self::p or self::div or self::option or self::legend or self::td or self::li or self::em or self::strong or self::code or " +
            "self::samp)";

        virtual public string ActiveElements
           => "(not(@type='hidden') and ( self::a or self::button or self::input or self::select or self::textarea or @role='button' or " +
            "@role='link' or @role='menuitem' ))";

        virtual public string SettableElements
          => "(not(@type='hidden') and (self::input or self::select or self::textarea))";


        virtual public string ClickableElements
          => "(not(@type='hidden') and ( self::a or self::button or self::input or @role='button' or @role='link' or @role='menuitem' ))";

        virtual public string SelectableElements
          => "(not(@type='hidden') and ((self::input and @list  ) or self::select ))";

        virtual public string CheckableElements
          => "(not(@type='hidden') and ((self::input and @type='checkbox'  ) or self::checkbox ))";

    }
}
