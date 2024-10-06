# serviceAPIsender

## Description
Along with the service at https://github.com/larisqueiroz/serviceAPI, this is an application that belongs to an microservice project and sends messages to the consumer.

## Architecture
![architecture.png](%2FScreenshots%2Farchitecture.png)

## Built With
* Docker Compose
* RabbitMQ

## Features
* Sends orders messages to the consumer at https://github.com/larisqueiroz/serviceAPI.

## Getting Started

1. Execute the command bellow:
```
docker-compose up --build
```
to run this project which acts as the producer. Also, clone and run the same command above for the project at https://github.com/larisqueiroz/serviceAPI to run the consumer.

2. Access http://localhost:3500 to see the API.

## Contact me
https://www.linkedin.com/in/larissalimaqueiroz/