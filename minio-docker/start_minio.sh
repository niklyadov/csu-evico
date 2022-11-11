#!/usr/bin/env bash 

mkdir ./minio-data
docker run --name minio --rm -v minio-data:/data -e MINIO_ROOT_USER=minio -e MINIO_ROOT_PASSWORD=minioadmin -p 9000:9000 -p 9001:9001 -it quay.io/minio/minio:RELEASE.2022-11-08T05-27-07Z server /data --console-address ":9001"