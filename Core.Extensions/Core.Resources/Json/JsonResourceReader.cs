using System;
using System.Collections;
using System.IO;
using System.Resources;
using System.Text.Json;

namespace Core.Resources.Json
{
    internal class JsonResourceReader : IResourceReader
    {
        private class KeyValueEnumerator : IDictionaryEnumerator, IDisposable
        {
            private JsonElement.ObjectEnumerator objectEnumerator;

            public KeyValueEnumerator(JsonElement.ObjectEnumerator objectEnumerator)
            {
                this.objectEnumerator = objectEnumerator;
            }

            public DictionaryEntry Entry
            {
                get
                {
                    var property = objectEnumerator.Current;
                    return new DictionaryEntry(property.Name, property.Value);
                }
            }

            public object Key => objectEnumerator.Current.Name;

            public object Value => objectEnumerator.Current.Value;

            public object Current => objectEnumerator.Current;

            public void Dispose()
            {
                objectEnumerator.Dispose();
            }

            public bool MoveNext()
            {
                return objectEnumerator.MoveNext();
            }

            public void Reset()
            {
                objectEnumerator.Reset();
            }
        }

        private readonly JsonDocument document;

        public JsonResourceReader(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            document = JsonDocument.Parse(stream);
            if (document.RootElement.ValueKind != JsonValueKind.Object)
            {
                throw new InvalidOperationException(nameof(JsonValueKind));
            }
        }

        private bool disposed;

        public void Dispose()
        {
            if (!disposed)
            {
                document.Dispose();
                disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(JsonResourceReader));
            }
        }

        public void Close()
        {
            Dispose();
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            ThrowIfDisposed();
            return new KeyValueEnumerator(document.RootElement.EnumerateObject());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            ThrowIfDisposed();
            return document.RootElement.EnumerateObject();
        }
    }
}
