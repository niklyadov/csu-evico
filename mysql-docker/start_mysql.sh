#! /bin/bash

mkdir ./mysql-data
docker run --name basic-mysql --rm -v mysql-data:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=evico_development -p 3306:3306 -it mysql:8.0