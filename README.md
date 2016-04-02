# aspnet-core-docker-sample

This is a sample asp.net core Web Api project running inside a Docker container.

## Motivation

- I've been working with NodeJS projects along side with docker and they work pretty well
- Searching the internet, I haven't found any other sample that runs asp.net projects with `docker-compose` approach for development purposes.  

## Stack of this repository

- [ASP.NET Core](http://docs.asp.net/en/latest/conceptual-overview/dotnetcore.html)
- [Mongo DB](https://www.mongodb.org/)
- [Redis](http://redis.io/)
- [Docker](https://www.docker.com/)

## How to run ?

- Build the base image

```bash
docker build -t mersocarlin/aspnet .
```

- Install project dependencies

```bash
docker-compose run --rm --service-ports api dnu restore
```

- Run project

```bash
docker-compose up
```

- Access your browser at `http://[host_machine_ip]:5000/api/accountHolders`
- Seed the data base `sh ./databases/seed.sh`
- Access your browser again at `http://[host_machine_ip]:5000/api/accountHolders`
