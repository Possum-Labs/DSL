namespace PossumLabs.DSL.Core.Variables
{
    public interface INullCoalesceWithDefaultAttribute
    {
        Characteristics Characteristics { get; }
        string Template { get; set; }
    }
}