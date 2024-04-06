using AutoMapper;
using CreaMT.Application.Services.AutoMapper;

namespace CleaMT.CommonTestUtilities.Mapper;
public class MapperBuilder
{
    public  static IMapper Build()
    {
        return  new MapperConfiguration(option =>
        {
            option.AddProfile(new AutoMapping());
        }).CreateMapper();
    }
}
