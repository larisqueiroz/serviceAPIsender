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

* Message format:
```
{
       "codigoPedido": 1001,
       "codigoCliente":1,
       "itens": [
           {
               "produto": "lápis",
               "quantidade": 100,
               "preco": 1.10
           },
           {
               "produto": "caderno",
               "quantidade": 10,
               "preco": 1.00
           }
       ]
   }
```


## Getting Started

1. Execute the command bellow:
```
docker-compose up --build
```
to run this project which acts as the producer. Also, clone and run the same command above for the project at https://github.com/larisqueiroz/serviceAPI to run the consumer.

2. Access http://localhost:3500 to see the API.

## Contact me
https://www.linkedin.com/in/larissalimaqueiroz/