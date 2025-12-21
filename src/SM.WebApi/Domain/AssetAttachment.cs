using SM.WebApi.Domain;

public class AssetAttachment
{
    public string Name { get; set; } = default!;
    public string ContentType { get; set; } = "application/octet-stream";
    public byte[] Bytes { get; set; } = Array.Empty<byte>();
    public long Size { get; set; }
    public DateTime CreatedUtc { get; set; }
}

