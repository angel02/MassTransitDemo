#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MassTransitDemo.Consumer/MassTransitDemo.Consumer.csproj", "MassTransitDemo.Consumer/"]
RUN dotnet restore "MassTransitDemo.Consumer/MassTransitDemo.Consumer.csproj"
COPY . .
WORKDIR "/src/MassTransitDemo.Consumer"
RUN dotnet build "MassTransitDemo.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransitDemo.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransitDemo.Consumer.dll"]