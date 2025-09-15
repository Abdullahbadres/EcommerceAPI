#stage 1 = build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
#copy . . = copy semuanya
COPY . .
#RUN dotnet "./EcommerceAPI.csproj"
RUN dotnet restore "./EcommerceAPI.csproj"
RUN dotnet publish "./EcommerceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

#stage 2 = runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "EcommerceAPI.dll"]