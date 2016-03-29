# aspnet-core-docker-sample

This is a sample asp.net core Web Api project running inside a Docker container.

## Motivation

- I've been working with NodeJS projects along side with docker and they work pretty well
- Searching the internet, I haven't found any other sample that runs asp.net projects with `docker-compose` approach for development purposes.  

## How to run ? 

- Build the base image

```
docker build -t mersocarlin/aspnet .
```

- Install project dependencies

```
docker-compose run --rm --service-ports api dnu restore
```

- Run project

```
docker-compose up
```

- Access your browser at `http://[host_machine_ip]:5000/api/bank`
- Seed the data base `sh ./databases/seed.sh`
- Access your browser again at `http://[host_machine_ip]:5000/api/bank`