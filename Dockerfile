FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /root/Core.Extensions/
COPY ./ /root/Core.Extensions/
RUN dotnet run --project ./Automation/Build/Build.csproj
RUN dotnet run --project ./Automation/Test/Test.csproj
RUN dotnet run --project ./Automation/Publish/Publish.csproj
ENTRYPOINT []
