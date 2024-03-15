using AutoMapper;
using Thinktecture.Blazor.GrpcDevTools.Shared.DTO;

namespace Thinktecture.Blazor.GrpcDevTools.WebApi.Mappers;

public class ConferencesMapper : Profile
{
    public ConferencesMapper()
    {
        CreateMap<Models.Conference, ConferenceOverview>();
        CreateMap<ConferenceOverview, Models.Conference>();
        CreateMap<Models.Conference, ConferenceDetailModel>();
        CreateMap<ConferenceDetailModel, Models.Conference>();
    }
}