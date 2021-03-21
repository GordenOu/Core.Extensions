FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /root/Core.Extensions/
COPY ./ /root/Core.Extensions/
RUN dotnet run --project ./Automation/Build/Build.csproj
RUN dotnet run --project ./Automation/Test/Test.csproj
RUN dotnet run --project ./Automation/Publish/Publish.csproj
ENTRYPOINT []
