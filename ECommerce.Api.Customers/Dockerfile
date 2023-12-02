# Ensure the Service Fabric cluster you deploy to supports the container image you are using. See https://aka.ms/containerimagehelp for information on Windows container version compatibility.
FROM mcr.microsoft.com/dotnet/aspnet:3.1 
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "ECommerce.Api.Customers.dll"]
