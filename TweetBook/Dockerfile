FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

ARG BUILDCONFIG=REALESE
ARG VERSION=1.0.0

COPY TweetBook.csproj /build/

RUN dotnet restore ./build/TweetBook.csproj

COPY . ./build

WORKDIR /build/

RUN dotnet publish ./TweetBook.csproj -c $BUILDCONFIG -o out /p:Version=$VERSION

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

EXPOSE 80

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "TweetBook.dll"]
