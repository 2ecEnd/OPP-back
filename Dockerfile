FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["OPP-back.csproj", "."]
RUN dotnet restore "OPP-back.csproj"

COPY . .
RUN dotnet publish "OPP-back.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV DATABASE_URL=postgresql://sky:8LFJiua8RMWd87s9yJAsu4EfXgFmeVwr@dpg-d4vt7i56ubrc73a3th0g-a/opp

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "OPP-back.dll"]