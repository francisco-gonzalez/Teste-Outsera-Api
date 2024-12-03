# Guia para Configuração e Execução do Projeto .NET Core

## Passos para Configurar e Executar

### 1. Clonar o Repositório

1. Certifique-se de que o Git está instalado. Verifique com:
   ```bash
   git --version
   ```   
2. Abra o terminal ou prompt de comando na pasta onde deseja clonar o projeto.
3. Baixe o projeto do Git
   ```bash
   git clone https://github.com/francisco-gonzalez/Teste-Outsera-Api.git
   ```
### 2. Certifique-se de que o .NET Core 8.0 SDK está instalado
  ```bash
  dotnet --version
  ```
### 3. Dentro da pasta do projeto, restaure os pacotes NuGet:
  ```bash
   dotnet restore
  ```
### 4. Configure o arquivo appsettings.json com o arquivo que deverá servir como referência csv e o seu delimitador
  ```bash
  "ImportSettings": {
    "Delimiter": ";",
    "CsvFilePath": "movielist.csv"
   },
  ```
>#### A aplicação foi criada com segurança de Chave de API que se encontar em appsettings.json que deverá ser informada no HEADER de todas as requisições:
>#### x-api-key: ApiKey
### 5. Execute a Aplicação com:
  ```bash
  dotnet run
  ```
### 6. Para executar os testes de integração utilize o comando: 
  ```bash
  dotnet test
  ```
