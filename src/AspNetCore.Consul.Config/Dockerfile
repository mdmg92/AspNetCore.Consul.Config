FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY bin/Release/netcoreapp3.1/publish /app
WORKDIR /app

EXPOSE 8080/tcp
ENV ASPNETCORE_URLS http://*:5000

ENTRYPOINT ["dotnet", "AspNetCore.Consul.Config.dll"]