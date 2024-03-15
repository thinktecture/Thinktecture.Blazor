using System.Runtime.Serialization;

namespace PatrickJahr.Blazor.GrpcDevTools.Shared.DTO;

[DataContract]
public class TimeResult
{
    [DataMember(Order = 1)]
    public string Time { get; set; } = string.Empty;
}