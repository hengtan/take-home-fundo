FROM mcr.microsoft.com/dotnet/sdk:9.0 AS test
WORKDIR /app

# Copia tudo, inclusive o appsettings.test.json
COPY . .

# (Opcional) Restaura dependências antes para cache ser reaproveitado
RUN dotnet restore "./backend/src/Fundo.Services.Tests/Fundo.Services.Tests.csproj"

# Define o diretório de trabalho para os testes
WORKDIR /app/backend/src/Fundo.Services.Tests

# Executa os testes com logger detalhado
RUN dotnet test Fundo.Services.Tests.csproj \
    --logger "console;verbosity=detailed" \
    --no-build \
    --results-directory /app/TestResults