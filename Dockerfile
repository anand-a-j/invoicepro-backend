# Build Stage

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files first (leverages Docker layer caching)
COPY ["InvoicePro.API/InvoicePro.API.csproj", "InvoicePro.API/"]
COPY ["InvoicePro.Application/InvoicePro.Application.csproj", "InvoicePro.Application/"]
COPY ["InvoicePro.Domain/InvoicePro.Domain.csproj", "InvoicePro.Domain/"]
COPY ["InvoicePro.Infrastructure/InvoicePro.Infrastructure.csproj", "InvoicePro.Infrastructure/"]

RUN dotnet restore "InvoicePro.API/InvoicePro.API.csproj"

# Now copy everything else and build
COPY . .
WORKDIR "/src/InvoicePro.API"
RUN dotnet build "InvoicePro.API.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "InvoicePro.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "InvoicePro.API.dll"]