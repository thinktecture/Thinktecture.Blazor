using AutoMapper;
using PatrickJahr.Blazor.GrpcDevTools.Shared.DTO;
using PatrickJahr.Blazor.GrpcDevTools.Shared.Services;
using PatrickJahr.Blazor.GrpcDevTools.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PatrickJahr.Blazor.GrpcDevTools.WebApi.Services;

public class ConferencesService : IConferencesService
{
    private readonly ConferencesDbContext _conferencesDbContext;
    private readonly IMapper _mapper;

    public ConferencesService(ConferencesDbContext conferencesDbContext, IMapper mapper)
    {
        _conferencesDbContext = conferencesDbContext;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ConferenceOverview>> ListConferencesAsync()
    {
        var conferences = await _conferencesDbContext.Conferences.OrderByDescending(c => c.DateCreated).ToListAsync();
        var confs = _mapper.Map<IEnumerable<ConferenceOverview>>(conferences);

        return confs;
    }

    public async Task<ConferenceDetailModel> AddNewConferenceAsync(ConferenceDetailModel conference)
    {
        var conf = _mapper.Map<Conference>(conference);
        conf.DateCreated = DateTime.UtcNow;

        _conferencesDbContext.Conferences.Add(conf);
        await _conferencesDbContext.SaveChangesAsync();

        return _mapper.Map<ConferenceDetailModel>(conf);

    }

    public async Task<ConferenceDetailModel?> GetConferenceDetailsAsync(ConferenceDetailsRequest request)
    {
        var conferenceDetails = await _conferencesDbContext.Conferences.FindAsync(request.ID);

        if (conferenceDetails == null)
        {
            return null;
        }

        return _mapper.Map<ConferenceDetailModel>(conferenceDetails);
    }

    public async Task UpdateConferenceAsync(ConferenceUpdateRequest request)
    {
        var conferenceDetails = await _conferencesDbContext.Conferences.FindAsync(request.ID);

        if (conferenceDetails != null)
        {
            conferenceDetails.Title = request.Conference?.Title ?? string.Empty;
            conferenceDetails.DateFrom = request.Conference?.DateFrom ?? DateTime.Now;
            conferenceDetails.DateTo = request.Conference?.DateTo ?? DateTime.Now;
            conferenceDetails.City = request.Conference?.City ?? string.Empty;
            conferenceDetails.Country = request.Conference?.Country ?? string.Empty;
            await _conferencesDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteConferenceAsync(ConferenceDetailsRequest request)
    {
        var conferenceDetails = await _conferencesDbContext.Conferences.FindAsync(request.ID);

        if (conferenceDetails != null)
        {
            _conferencesDbContext.Remove(conferenceDetails);
            await _conferencesDbContext.SaveChangesAsync();
        }
    }
}