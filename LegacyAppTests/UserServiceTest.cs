using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTest
{
    [Fact]
    public void VerifyNames_Should_Return_False_When_FirstName_And_LastName_Is_Empty()
    {
        string firstName = "";
        string lastName = "";
        UserService userService = new UserService();
        
        
        bool result = userService.VerifyNames(firstName, lastName);
        
        Assert.Equal(false,result);

    }
    [Fact]
    public void VerifyNames_Should_Return_False_When_FirstName_Is_Empty()
    {
        string firstName = "";
        string lastName = "Kowalski";
        UserService userService = new UserService();
        
        
        bool result = userService.VerifyNames(firstName, lastName);
        
        Assert.Equal(false,result);

    }
    [Fact]
    public void VerifyNames_Should_Return_False_When_LastName_Is_Empty()
    {
        string firstName = "Ania";
        string lastName = "";
        UserService userService = new UserService();
        
        
        bool result = userService.VerifyNames(firstName, lastName);
        
        Assert.Equal(false,result);

    }

    [Fact]
    public void VerifyEmail_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        string email = "cos";
        UserService service = new UserService();

        bool result = service.VerifyEmail(email);
        Assert.Equal(false,result);
    }

    [Fact]
    public void VerifyAge_Should_Return_False_When_Age_Is_Smaller_Than_21()
    {
        DateTime birth = new DateTime(2015, 5, 29);
        UserService service = new UserService();

        bool result = service.VerifyAge(birth);
        
        Assert.Equal(false,result);

    }

    [Fact]
    public void SetCredditLimit_When_ClinetType_Is_VeryImportantClient_CreditLimit_Should_Be_False()
    {
        Client client = new Client();
        client.Type = "VeryImportantClient";
        User user = new User();
        UserService service = new UserService();
        
        service.SetCredditLimit(client,user);
        bool result = user.HasCreditLimit;
        
        Assert.Equal(false,result);
    }

    [Fact]
    public void SetCredditLimit_When_ClinetType_Is_ImportantClient_CreditLimit_Should_Be_TwoTimesBigger()
    {
        Client client = new Client();
        client.Type = "ImportantClient";
        User user = new User();
        user.CreditLimit = 1000;
        user.LastName = "Kowalksi";
        user.DateOfBirth = new DateTime(2002, 5, 29);
        UserService service = new UserService();
        
        service.SetCredditLimit(client,user);
      bool result = (2000==user.CreditLimit);
      
        
        Assert.Equal(true,result);
        
    }
    
}