﻿FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
EXPOSE 80
EXPOSE 443
COPY ./app /etc/citationneeded/
ENTRYPOINT ["dotnet", "CitationNeeded.WebApp.dll"]