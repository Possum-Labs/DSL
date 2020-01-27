using PossumLabs.DSL.Core.Variables;
using System.IO;

namespace PossumLabs.DSL.Core.Files
{
    public interface IFile: IEntity
    {
        Stream Stream { get; }
    }
}