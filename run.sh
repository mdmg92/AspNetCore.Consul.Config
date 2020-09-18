#! /bin/bash

pushd $( dirname $0 )
dotnet publish -c Release
docker-compose down
docker-compose -f docker-compose.yml up --build --force-recreate
popd