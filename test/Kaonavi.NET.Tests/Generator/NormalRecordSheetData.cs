namespace Kaonavi.Net.Tests.Generator;

[SheetSerializable]
internal partial record NormalRecordSheetData : ISheetData
{
    public string Code { get; init; } = default!;
    [CustomField(101)] public string? Name { get; init; }
    [CustomField(102)] public DateTime Date1 { get; init; }
    [CustomField(103)] public DateTimeOffset Date2 { get; init; }
    [CustomField(104)] public DateOnly Date3 { get; init; }
}
