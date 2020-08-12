---
layout: default
title: Docker
parent: Getting Started
nav_order: 2
---

## Docker

Docker is a tool that makes it possible to run multiple copies of applications. If you want to use automated tests to test a website, you can use Docker to run those tests in multiple browsers at the same, which will speed things up.

If you plan to test a website, follow the instructions below to install Docker. If you're planning to run the examples in the [Possum Labs DSL project](https://github.com/Possum-Labs/DSL), you will also need to install Docker.

### Docker for Windows

1. [Download](https://www.docker.com/products/docker-desktop) and install Docker Desktop and start Docker
1. Open a terminal and change to the `development-setup` directory in the DSL project. Then run the following command: `docker-compose up --scale node-chrome=3 -d`
1. Visit `localhost:4444/grid/console` in your browser. You should see three web drivers.

### Docker for Mac

1. [Download](https://www.docker.com/products/docker-desktop) and install Docker Desktop and start Docker
1. Open a terminal and change to the `development-setup` directory in the DSL project. Then run the following command: `docker-compose up --scale node-chrome=3 -d`
1. Visit `localhost:4444/grid/console` in your browser. You should see three web drivers.

<feedback>
