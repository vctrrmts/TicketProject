using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;

namespace Core.Tests.Helpers;

public static class FixtureHelper
{
    public static readonly Fixture DefaultFixture = CreateFixture();
    
    public static Fixture CreateFixture()
    {
        var fixture = new Fixture();
        fixture.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString().Substring(0, 10)));
        fixture.Customize(new AutoMoqCustomization
        {
            ConfigureMembers = true,
            GenerateDelegates = false
        });
        fixture.Customizations.Add(new StableFiniteSequenceRelay());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        return fixture;
    }
}