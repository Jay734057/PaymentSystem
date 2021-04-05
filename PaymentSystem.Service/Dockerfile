#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PaymentSystem.Service/PaymentSystem.Service.csproj", "PaymentSystem.Service/"]
COPY ["PaymentSystem.Business/PaymentSystem.Business.csproj", "PaymentSystem.Business/"]
COPY ["PaymentSystem.Core/PaymentSystem.Core.csproj", "PaymentSystem.Core/"]
COPY ["PaymentSystem.Repositories/PaymentSystem.Repositories.csproj", "PaymentSystem.Repositories/"]
RUN dotnet restore "PaymentSystem.Service/PaymentSystem.Service.csproj"
COPY . .
WORKDIR "/src/PaymentSystem.Service"
RUN dotnet build "PaymentSystem.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentSystem.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentSystem.Service.dll"]