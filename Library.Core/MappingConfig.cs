using AutoMapper;

namespace Library.Core
{
    public class MappingConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config => config.AddProfile(new MappingProfile()));
        }
    }
}
