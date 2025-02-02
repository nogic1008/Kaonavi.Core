using Kaonavi.Net.Entities;
using RandomFixtureKit;

namespace Kaonavi.Net.Tests;

/// <summary><see cref="ISheetDataExtensions"/>の単体テスト</summary>
[TestClass, TestCategory("Entities")]
public sealed class ISheetDataExtensionsTest
{
    public class TestSheetData : ISheetData
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
        public IReadOnlyList<CustomFieldValue> ToCustomFields() => [new(101, Name)];
    }

    /// <summary>
    /// <see cref="ISheetDataExtensions.ToSingleSheetData{T}(T)"/>は、単一レコードであるSheetDataの一覧を返す。
    /// </summary>
    [TestMethod($"{nameof(ISheetDataExtensions)}.{nameof(ISheetDataExtensions.ToSingleSheetData)}() > 単一レコードであるSheetDataの一覧を返す。")]
    public void ToSingleSheetData_Returns_Single_SheetData()
    {
        // Arrange
        const int length = 10;
        var values = FixtureFactory.CreateMany<TestSheetData>(length);

        // Act
        var actual = values.ToSingleSheetData();

        // Assert
        actual.ShouldSatisfyAllConditions(
            static sut => sut.Count.ShouldBe(length),
            static sut => sut.ShouldAllBe(d => d.Records.Count == 1)
        );
    }

    /// <summary>
    /// <see cref="ISheetDataExtensions.ToMultipleSheetData{T}(IEnumerable{T})"/>は、複数レコードであるSheetDataの一覧を返す。
    /// </summary>
    [TestMethod($"{nameof(ISheetDataExtensions)}.{nameof(ISheetDataExtensions.ToMultipleSheetData)}() > 複数レコードであるSheetDataの一覧を返す。")]
    public void ToMultipleSheetData_Returns_Multiple_SheetData()
    {
        // Arrange
        const int codeLength = 10;
        string[] codes = FixtureFactory.CreateMany<string>(codeLength);
        const int nameLength = 3;
        string[] names = FixtureFactory.CreateMany<string>(nameLength);
        var values = codes.SelectMany(code => names.Select(name => new TestSheetData { Code = code, Name = name }));

        // Act
        var actual = values.ToMultipleSheetData();

        // Assert
        actual.ShouldSatisfyAllConditions(
            static sut => sut.Count.ShouldBe(codeLength),
            static sut => sut.ShouldAllBe(d => d.Records.Count == nameLength)
        );
    }
}
