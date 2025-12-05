
# Capital Gains CLI

O projeto tem como objetivo o cálculo de imposto a ser pago sobre lucros ou prejuízos de operações no mercado financeiro de ações.

## Regra de negócio

O programa vai lidar com dois tipos de operações ( buy e sell ) e ele vai seguir as seguintes regras:
* O percentual de imposto pago é de 20% sobre o lucro obtido na operação. Ou seja, o imposto vai ser pago quando há uma operação de venda cujo preço é superior ao preço médio ponderado de compra.
* Para determinar se a operação resultou em lucro ou prejuízo, vai ser calculado o preço médio ponderado, então quando a operação for de compra, o preço médio ponderado será recalculado através da fórmula:
  
```bash
nova-media-ponderada = ((quantidade-de-acoes-atual * media-ponderadaatual) + (quantidade-de-acoes-compradas * valor-de-compra)) / (quantidade-de-acoes-atual +
quantidade-de-acoes-compradas)
```

Por exemplo, se você comprou 10 ações por R$ 20,00, vendeu 5, depois comprou outras 5 por R$ 10,00, a média ponderada é ((5 x 20.00) + (5 x 10.00)) / (5 + 5) = 15.00.

* O prejuízo é deduzido dos múltiplos lucros futuros, até que todo prejuízo seja deduzido.

* Prejuízos acontecem quando você vende ações a um valor menor do que o preço médio ponderado de compra. Neste caso, nenhum imposto vai ser pago e vai ser subtraído do prejuízo dos lucros seguintes, antes de calcular o imposto.

* Não será pago nenhum imposto e não será deduzido o lucro obtido dos prejuízos acumulados se o valor total da operação (custo unitário da ação x quantidade) for menor ou igual a R$ 20000,00. 

* Nenhum imposto é pago em operações de compra.

## Stacks utilizadas

* .NET 10
* xUnit
* FluentAssertions
* System.Text.Json
* Docker
* Docker-compose


## Documentação
O programa deve receber listas, uma por linha, de operações do mercado financeiro de ações em formato
JSON através da entrada padrão ( ```stdin```). Com os seguintes campos:

| Nome | Significado |
| --- | --- |
| operation | Se a operação é uma operação de compra ( buy ) ou venda ( sell )|
| unit-cost |  Preço unitário da ação em uma moeda com duas casas decimais |
| quantity | Quantidade de ações negociadas |

Exemplo de entrada:

```bash
[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":20.00, "quantity": 5000}]
[{"operation":"buy", "unit-cost":20.00, "quantity": 10000},{"operation":"sell", "unit-cost":10.00, "quantity": 5000}]
```

Para cada linha da entrada, o programa vai retornar uma lista contendo o imposto pago para cada
operação recebida. Os elementos desta lista vão estar codificados em formato JSON e a saída vai ser
retornada através da saída padrão ( ```stdout```). Com o seguinte campo:

| Nome | Significado |
| --- | --- |
| tax | O valor do imposto pago em uma operação|

Exemplo de saída:

```bash
[{"tax": 0.0}, {"tax": 10000.0}]
[{"tax": 0.0}, {"tax": 0.0}]
```

## Executando o projeto

Será apresentada duas formas de executar o projeto, via Docker ou via .NET SDK. Segue intruções abaixo:



### Docker

#### Instalando o Docker

Acesse a página oficial de instalação do Docker Desktop:
https://docs.docker.com/get-docker/

Escolha o instalador para o seu sistema operacional (Windows, Linux ou macOS).

Após instalar, confirme que o Docker está funcionando:

```bash
docker --version
```

Resultado esperado:
```bash
Docker version 'sua-versão'
```

Dentro da pasta raiz do projeto, rode o comando:

```bash
docker compose build
```

Em seguida:

```bash
docker compose run --rm capital-gains-cli
```

A aplicação ficará esperando uma ação, cole a seguinte operação:

```bash
[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":20.00, "quantity": 5000}]
```

Retorno esperado:

```bash
[{"tax": 0.0}, {"tax": 10000.0}]
```

A aplicação ficará esperando uma linha vazia para finalizar, portanto, pressione ```ENTER```. Ou envie outras operações para continuar.

Dentro do projeto existe um arquivo de texto, onde também pode ser executado, basta rodar o comando:

```bash
docker run --rm -i capital-gains-cli < Source/Input/input.txt
```


### .NET SDK
#### Instalando o .NET 10.0

Acesse a página oficial de downloads do .NET:

https://dotnet.microsoft.com/download

Baixe o instalador do .NET SDK 10, ou superior, correspondente ao seu sistema operacional (Windows, Linux ou macOS).

Após instalar, confirme a instalação executando no terminal:

```bash
dotnet --version
```

Resultado esperado:
```bash
10.0.100
```

Dentro da pasta raiz do projeto, abra a seguinte pasta:

```bash
cd Source
```

Em seguida:

```bash
dotnet restore
dotnet build
```

Em caso de sucesso, excute:

```bash
dotnet run
```

A aplicação ficará esperando uma ação, cole a seguinte operação:

```bash
[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},{"operation":"sell", "unit-cost":20.00, "quantity": 5000}]
```

Retorno esperado:

```bash
[{"tax": 0.0}, {"tax": 10000.0}]
```

A aplicação ficará esperando uma linha vazia para finalizar, portanto, pressione ```ENTER```. Ou envie outras operações para continuar.

Dentro do projeto existe um arquivo de texto, onde também pode ser executado, basta rodar o comando:

```bash
dotnet run < Input/input.txt
```
## Rodando os testes

O projeto é composto por testes de unidade e de integração, para garantir a funcionalidade desejada. Para executar os testes, também será apresentada das duas formas anteriores.

### Docker

Para executar via docker recomenda-se instalar o projeto em uma máquina virtual com .NET SDK instalado, para isso, precisa-se passar o caminho onde está o projeto, de acordo com o comando:

```bash
docker run -it --rm -v seu-caminho-aqui\CapitalGains:/src mcr.microsoft.com/dotnet/sdk:10.0 bash
```

Basta abrir a pasta:

```bash
cd src
```

A seguir executar mais dois comandos:

```bash
dotnet restore
dotnet test
```

### .NET SDK

Dentro da pasta raiz do projeto, abra a seguinte pasta:

```bash
cd Source
```

Em seguida executar os dois comandos:

```bash
dotnet restore
dotnet test
```
