#!/usr/bin/env bash 
git checkout -f origin/master
git pull
docker-compose build
docker-compose up