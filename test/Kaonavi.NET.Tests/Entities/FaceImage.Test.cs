using Kaonavi.Net.Entities;
using Kaonavi.Net.Json;

namespace Kaonavi.Net.Tests.Entities;

/// <summary><see cref="FaceImage"/>の単体テスト</summary>
[TestClass, TestCategory("Entities")]
public sealed class FaceImageTest
{
    /// <summary>JSONからデシリアライズできる。</summary>
    [TestMethod($"{nameof(FaceImage)} > JSONからデシリアライズできる。"), TestCategory("JSON Deserialize")]
    public void CanDeserializeJSON()
    {
        // Arrange
        const string base64 = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAAXNSR0IArs4c6QAAAA1JREFUGFdj+P///38ACfsD/QVDRcoAAAAASUVORK5CYII=";
        // lang=json,strict
        const string json = $$"""
        {
          "code": "A0001",
          "base64_face_image": "{{base64}}"
        }
        """;

        // Act
        var sut = JsonSerializer.Deserialize(json, Context.Default.FaceImage);

        // Assert
        _ = sut.Should().NotBeNull();
        _ = sut!.Code.Should().Be("A0001");
        _ = sut.Content.Should().Equal(Convert.FromBase64String(base64));
    }
}
