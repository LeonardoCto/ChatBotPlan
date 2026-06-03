using System.Runtime.CompilerServices;
using ChatBotPlan.Application.DTOS;
using Microsoft.Extensions.AI;

namespace ChatBotPlan.Infrastructure;

public class OllamaAdapter(IChatClient chatClient) : ILLMService
{
    public async IAsyncEnumerable<string> StreamAsync(List<ChatMessage> messages, [EnumeratorCancellation] CancellationToken ct)
    {
        await foreach (var update in chatClient.GetStreamingResponseAsync(messages, cancellationToken: ct))
        {
            if (update.Text is not null)
                yield return update.Text;
        }
    }
}