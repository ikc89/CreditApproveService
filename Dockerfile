FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["KocFinansCC.Api/KocFinansCC.Api.csproj", "KocFinansCC.Api/"]
RUN dotnet restore "KocFinansCC.Api/KocFinansCC.Api.csproj"
COPY . .
WORKDIR "/src/KocFinansCC.Api"
RUN dotnet build "KocFinansCC.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "KocFinansCC.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KocFinansCC.Api.dll"]