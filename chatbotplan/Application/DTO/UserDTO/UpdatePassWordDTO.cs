namespace ChatBotPlan.Application.DTOS;

public class UpdatePassWordDTO
{
    public string? Email { get; set; }
    public string? PassWord { get; set; }
    public string? NewPassWord { get; set; }
    public string? Code { get; set; }
}