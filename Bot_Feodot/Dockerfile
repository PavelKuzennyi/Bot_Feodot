FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Bot_Feodot/Bot_Feodot.csproj", "Bot_Feodot/"]
RUN dotnet restore "Bot_Feodot/Bot_Feodot.csproj"
COPY . .
WORKDIR "/src/Bot_Feodot"
RUN dotnet build "Bot_Feodot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bot_Feodot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bot_Feodot.dll"]
