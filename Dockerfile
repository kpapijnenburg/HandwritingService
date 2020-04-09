#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./HandwritingService.Web/HandwritingService.Web.csproj", "HandwritingService.Web/"]
COPY ["./HandwritingService.DAL/HandwritingService.DAL.csproj", "HandwritingService.DAL/"]
COPY ["./HandwritingService.Domain/HandwritingService.Domain.csproj", "HandwritingService.Domain/"]
RUN dotnet restore "HandwritingService.Web/HandwritingService.Web.csproj"
COPY . .
WORKDIR "/src/HandwritingService.Web"
RUN dotnet build "HandwritingService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HandwritingService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HandwritingService.Web.dll"]