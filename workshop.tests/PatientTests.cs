using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace workshop.tests;

public class Tests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    [SetUp]
    public void Setup()
    {
        // Arrange
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task PatientEndpointStatus()
    {

        // Act
        var response = await _client.GetAsync("surgery/patients");

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task PatientEndpointCreate()
    {
        // Arrange
        var content = new StringContent("{\"FullName\":\"John Doe\"}", Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("surgery/patients", content);

        // Assert
        Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, $"Expected Created but got {response.StatusCode}");
    }
}