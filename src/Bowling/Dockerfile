FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/Bowling
RUN dotnet restore
WORKDIR /src/Bowling
RUN dotnet test /src/Bowling/Bowling.Test/Bowling.Test.csproj
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT [ "dotnet", "Bowling.Api.dll" ]