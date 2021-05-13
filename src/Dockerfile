#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Study.PaymentGateway.API/Study.PaymentGateway.API.csproj", "Study.PaymentGateway.API/"]
COPY ["Study.PaymentGateway.Domain.Services/Study.PaymentGateway.Domain.Services.csproj", "Study.PaymentGateway.Domain.Services/"]
COPY ["Study.PaymentGateway.Domain.Entity/Study.PaymentGateway.Domain.Entities.csproj", "Study.PaymentGateway.Domain.Entity/"]
COPY ["Study.PaymentGateway.Shared.Messages/Study.PaymentGateway.Shared.Messages.csproj", "Study.PaymentGateway.Shared.Messages/"]
COPY ["Study.PaymentGateway.Shared.Enums/Study.PaymentGateway.Shared.Enums.csproj", "Study.PaymentGateway.Shared.Enums/"]
COPY ["Study.PaymentGateway.Domain.Repository/Study.PaymentGateway.Domain.Repository.csproj", "Study.PaymentGateway.Domain.Repository/"]
COPY ["Study.PaymentGateway.Repository.MongoDB/Study.PaymentGateway.Repository.MongoDB.csproj", "Study.PaymentGateway.Repository.MongoDB/"]
COPY ["Study.PaymentGateway.Shared.DTO/Study.PaymentGateway.Shared.DTO.csproj", "Study.PaymentGateway.Shared.DTO/"]
COPY ["Study.PaymentGateway.App.Services/Study.PaymentGateway.App.Services.csproj", "Study.PaymentGateway.App.Services/"]
COPY ["Study.PaymentGateway.Gateways/Study.PaymentGateway.Gateways.csproj", "Study.PaymentGateway.Gateways/"]
COPY ["Study.PaymentGateway.Domain.AcquiringBanksGateway/Study.PaymentGateway.Domain.AcquiringBanksGateway.csproj", "Study.PaymentGateway.Domain.AcquiringBanksGateway/"]
COPY ["Study.PaymentGateway.App.Mapper/Study.PaymentGateway.App.Mapper.csproj", "Study.PaymentGateway.App.Mapper/"]
RUN dotnet restore "Study.PaymentGateway.API/Study.PaymentGateway.API.csproj"
COPY . .
WORKDIR "Study.PaymentGateway.API"
RUN dotnet build "Study.PaymentGateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Study.PaymentGateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Study.PaymentGateway.API.dll"]
