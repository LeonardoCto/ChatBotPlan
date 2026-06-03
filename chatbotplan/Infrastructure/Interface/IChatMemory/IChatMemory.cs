using Microsoft.Extensions.AI;

namespace ChatBotPlan.Application.Interfaces;

public interface IChatMemory
{
    public Task<List<ChatMessage>> GetHistory(string chatId);
    public Task AddMessageAsync(string chatId, ChatMessage messages);
}