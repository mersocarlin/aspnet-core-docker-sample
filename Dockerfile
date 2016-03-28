FROM microsoft/aspnet

RUN mkdir -p /app
COPY ./BankService/BankService.Api/project.json /app
WORKDIR /app

RUN dnu restore

EXPOSE 5000