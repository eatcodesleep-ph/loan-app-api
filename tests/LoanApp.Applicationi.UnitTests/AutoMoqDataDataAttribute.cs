using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace LoanApp.Application.UnitTests;

[AttributeUsage(AttributeTargets.Method)]
public class AutoMoqDataDataAttribute : AutoDataAttribute
{
    public AutoMoqDataDataAttribute() : base(() =>
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization());
        return fixture;
    })
    { }

    public class InlineAutoMoqDataDataAttribute(params object[] objects) : InlineAutoDataAttribute(new AutoMoqDataDataAttribute(), objects)
    {

    }
}
