using System;
using System.Globalization;
using System.Resources;

namespace Core.Resources.Json
{
    internal class JsonResourceManager : ResourceManager
    {
        private const string jsonExtension = ".json";

        public override string BaseName { get; }

        public JsonResourceManager(Type resourceSource)
            : base(resourceSource.Name, resourceSource.Assembly, typeof(JsonResourceSet))
        {
            BaseName = resourceSource.FullName;
        }

        public override Type ResourceSetType => typeof(JsonResourceSet);

        protected override string GetResourceFileName(CultureInfo culture)
        {
            return $"{BaseName}{jsonExtension}";
        }
    }
}
