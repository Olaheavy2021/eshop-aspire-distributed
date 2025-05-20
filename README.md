# eshop-aspire-distributed
# EShop Aspire

**EShop Aspire*, an all-in-one solution for exploring a production-ready, AI-infused microservices e-commerce platform powered by .NET Aspire and GenAI. This repository contains everything you need to run, explore, and extend a distributed architecture that emphasizes resilience, scalability, and real-time intelligence.

## Overview

EShop Aspire demonstrates how to compose a modern e-shop out of loosely coupled services, each built on .NET Aspire for seamless orchestration. From product catalog management in PostgreSQL to basket storage in Redis, every component showcases best practices in cloud-native design. GenAI capabilities—powered by Microsoft.Extensions.AI and the Semantic Kernel—bring automated product recommendations and dynamic descriptions to shopping experience.

## Architecture

At the heart of this solution, each microservice registers itself with Aspire’s service discovery, health-checks are performed automatically, and resilient retries protect against transient failures. Synchronous calls flow over HTTP while asynchronous workflows leverage RabbitMQ with MassTransit for event-driven operations. A centralized Keycloak instance secures endpoints with OAuth2/OIDC, and the Blazor WebAssembly client interacts with services through Aspire proxies.

## Getting Started

Begin by cloning the repository to your local machine:
