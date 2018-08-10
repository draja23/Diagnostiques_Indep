FROM microsoft/dotnet:2.1.2-aspnetcore-runtime-stretch-slim
WORKDIR /app
COPY bin/Release/netcoreapp2.1/publish /app/
ENTRYPOINT ["dotnet", "Diagnostique.BO.dll"]