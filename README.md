# aspnet-core-docker-sample

This is a sample asp.net core Web Api project running inside a Docker container.

## Motivation

- I've been working with NodeJS projects along side with docker and they work pretty well
- Searching the internet, I haven't found any other sample that runs asp.net projects with `docker-compose` approach for development purposes.  

## How to run ? 

1.  Build your base image

```
docker build -t mersocarlin/aspnet .
```

2. Install the project dependencies

```
docker-compose run --rm --service-ports api dnu restore
```

3. Run the project

```
docker-compose up
```

4. Access your browser at `http://[host_machine_ip]:5000`