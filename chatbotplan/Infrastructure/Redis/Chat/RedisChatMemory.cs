using System.Text.Json;
using ChatBotPlan.Application.Interfaces;
using Microsoft.Extensions.AI;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace ChatBotPlan.Infrastructure;

public class RedisChatMemory : IChatMemory
{
    private readonly IDatabase _db;

    public RedisChatMemory(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task AddMessageAsync(string chatId, ChatMessage messages)
    {
        var json = JsonSerializer.Serialize(messages);

        await _db.ListRightPushAsync(
                    $"chatId:{chatId}",
                    json);

        await _db.KeyExpireAsync($"chatId:{chatId}", TimeSpan.FromDays(3));
    }

    public async Task<List<ChatMessage>> GetHistory(string chatId)
    {
        var values = await _db.ListRangeAsync($"chatId:{chatId}");

        return values
        .Select(v => JsonSerializer.Deserialize<ChatMessage>(v.ToString())!)
        .ToList();
    }
}