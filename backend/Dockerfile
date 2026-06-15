FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY RpsslGameApi.Application/ RpsslGameApi.Application/
COPY RpsslGameApi.Contracts/ RpsslGameApi.Contracts/
COPY RpsslGameApi.Domain/ RpsslGameApi.Domain/
COPY RpsslGameApi.Infrastructure/ RpsslGameApi.Infrastructure/
COPY Tests/ Tests/
COPY RpsslGameApi.sln .

RUN dotnet restore RpsslGameApi.sln
RUN dotnet publish RpsslGameApi.Application/RpsslGameApi.Application.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "RpsslGameApi.Application.dll"]