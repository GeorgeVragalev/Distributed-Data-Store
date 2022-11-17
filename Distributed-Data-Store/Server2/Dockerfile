FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server2/Server2.csproj", "Server2/"]
RUN dotnet restore "Server2/Server2.csproj"
COPY . .
WORKDIR "/src/Server2"
RUN dotnet build "Server2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server2.dll"]
