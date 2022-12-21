#!/usr/bin/env bash 
git checkout -f origin/main
git pull https://github.com/niklyadov/csu-evico.git main
docker-compose build
docker-compose up