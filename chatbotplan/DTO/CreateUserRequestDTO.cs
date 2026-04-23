
namespace ChatBotPlan.Application.DTOS;
public class CreateUserRequestDTO
{
     public string Name {get; set;}
    public string Email {get; set;}
    public string PassWordHash {get; set;}
    public string Number {get; set;}
}