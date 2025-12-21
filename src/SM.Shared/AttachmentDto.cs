public class AttachmentDto
{
  public string Name { get; set; } = default!;
  public string ContentType { get; set; } = "application/octet-stream";
  public long Size { get; set; }
  public byte[] Bytes { get; set; } = Array.Empty<byte>();
  public string? PreviewDataUrl { get; set; }
  public string? Error { get; set; }
}
