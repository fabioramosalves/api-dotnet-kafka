FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV TZ=america/sao_paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["api-dotnet-kafka/api-dotnet-kafka.csproj", "api-dotnet-kafka/"]
COPY ["nuget.config", "./"]
RUN dotnet restore "api-dotnet-kafka/api-dotnet-kafka.csproj" --configfile nuget.config 
#--verbosity diag
COPY . .
WORKDIR "/src/"
RUN dotnet build "api-dotnet-kafka/api-dotnet-kafka.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api-dotnet-kafka/api-dotnet-kafka.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api-dotnet-kafka.dll"]