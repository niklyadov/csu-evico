#!/usr/bin/env bash 
git checkout -f origin/main
git pull
docker-compose build
docker-compose up