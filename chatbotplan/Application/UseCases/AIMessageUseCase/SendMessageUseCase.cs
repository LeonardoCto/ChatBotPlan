using System.Runtime.CompilerServices;
using System.Text;
using ChatBotPlan.Application.DTOS;
using ChatBotPlan.Application.Interfaces;
using ChatBotPlan.Domain.Entities;
using ChatBotPlan.Domain.Exceptions;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace ChatBotPlan.Application;

public class SendMessageUseCase(ILLMService lLMService, IChatMemory chatMemory, IUserContext userContext)
{

    public async IAsyncEnumerable<string> ExecuteAsync(UserMessageDTO chatMessage, [EnumeratorCancellation] CancellationToken ct)
    {
        var systemPrompt = "Você é um chatbot de atendimento ao cliente via whatssApp em uma loja de cadeiras para barbeiros, seu nome é Louís BOT";
        var chatId = userContext.userId;
        if (string.IsNullOrWhiteSpace(chatId))
            throw new DomainException("Key to find chat history not found");


        List<ChatMessage> messages = new()
        {
            new ChatMessage(ChatRole.System, systemPrompt)
        };
        var history = await chatMemory.GetHistory(chatId);
        messages.AddRange(history);

        var userMessage = new ChatMessage(ChatRole.User, chatMessage.Message);
        messages.Add(userMessage);

        var assistanceResponse = new StringBuilder();

        await foreach (var token in lLMService.StreamAsync(messages, ct))
        {
            assistanceResponse.Append(token);
            yield return token;
        }

        await chatMemory.AddMessageAsync(chatId, userMessage);
        await chatMemory.AddMessageAsync(chatId, new ChatMessage(ChatRole.Assistant, assistanceResponse.ToString()));

    }
}