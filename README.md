# ATMA

This project is implemented as part of test tasks for interview process with ATMA. 

# Quick start

The steps bellow describes how to start and test the project. For running the project and executing scripts was used **Ubuntu 18.04.3 LTS**, **Docker version 19.03.5**,  **docker-compose version 1.24.0**.

## Start API as docker service

There are 2 ways to start the container:
1. run `docker-compose up` . This command will build and start the API server exposing 5005 and 5010 ports mirrorin ports 80 and 443 correspondingly.  It is implemented for easier intergration with database in future. 
2. build image with the following command: ` docker build -t atma .` This command will build the image called `atma`. Now you can start the container as follows: `docker run -p 5010:443 -p 5005:80 atma atma_container`. This command will start identical conatainer as with docker-compose.

## Get OpenAPI specification

The specification is is available over this link: `localhost:5005/swagger/index.html`

## Quick tests

For the sake of simplification were created a bunch of scripts to immitate normal workflow and tests.
Load articles into the memory with:

    bash scripts/1-push-items.sh
This command wll create 5 records.
Count all items for all days:

    bash scripts/2-count-all-items.sh

Count items for a specific day:

    bash scripts/2-count-all-items.sh 2020-03-24

For all commands below the day specification also works.
Get revenue: 

    bash scripts/3-get-revenue.sh 2020-03-23

Get reveneu by article:

    bash scripts/4-get-revenue-by-article.sh
