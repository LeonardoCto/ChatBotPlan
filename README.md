# 🤖 ChatBotPlan

WhatsApp customer service plataform with AI 

Built with ASP.NET CORE following Clean architecture and SOLID principles.

---

## 🏛 Architecture

```
API → Application → Domain
         ↓
    Infrastructure
    (Redis, Ollama, JWT, RabbitMQ, EF Core)
```

**Message flow:**
```
WhatsApp (Webhook) → API → RabbitMQ → Ollama AI → SignalR → Frontend
```

---

## 🛠 Tecnologias

| Tecnologia | Finalidade |
|-----------|-----------|
| ASP.NET Core 9 | Core framework |
| JWT + Roles | Authentication and authorization |
| Ollama (LLM) | AI for customers responses |
| Redis | Cache |
| RabbitMQ | Message Queue |
| SignalR | Real time responses streaming |
| WebHook | WhatsApp message notifcations |
| FluentValidation | Input validation |
| Exception Middleware | Glocal exception handler |
| Azure Key Vault | Safe keys manager |
| Azure Communication Services | Emails service |
| Entity Framework Core + PostgreSQL | Data persistence |
| Docker + Docker Compose | Services conteinerization |
| Azure CLOUD | 

---

## 📁 Estrutura

```
ChatBotPlan/
├── API/                  → Controllers, Middlewares
├── Application/          → Use Cases, DTOs, Interfaces
├── Domain/               → Entidades, Exceções de domínio
└── Infrastructure/       → Redis, Ollama, JWT, RabbitMQ, EF Core
```

---

## 🗺 Roadmap

**✅ Phase 1 — Base**
- [x] Clean Architecture + SOLID
- [x] JWT + Roles
- [x] Redis
- [x] Ollama AI
- [x] FluentValidation + Exception Middleware
- [x] Azure Key Vault + Email Service

**🔄 Phase 2 — Messagerua & Real-time**
- [ ] RabbitMQ
- [ ] [] Docker
- [ ] SignalR
- [ ] Webhook WhatsApp

**📋 Phase 3 — Multi-tenant**
- [ ] Data isolation by tenant
- [ ] Bot configuration
- [ ] Rate limiting

**🔭 Fase 4 — MCP**
- [ ] Model Context Protocol
- [ ] Each tenant integrates yours own sources(orders, schedules...)
