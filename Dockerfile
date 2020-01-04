FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /root/Core.Extensions/
COPY ./ /root/Core.Extensions/
RUN dotnet build ./Source/Core.Extensions/Core.Extensions.csproj
RUN dotnet build ./Test/Core.Extensions.Test/Core.Extensions.Test.csproj
RUN dotnet run --project ./Test/Core.Extensions.Test/Core.Extensions.Test.csproj
ENTRYPOINT []
