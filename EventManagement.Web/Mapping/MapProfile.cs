using AutoMapper;
using EventManagement.Domain.Entities;
using EventManagement.Web.Models;

namespace EventManagement.Web.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Event, EventModel>();
            CreateMap<EventModel, Event>();
        }
    }
}
