# Etapa 1: Usando a imagem oficial do SDK para desenvolvimento
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copiar os arquivos do projeto
COPY *.csproj ./
RUN dotnet restore

COPY . ./

# Expondo a porta 8080
EXPOSE 8080

# Executa a aplicação diretamente com dotnet run
CMD ["dotnet", "run", "--urls", "http://0.0.0.0:8080"]
