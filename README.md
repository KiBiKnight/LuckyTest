# LuckyClean - Clean Architecture Order Processing API

A refactored order processing system built with .NET 8.0 following Clean Architecture principles.

## Project Structure

```
LuckyClean/
├── LuckyClean.Domain          # Entities, enums, repository interfaces
├── LuckyClean.Application     # Business logic, DTOs, validation, service interfaces
├── LuckyClean.Infrastructure  # EF Core, external service implementations
└── LuckyClean.API             # Controllers, middleware, configuration
```

Dependencies flow inward: **API → Infrastructure → Application → Domain**

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

## Getting Started

1. **Clone the repository**
   ```bash
   git clone <repo-url>
   cd LuckyClean
   ```

2. **Set up user secrets** (for sensitive configuration)
   ```bash
   dotnet user-secrets init --project LuckyClean.API
   dotnet user-secrets set "EmailSettings:SenderPassword" "your-password" --project LuckyClean.API
   ```

3. **Apply database migrations**
   ```bash
   dotnet ef database update --project LuckyClean.Infrastructure --startup-project LuckyClean.API
   ```
   This creates a local SQLite database (`luckytest.db`) with seeded product data.

4. **Run the application**
   ```bash
   dotnet run --project LuckyClean.API
   ```

5. **Open Swagger UI** to test endpoints at `https://localhost:{port}/swagger`

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/orders` | Process a new order |
| GET | `/api/orders/{customerId}/history` | Get order history for a customer |
| POST | `/api/products` | Add a new product |
| GET | `/api/products` | Get all products |

### Sample Order Request

```json
{
  "customerId": 1,
  "customerEmail": "customer@example.com",
  "items": ["Widget", "Gadget"],
  "paymentMethod": "CreditCard",
  "discount": 0.1
}
```

## Configuration

### appsettings.json

| Setting | Description |
|---------|-------------|
| `ConnectionStrings:Default` | SQLite connection string |
| `PaymentSettings:CreditCardUrl` | Credit card payment API endpoint |
| `PaymentSettings:PayPalUrl` | PayPal payment API endpoint |
| `EmailSettings:SmtpHost` | SMTP server host |
| `EmailSettings:SmtpPort` | SMTP server port |
| `EmailSettings:SenderEmail` | Sender email address |
| `EmailSettings:EnableSsl` | Enable SSL for SMTP |
| `UseMockPayment` | `true` to use mock payment service |
| `UseMockEmail` | `true` to use mock email service |

### Mock Services

Both the payment and email services have mock implementations for local development. Set `UseMockPayment` and `UseMockEmail` to `true` in `appsettings.json` to use them. The mock payment service returns success for all valid payment methods, and the mock email service logs to the console.

## Key Design Decisions

- **Clean Architecture** — strict separation of concerns with inward-only dependency flow
- **SQLite** — lightweight local database, easily swappable via EF Core
- **FluentValidation** — input validation on incoming requests before business logic executes
- **Enum-based payment methods and statuses** — replaces magic strings from the original code, with EF Core string conversion for readable database values
- **Mock service pattern** — configurable toggle between real and mock implementations for payment and email services
- **Global exception handling** — returns clean `ProblemDetails` responses, with detailed error messages only in development