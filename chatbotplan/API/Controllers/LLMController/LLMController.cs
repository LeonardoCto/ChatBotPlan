using System.Text;
using ChatBotPlan.Application;
using ChatBotPlan.Application.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotPlan.API.Controllers;

[ApiController]
[Route("api/LLMChat")]
public class LLMControler(SendMessageUseCase messageUseCase) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> sendMessage(UserMessageDTO message, CancellationToken ct)
    {
        var response = new StringBuilder();
        await foreach (var token in messageUseCase.ExecuteAsync(message, ct))
        {
            response.Append(token);
        }
        return Ok(new { response = response.ToString() });
    }
}