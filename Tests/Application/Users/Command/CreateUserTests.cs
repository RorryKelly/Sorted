using Moq;
using Sorted.Application.Commands;

namespace Tests.Application.Commands;

public class CreateUserTests
{
    private Mock<IIdentityService> identityService;
    private CreateUserHandler createUserHandler;

    [SetUp]
    public void SetUp()
    {
        identityService = new Mock<IIdentityService>();
        createUserHandler = new CreateUserHandler(identityService.Object);
    }

    [Test]
    public async Task CreateUsersShouldCallIdentityService()
    {
        string username = "user";
        string password = "password";
        string email = "emailaddress";
        string expectedResult = "expectedResult";
        CreateUserCommand command = new CreateUserCommand(username, password, email);
        identityService.Setup(id => id.CreateUser(It.IsAny<User>(), password)).ReturnsAsync(Result<string>.Success(expectedResult));

        string result = await createUserHandler.Handle(command, CancellationToken.None);

        identityService.Verify(id => id.CreateUser(It.IsAny<User>(), password));
        Assert.That(expectedResult == result);
    }
}