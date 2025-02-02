using System.Globalization;

namespace Kaonavi.Net.Json;

/// <summary>
/// <c>""</c>を<c>null</c>とみなす<see cref="Nullable{DateOnly}"/>用のJsonConverter実装。
/// </summary>
public class BlankNullableDateConverter : JsonConverter<DateOnly?>
{
    /// <inheritdoc/>
    public override bool HandleNull => true;

    /// <inheritdoc/>
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        return string.IsNullOrEmpty(value) ? null : DateOnly.Parse(value);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            Span<byte> buffer = stackalloc byte[12]; // "yyyy-MM-dd".Length
            buffer[0] = (byte)'"';
            _ = value.GetValueOrDefault().TryFormat(buffer[1..], out int written, "o", CultureInfo.InvariantCulture);
            buffer[written + 1] = (byte)'"';
            writer.WriteRawValue(buffer[0..(written + 2)], false);
        }
        else
        {
            writer.WriteStringValue(""u8);
        }
    }
}
