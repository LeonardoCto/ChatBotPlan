using ChatBotPlan.Application.DTOS;
using Microsoft.Extensions.AI;

public interface ILLMService
{
    IAsyncEnumerable<string> StreamAsync(List<ChatMessage> messages, CancellationToken ct);
}