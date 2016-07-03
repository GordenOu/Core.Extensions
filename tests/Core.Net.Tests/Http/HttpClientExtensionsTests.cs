using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Net.Http.Tests
{
    public class MockHandler : HttpMessageHandler
    {
        public string Response { get; }

        public MockHandler(string response)
        {
            Response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                Content = new StringContent(Response)
            });
        }
    }

    [TestClass]
    public class HttpClientExtensionsTests
    {
        [TestMethod]
        public async Task GetFileAsyncPreconditions()
        {
            var handler = new MockHandler("Hello");
            HttpClient client = null;
            string requestUri = "http://dot.net";
            string tempFileName = Path.GetTempFileName();

            await AssertAsync.ThrowsException<ArgumentNullException>(async () =>
            {
                await client.GetFileAsync(requestUri, tempFileName);
            });
            await AssertAsync.ThrowsException<ArgumentNullException>(async () =>
            {
                await client.GetFileAsync(new Uri(requestUri), tempFileName);
            });

            client = new HttpClient(handler);
            await AssertAsync.ThrowsException<ArgumentNullException>(async () =>
            {
                await client.GetFileAsync((string)null, tempFileName);
            });
            await AssertAsync.ThrowsException<ArgumentNullException>(async () =>
            {
                await client.GetFileAsync((Uri)null, tempFileName);
            });

            await AssertAsync.ThrowsException<ArgumentException>(async () =>
            {
                await client.GetFileAsync(string.Empty, tempFileName);
            });

            await AssertAsync.ThrowsException<ArgumentNullException>(async () =>
            {
                await client.GetFileAsync(requestUri, null);
            });
            await AssertAsync.ThrowsException<ArgumentNullException>(async () =>
            {
                await client.GetFileAsync(new Uri(requestUri), null);
            });
            await AssertAsync.ThrowsException<ArgumentException>(async () =>
            {
                await client.GetFileAsync(requestUri, string.Empty);
            });
            await AssertAsync.ThrowsException<ArgumentException>(async () =>
            {
                await client.GetFileAsync(new Uri(requestUri), string.Empty);
            });
        }

        [TestMethod]
        public async Task GetFileAsync()
        {
            var handler = new MockHandler("Hello");
            var client = new HttpClient(handler);

            string tempFileName = Path.GetTempFileName();
            await client.GetFileAsync("http://dot.net", tempFileName);
            Assert.AreEqual(handler.Response, File.ReadAllText(tempFileName));

            tempFileName = Path.GetTempFileName();
            await client.GetFileAsync(new Uri("http://dot.net"), tempFileName);
            Assert.AreEqual(handler.Response, File.ReadAllText(tempFileName));
        }
    }
}
