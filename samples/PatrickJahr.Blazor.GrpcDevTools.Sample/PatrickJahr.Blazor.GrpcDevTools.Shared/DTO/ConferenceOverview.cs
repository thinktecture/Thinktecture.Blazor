using System.Runtime.Serialization;

namespace PatrickJahr.Blazor.GrpcDevTools.Shared.DTO;

[DataContract]
public class ConferenceOverview
{
    [DataMember(Order = 1)] 
    public Guid ID { get; set; }

    [DataMember(Order = 2)] 
    public string Title { get; set; } = string.Empty;
    [DataMember(Order = 3)] 
    public DateTime DateFrom { get; set; }
    [DataMember(Order = 4)] 
    public DateTime DateTo { get; set; }
    [DataMember(Order = 5)] 
    public string City { get; set; } = string.Empty;
}