using Application;

using AutoMapper;

using Xunit;

namespace Sample.UnitTests
{
    public class AutoMapperTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(c => c.AddProfile<MappingProfiles>());
            config.AssertConfigurationIsValid();
        }
    }
}