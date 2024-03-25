FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ScgcJob/ScgcJob.csproj", "ScgcJob/"]
RUN dotnet restore "ScgcJob/ScgcJob.csproj"
COPY . .
WORKDIR "/src/ScgcJob"
RUN dotnet build "ScgcJob.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScgcJob.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScgcJob.dll"]
