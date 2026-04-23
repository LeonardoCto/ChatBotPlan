using ChatBotPlan.Domain.Exceptions;    

namespace ChatBotPlan.Domain.Exceptions;
public class UserNotFoundException(Guid id): DomainException($"Usuário com id '{id}'não encontrado");