FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["CaseJuri/CaseJuri.API/CaseJuri.API.csproj", "CaseJuri.API/"]
COPY ["CaseJuri/CaseJuri.Application/CaseJuri.Application.csproj", "CaseJuri.Application/"]
COPY ["CaseJuri/CaseJuri.Domain/CaseJuri.Domain.csproj", "CaseJuri.Domain/"]
COPY ["CaseJuri/CaseJuri.Infrastructure/CaseJuri.Infrastructure.csproj", "CaseJuri.Infrastructure/"]

RUN dotnet restore "CaseJuri.API/CaseJuri.API.csproj"

COPY CaseJuri/ ./

WORKDIR "/src/CaseJuri.API"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

ENV DOTNET_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "CaseJuri.API.dll"]
