# 🤖 ChatBotPlan

WhatsApp customer service plataform with AI 

Built with ASP.NET CORE following Clean architecture and SOLID principles.

This project is driven by my desire to level up my technical skills. I already have solid experience working with complex concepts and large-scale systems, but I've had limited hands-on exposure to key industry tools and frameworks — spanning AI, containerization, caching, message queues, cloud infrastructure, and more. I'm passionate about learning by building, and I genuinely enjoy thinking through scalability challenges, architectural decisions, and the tradeoffs that come with them. And there's no better way to grow than through your own projects..

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

## 🛠 Technologies

| Technology | Purpose |
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

## 📁 Structure

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

**🔄 Phase 2 — Messageria & Real-time**
- [ ] RabbitMQ
- [ ] [] Docker
- [ ] SignalR
- [ ] Webhook WhatsApp

**📋 Phase 3 — Multi-tenant**
- [ ] Data isolation by tenant
- [ ] Bot configuration
- [ ] Rate limiting

**🔭 Phase 4 — MCP**
- [ ] Model Context Protocol
- [ ] Each tenant integrates yours own sources(orders, schedules...)
