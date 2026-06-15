# RPSSL — Rock Paper Scissors Spock Lizard

A full-stack implementation of the classic **Rock Paper Scissors Spock Lizard** game.

The project consists of:

* **Frontend:** React + Vite + Material UI
* **Backend:** ASP.NET Core 10 Web API
* **Docker & Docker Compose** for containerized execution

---

## Architecture

```text
.
├── frontend/
├── backend/
│   ├── RpsslGameApi.Application/
│   ├── RpsslGameApi.Contracts/
│   ├── RpsslGameApi.Domain/
│   └── RpsslGameApi.Infrastructure/
└── docker-compose.yml
```

---

## Tech Stack

### Frontend

* React 18
* Vite
* Material UI (MUI)
* Vitest
* React Testing Library

### Backend

* ASP.NET Core 10
* Clean Architecture
* Swagger

### DevOps

* Docker
* Docker Compose
* Nginx

---

## Prerequisites

To run the project locally without Docker:

* Node.js 20+
* npm
* .NET 10 SDK

To run with containers:

* Docker
* Docker Compose

---

## Running the Application with Docker

From the project root:

```bash
docker compose up --build
```

This command will:

1. Build the frontend image
2. Build the backend image
3. Start both containers
4. Connect them through Docker networking

### Available Services

| Service     | URL                           |
| ----------- |-------------------------------|
| Frontend    | http://localhost:3000         |
| Backend API | http://localhost:8080         |
| Swagger     | http://localhost:8080/swagger/index.html|

To stop the containers:

```bash
docker compose down
```

---

## Running Locally

### Backend

```bash
cd backend

dotnet restore RpsslGameApi.sln

dotnet run --project RpsslGameApi.Application/RpsslGameApi.Application.csproj
```

API available at:

```text
http://localhost:8080
```

Swagger:

```text
http://localhost:8080/swagger/index.html
```

---

### Frontend

Open a second terminal:

```bash
cd frontend

npm install

npm run dev
```

Application available at:

```text
http://localhost:3000
```

---

## Running Tests

### Frontend

```bash
cd frontend

npm run test
```

### Frontend Watch Mode

```bash
npx vitest
```

### Frontend Test UI

```bash
npx vitest --ui
```

---

## API Endpoints

| Method | Endpoint    | Description                        |
| ------ | ----------- | ---------------------------------- |
| GET    | /choices    | Returns all playable choices       |
| GET    | /choice     | Returns a random computer choice   |
| POST   | /play       | Plays a round against the computer |
| GET    | /scoreboard | Returns game history               |
| DELETE | /scoreboard | Deletes the last scoreboard entry  |

---

## Game Rules

Rock Paper Scissors Spock Lizard extends the traditional game by adding two additional choices.

| Choice   | Beats            |
| -------- | ---------------- |
| Scissors | Paper, Lizard    |
| Rock     | Scissors, Lizard |
| Paper    | Rock, Spock      |
| Lizard   | Spock, Paper     |
| Spock    | Scissors, Rock   |

Reference:

http://www.samkass.com/theories/RPSSL.html
