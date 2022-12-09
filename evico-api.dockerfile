FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN dotnet dev-certs https
COPY ["Evico.Api/Evico.Api.csproj", "Evico.Api/"]
COPY ["Evico/Evico.csproj", "Evico/"]
RUN dotnet restore "Evico.Api/Evico.Api.csproj"
COPY . .
WORKDIR "/src/Evico.Api"
RUN dotnet build "Evico.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Evico.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
ENTRYPOINT ["dotnet", "Evico.Api.dll"]
