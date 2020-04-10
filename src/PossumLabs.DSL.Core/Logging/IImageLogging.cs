namespace PossumLabs.DSL.Core.Logging
{
    public interface IImageLogging
    {
        byte[] AddTextToImage(byte[] image, string text);
    }
}