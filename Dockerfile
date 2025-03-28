FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["PersonApi.csproj", "./"]
RUN dotnet restore "PersonApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "PersonApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonApi.dll"]

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production