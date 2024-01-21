using System;
using System.Runtime.Intrinsics.X86;

namespace Example.models;
public interface IUser
{
    long TCNo { get; }
    string UserName { get; }
    string Password { get; }
    string FirstName { get; }
    string LastName { get; }
    bool IsActive { get; }
}





public abstract class User : IUser
{
    public long TCNo { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public User() { }
    public User(string userName, string password, bool isActive)
    {
        UserName = userName;
        Password = password;
        IsActive = isActive;
    }

}






public enum UserTypeEnum
{
    Personal,
    Student,
    Jobber
}
public static class UserFactory
{
    public static IUser GetInstance(UserTypeEnum userType)
    {
        if (userType == UserTypeEnum.Personal)
            return new Personal();
        if (userType == UserTypeEnum.Student)
            return new Student();
        return new Jobber();
    }
}