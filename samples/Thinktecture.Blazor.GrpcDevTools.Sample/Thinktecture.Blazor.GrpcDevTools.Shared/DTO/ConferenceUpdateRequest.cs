using System.Runtime.Serialization;

namespace Thinktecture.Blazor.GrpcDevTools.Shared.DTO
{
    [DataContract]
    public class ConferenceUpdateRequest
    {
        [DataMember(Order = 1)]
        public Guid ID { get; set; }

        [DataMember(Order = 2)]
        public ConferenceDetailModel Conference { get; set; }
    }
}
