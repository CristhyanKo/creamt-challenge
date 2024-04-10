using CreaMT.Domain.Repositories;
using Moq;

namespace CleaMT.CommonTestUtilities.Repositories;
public class UnitOfWorkBuilder
{ 
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();
        return mock.Object;
    }
}
