using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Diagnostics;

namespace Core.Net.Http
{
    /// <summary>
    /// Provides extension methods for <see cref="HttpClient"/>
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Downloads the file resource to a local path using the HTTP Get method.
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> used to send HTTP requests.</param>
        /// <param name="requestUri">The Uri to send requests.</param>
        /// <param name="fileName">The local path to the downloaded file.</param>
        /// <param name="cancellationToken">
        /// A token used to receive cancellation requests.
        /// </param>
        /// <returns>The task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="client"/> or <paramref name="requestUri"/> or
        /// <paramref name="fileName"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="requestUri"/> or <paramref name="fileName"/> is empty.
        /// </exception>
        public static async Task GetFileAsync(
            this HttpClient client,
            string requestUri,
            string fileName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Requires.NotNull(client, nameof(client));
            Requires.NotNullOrEmpty(requestUri, nameof(client));
            Requires.NotNullOrEmpty(fileName, nameof(fileName));

            using (var stream = await client.GetStreamAsync(requestUri))
            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(file, 4096, cancellationToken);
            }
        }

        /// <summary>
        /// Downloads the file resource to a local path using the HTTP Get method.
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> used to send HTTP requests.</param>
        /// <param name="requestUri">The Uri to send requests.</param>
        /// <param name="fileName">The local path to the downloaded file.</param>
        /// <param name="cancellationToken">
        /// A token used to receive cancellation requests.
        /// </param>
        /// <returns>The task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="client"/> or <paramref name="requestUri"/> or
        /// <paramref name="fileName"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="requestUri"/> or <paramref name="fileName"/> is empty.
        /// </exception>
        public static async Task GetFileAsync(
           this HttpClient client,
           Uri requestUri,
           string fileName,
           CancellationToken cancellationToken = default(CancellationToken))
        {
            Requires.NotNull(client, nameof(client));
            Requires.NotNull(requestUri, nameof(client));
            Requires.NotNullOrEmpty(fileName, nameof(fileName));

            using (var stream = await client.GetStreamAsync(requestUri))
            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(file, 4096, cancellationToken);
            }
        }
    }
}
