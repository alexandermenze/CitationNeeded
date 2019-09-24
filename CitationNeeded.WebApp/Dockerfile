FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CitationNeeded.WebApp/CitationNeeded.WebApp.csproj", "CitationNeeded.WebApp/"]
COPY ["CitationNeeded.Cryptography/CitationNeeded.Cryptography.csproj", "CitationNeeded.Cryptography/"]
COPY ["CitationNeeded.Domain/CitationNeeded.Domain.csproj", "CitationNeeded.Domain/"]
COPY ["CitationNeeded.Database/CitationNeeded.Database.csproj", "CitationNeeded.Database/"]
COPY ["CitationNeeded.Infrastructure/CitationNeeded.Infrastructure.csproj", "CitationNeeded.Infrastructure/"]
RUN dotnet restore "CitationNeeded.WebApp/CitationNeeded.WebApp.csproj"
COPY . .
WORKDIR "/src/CitationNeeded.WebApp"
RUN dotnet build "CitationNeeded.WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CitationNeeded.WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CitationNeeded.WebApp.dll"]