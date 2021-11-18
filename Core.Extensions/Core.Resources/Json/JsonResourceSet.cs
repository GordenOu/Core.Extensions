using System.Resources;

namespace Core.Resources.Json;

internal class JsonResourceSet : ResourceSet
{
    public JsonResourceSet(Stream stream)
        : base(new JsonResourceReader(stream))
    { }

    public JsonResourceSet(string fileName)
        : this(File.OpenRead(fileName))
    { }

    public override Type GetDefaultReader()
    {
        return typeof(JsonResourceReader);
    }

    public override Type GetDefaultWriter()
    {
        throw new NotSupportedException();
    }
}
