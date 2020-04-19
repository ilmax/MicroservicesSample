FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services.OrderService/Services.OrderService.csproj", "src/Services.OrderService/"]
RUN dotnet restore "src/Services.OrderService/Services.OrderService.csproj"
COPY . .
WORKDIR "/src/src/Services.OrderService"
RUN dotnet build "Services.OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.OrderService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.OrderService.dll"]