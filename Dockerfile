FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV TZ=america/sao_paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApp/WebApp.csproj", "WebApp/"]
COPY ["nuget.config", "./"]
RUN dotnet restore "WebApp/WebApp.csproj" --configfile nuget.config 
#--verbosity diag
COPY . .
WORKDIR "/src/"
RUN dotnet build "WebApp/WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp/WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.dll"]