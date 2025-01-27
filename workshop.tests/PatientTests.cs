using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace workshop.tests;

public class Tests
{

    [Test]
    public async Task PatientEndpointStatus()
    {
        var _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var _client = _factory.CreateClient();

        // Act
        var response = await _client.GetAsync("surgery/patients");

        // Assert
        Assert.That(System.Net.HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
    }

    [Test]
    public async Task PatientEndpointCreate()
    {
        var _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var _client = _factory.CreateClient();

        // Arrange
        var content = new StringContent("{\"FullName\":\"John Doe\"}", Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("surgery/patients", content);

        // Assert
        Assert.That(System.Net.HttpStatusCode.Created, Is.EqualTo(response.StatusCode));
    }
}