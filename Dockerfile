FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "PaymentSystem.sln"
RUN dotnet build "PaymentSystem.sln" -c Release -o /app/build

FROM build AS publish
WORKDIR "/app/PaymentSystem.Service"
RUN dotnet publish "PaymentSystem.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentSystem.Service.dll"]

