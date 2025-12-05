# ============================
# STAGE 1 - Build
# ============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy files
COPY . .

# Restore
RUN dotnet restore

# Build
RUN dotnet publish -c Release -o out

# ============================
# STAGE 2 - Runtime
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "CapitalGains.Cli.dll"]
