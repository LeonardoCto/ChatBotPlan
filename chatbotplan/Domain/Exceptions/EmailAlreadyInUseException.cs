using ChatBotPlan.Domain.Exceptions;

namespace ChatBotPlan.Domain.Exceptions;

public class EmailAlreadyInUseException(string Email) : DomainException ($"Email '{Email}' já está em uso.");