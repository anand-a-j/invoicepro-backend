# Build Stage

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files first (leverages Docker layer caching)
COPY ["src/InvoicePro.API/InvoicePro.API.csproj", "src/InvoicePro.API/"]
COPY ["src/InvoicePro.Application/InvoicePro.Application.csproj", "src/InvoicePro.Application/"]
COPY ["src/InvoicePro.Domain/InvoicePro.Domain.csproj", "src/InvoicePro.Domain/"]
COPY ["src/InvoicePro.Infrastructure/InvoicePro.Infrastructure.csproj", "src/InvoicePro.Infrastructure/"]

RUN dotnet restore "src/InvoicePro.API/InvoicePro.API.csproj"

# Now copy everything else and build
COPY . .
WORKDIR "/src/src/InvoicePro.API"
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