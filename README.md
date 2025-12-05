# Capital Gains CLI

> CLI para cálculo de imposto sobre ganho de capital em operações de compra e venda de ações. Implementação do desafio técnico no padrão exigido pelo enunciado.

---

## ✅ Requisitos

* .NET 8 SDK ou superior
* Sistema operacional: Windows, Linux ou macOS

---

## ▶️ Como Executar

```bash
dotnet run --project CapitalGains.Cli
```

Cole a entrada no formato JSON pela entrada padrão (`stdin`):

```json
[{"operation":"buy","unit-cost":10,"quantity":100},{"operation":"sell","unit-cost":15,"quantity":50}]
```

Saída esperada (`stdout`):

```json
[{"tax":0.0},{"tax":0.0}]
```

O programa aceita múltiplas linhas de entrada consecutivas.

---

## 🧠 Regras de Negócio Implementadas

* Compras nunca geram imposto
* Imposto de 20% sobre o lucro
* Isenção para operações cujo valor total seja menor ou igual a R$20.000
* Prejuízos são acumulados para abatimento futuro
* Média ponderada recalculada a cada compra
* Prejuízos são descontados antes da tributação
* Quantidade de ações não pode ficar negativa

---

## 🏗️ Arquitetura do Projeto

```
CapitalGains
│
├── CapitalGains.Domain
│   ├── Entities
│   └── Services
│
├── CapitalGains.Cli
│   └── Program.cs
│
├── CapitalGains.UnitTests
│
└── CapitalGains.IntegrationTests
```

### Camadas

* **Domain**: Contém toda a lógica de negócio isolada
* **CLI**: Responsável apenas por entrada e saída (STDIN / STDOUT)
* **UnitTests**: Validação isolada das regras do domínio
* **IntegrationTests**: Execução real do CLI como caixa-preta

---

## 🧪 Testes

### Rodar todos os testes

```bash
dotnet test
```

### Tipos de testes

* ✅ Testes unitários (regras isoladas)
* ✅ Testes de integração (STDIN → STDOUT)
* ✅ Casos oficiais do enunciado

---

## 🧾 Padrões Técnicos

* Clean Architecture
* SOLID
* Separação total entre domínio e infraestrutura
* Sem uso de estado global
* Totalmente determinístico

---

## 📦 Tecnologias Utilizadas

* C#
* .NET 8
* xUnit
* FluentAssertions
* System.Text.Json

---

## 👨‍💻 Autor

Desenvolvido por Moisés Estevão como parte de processo seletivo para vaga de Engenheiro de Software.

---

## 🐳 Execução com Docker (Build Conteinerizada)

### Build da imagem

```bash
docker build -t capital-gains-cli .
```

### Execução interativa

```bash
docker run -it capital-gains-cli
```

### Execução via pipe (STDIN)

```bash
echo '[{"operation":"buy","unit-cost":10,"quantity":100},{"operation":"sell","unit-cost":15,"quantity":50}]' | docker run -i capital-gains-cli
```

---

## 🇬🇧 English Version

# Capital Gains CLI

CLI application to calculate capital gains tax from stock buy/sell operations. Technical challenge implementation following the official specifications.

## Requirements

* .NET 8 SDK or higher

## Run

```bash
dotnet run --project CapitalGains.Cli
```

Input (stdin):

```json
[{"operation":"buy","unit-cost":10,"quantity":100},{"operation":"sell","unit-cost":15,"quantity":50}]
```

Output (stdout):

```json
[{"tax":0.0},{"tax":0.0}]
```

## Business Rules

* Buy operations generate no tax
* 20% tax over profit
* Tax exemption for operations ≤ 20,000 currency
* Losses are accumulated
* Weighted average price is recalculated on buy
* Losses deductible before taxation
* Stock quantity cannot be negative

## Tests

```bash
dotnet test
```

Includes full unit and integration test coverage.

---

✅ Fully compliant with the official challenge specification.
