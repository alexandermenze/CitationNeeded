FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
RUN find | sed 's|[^/]*/|- |g'
RUN dotnet restore "CitationNeeded.WebApp.csproj"
RUN dotnet build "CitationNeeded.WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CitationNeeded.WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CitationNeeded.WebApp.dll"]