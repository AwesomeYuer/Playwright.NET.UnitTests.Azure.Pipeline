using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net;

namespace MoqHttpClientUnitTests;
[TestFixture]
public class MoqHttpClientNUnitTests
{
    [Test]
    public void Test()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        var baseAddress = "https://www.fake.com";
        var magicHttpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri(baseAddress)
             ,
            Timeout = TimeSpan.FromSeconds(5)
        };
        handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync
                (
                    () =>
                    {
                        var json = $@"{{ ""result"" : ""Moq {nameof(HttpClient)}.{nameof(HttpClient.SendAsync)}.{nameof(HttpResponseMessage)}"" }}";
                        return
                                new HttpResponseMessage
                                {
                                    StatusCode = HttpStatusCode.OK,
                                    Content = new StringContent(json)
                                };
                    }
                ).Verifiable();


        var relativeUrl = $"fake";
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(relativeUrl, UriKind.Relative))
        {
            Content = new StringContent("{}")
        };
        var responseMessage = magicHttpClient.SendAsync(requestMessage).Result;
        Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var json = responseMessage.Content.ReadAsStringAsync().Result;
        Console.WriteLine(responseMessage.StatusCode);
        Console.WriteLine(json);
        var result = JObject.Parse(json);
        Assert.IsNotNull(result);
        Assert.IsTrue(result is not null);
        Assert.IsTrue(result!["result"]!.Value<string>()!.StartsWith("moq", StringComparison.OrdinalIgnoreCase));

    }
}
