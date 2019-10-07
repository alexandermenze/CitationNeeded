FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
EXPOSE 5000
ENV AppSettings__SendGridApiKey=NONE
ENV AppSettings__DbConnectionString=NONE
COPY ./app /etc/citationneeded/
WORKDIR /etc/citationneeded/
CMD ["dotnet", "CitationNeeded.WebApp.dll"]
