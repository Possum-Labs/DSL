using PossumLabs.DSL.Web;
using PossumLabs.DSL.Web.Selectors;
using System;
using System.Collections.Generic;
using System.Text;

namespace DSL.Documentation.Example
{
    /// <summary>
    /// Only the common changes are enumerated below, there are more xpaths that
    /// are provided and if this does not offer enough please consider checking 
    /// out the source or look at the Customizing Selectors example
    /// </summary>
    /// 
    ///Please make sure to look at FrameworkInitializationSteps to see where we register this class
    public class CustomXpathProvider : XpathProvider
    {
        public CustomXpathProvider() : base()
        {
        }

        /// <summary>
        /// this is how we match text in elements.
        /// You'd change this if you have a style guide that specifies things like
        /// All labels must end in a : (or one that sais they don't)
        /// If you are really unlucky and work with code old enough to be littered 
        /// with non breaking white spaces then you will have to replace / trim those 
        /// here.
        /// </summary>
        override public string TextMatch(string target)
            => $"text()[normalize-space(.)={target.XpathEncode()} or normalize-space(.)={$"{target}:".XpathEncode()}]";

        /// <summary>
        /// what kind of elements do we look at for markers, again you can remove what your 
        /// style guid forbids
        /// </summary>
        override public string MarkerElements
            => "( self::label or self::b or self::h1 or self::h2 or self::h3 or self::h4 or self::h5 or self::h6 or self::span or " +
            "self::em or self::strong or self::code or self::samp)";

        /// <summary>
        /// places we search for content selectors, remove what is not valid for your style guide
        /// </summary>
        override public string ContentElements
            => "( self::label or self::a or self::b or self::h1 or self::h2 or self::h3 or self::h4 or self::h5 or self::h6 or self::span " +
            "or self::p or self::div or self::option or self::legend or self::td or self::li or self::em or self::strong or self::code or " +
            "self::samp)";
    }
}
