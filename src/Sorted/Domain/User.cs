using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<string>
{

}

public class UserBuilder
{
    private User _user;

    public UserBuilder()
    {
        _user = new User();
    }

    public UserBuilder Id(string id)
    {
        _user.Id = id;
        return this;
    }

    public UserBuilder Username(string username)
    {
        _user.UserName = username;
        return this;
    }

    public UserBuilder Email(string email)
    {
        _user.Email = email;
        return this;
    }

    public User Build()
    {
        return _user;
    }
}