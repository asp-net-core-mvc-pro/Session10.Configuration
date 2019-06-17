using Microsoft.AspNetCore.Http;
using Session10.Configuration.Infrastructures;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Session10.Configuration.Tests.Infrastructures
{
    public class ShortCircuitMiddlewareTests
    {
        private readonly Task _onNextResult = Task.FromResult(0);
        private readonly RequestDelegate _onNext;
        private readonly DefaultHttpContext _context;

        public ShortCircuitMiddlewareTests()
        {
            _onNext = _ =>
            {
                return _onNextResult;
            };
            _context = new DefaultHttpContext();
        }

        [Fact]
        public async Task WhenBrowserIsEdgeThenStatusCode403()
        {
            // arrange
            var secureHeadersMiddleware = new ShortCircuitMiddleware(_onNext);
            _context.Items.Add("EdgeBrowser", "true");

            // act
            await secureHeadersMiddleware.Invoke(_context);

            Assert.True(_context.Response.StatusCode == 403);
        }
    }
}
