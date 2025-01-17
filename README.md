# 📊 Cryptocurrency Exchange Rate Service

Um serviço de backend em **.NET** para recomendação de exchanges de criptomoedas, integrando dados da **API da Binance** e fornecendo taxas de câmbio em tempo real com **cache via Redis**.

---

## 🚀 Funcionalidades

- 🔄 Consulta de taxas de câmbio entre criptomoedas.
- ⚡ Integração com a **API da Binance**.
- 🗂️ Cache de dados com **Redis** para otimizar o desempenho.
- 🏗️ Arquitetura baseada em **DDD (Domain-Driven Design)** e **Clean Architecture**.

---

## 🏗️ Arquitetura do Projeto

```
RateService/
├── Application/         # Camada de aplicação (DTOs, interfaces, serviços)
├── Domain/             # Entidades, agregados e interfaces de domínio
├── Infrastructure/     # Implementações de infraestrutura (Redis, APIs externas)
├── API/               # Endpoints e controllers
├── Tests/             # Testes unitários e de integração
└── RateService.sln    # Solution do projeto
```

---

## 🛠️ Tecnologias Utilizadas

- **.NET 8**
- **C#**
- **Redis** (Cache)
- **Binance API** (Integração de taxas de câmbio)
- **Docker** (Containerização)
- **xUnit** e **Moq** (Testes)

---

## 🔧 Configuração do Ambiente

1. **Clone o repositório:**

```bash
git clone https://github.com/seu-usuario/cryptocurrency-exchange-rate-service.git
```

2. **Navegue até o diretório:**

```bash
cd cryptocurrency-exchange-rate-service
```

3. **Configure o Redis:**

```bash
docker run -d -p 6379:6379 --name redis redis
```

4. **Configure o appsettings.json:**

```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379",
    "SqlServer": "Server=localhost;Database=CryptoDB;Trusted_Connection=True;"
  },
  "BinanceApi": {
    "BaseUrl": "https://api.binance.com",
    "ApiKey": "SUA_API_KEY"
  }
}
```

5. **Rode o projeto:**

```bash
dotnet run --project API
```

---

## 🧪 Executando os Testes

Para rodar os testes unitários e de integração:

```bash
dotnet test
```

---

## 🔮 Próximos Passos

- 📈 Integração com outras exchanges.
- 📊 Implementação de gráficos em tempo real.
- 🛡️ Melhoria nas políticas de segurança.

---

## 🤝 Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir **Issues** ou enviar um **Pull Request**. 😊

---

## 📝 Licença

Este projeto está sob a licença **MIT**.

---

## 👨‍💻 Desenvolvido por

**Leonardo Pedroza de Faria**

[![LinkedIn](https://img.shields.io/badge/LinkedIn-blue?logo=linkedin)](https://www.linkedin.com/in/leonardopedroza)
[![GitHub](https://img.shields.io/badge/GitHub-black?logo=github)](https://github.com/seu-usuario)

---

⭐ Se você gostou do projeto, não esqueça de deixar uma **estrela** no repositório!

