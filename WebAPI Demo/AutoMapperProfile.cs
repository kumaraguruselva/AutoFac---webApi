using AutoMapper;
using WebAPI_Demo.Models;

namespace WebAPI_Demo
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {
            CreateMap<car,carModel>().ReverseMap();
        }
    }
}
