namespace PossumLabs.DSL.Core.Logging
{
    public interface IMovieLogger
    {
        bool IsEnabled { get; set; }

        void AddScreenShot(byte[] image);
        void ComposeMovie();
        void StepEnd(string step);
    }
}