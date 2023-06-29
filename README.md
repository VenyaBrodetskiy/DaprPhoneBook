# Dapr Microservices Communication Demo with RabbitMQ

## Project Description:
This repository demonstrates the communication between microservices using Dapr and RabbitMQ. It consists of two microservices: a "Manager" service and an "Accessor" service. The application simulates a simple phone book system, storing and retrieving phone entries, using MongoDB as the data store. 

The Manager service is responsible for handling requests and, after validating them, passing them to the Accessor service via a RabbitMQ queue. The Accessor service then processes these requests and manipulates the data in MongoDB accordingly.

## Technologies Used:
- .NET Core: The framework used to develop the microservices.
- Dapr: An event-driven, portable runtime for building microservices.
- Docker: A platform used to containerize and manage the microservices.
- RabbitMQ: A message broker for the communication between the microservices.
- MongoDB: The database used to store and retrieve phone entries.

## Getting Started:
1. Clone the repository.
2. Navigate to the project directory.
3. Run the command `docker-compose up` to start the services and their Dapr sidecars.

## Architecture:
- Manager Service: Accepts requests (Get, Post, Delete) related to phone entries, validates them, and pushes to the RabbitMQ queue via Dapr.
- Accessor Service: Picks up requests from the RabbitMQ queue, processes the requests, and performs actions on MongoDB.
- RabbitMQ: Used as the messaging system for the exchange of requests between the Manager and Accessor services.
- Dapr: Handles the communication between the Manager and Accessor services and the RabbitMQ queue.
- MongoDB: The database used to store and retrieve phone entries.

## Usage:
The Manager service exposes the following endpoints:
- `GET /phonebooks`: Returns all the phone entries.
- `GET /phonebook?phone={phone}`: Returns the phone entry associated with the given phone number.
- `DELETE /phonebook?phone={phone}`: Deletes the phone entry associated with the given phone number.
- `POST /phonebook`: Adds a new phone entry. The phone entry data should be included in the request body in JSON format.
