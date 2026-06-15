# RPSSL Backend

> Rock Paper Scissors Spock Lizard — REST API built with ASP.NET Core 10.

---

## Tech Stack

- **.NET 10** / ASP.NET Core
- **Clean Architecture** (Application, Domain, Contracts, Infrastructure)
- **Swagger** for API documentation
- **Docker** for containerization

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Docker (optional)

---

## Setup & Run

### Local Development

```bash
# 1. Navigate to the backend folder
cd backend

# 2. Restore dependencies
dotnet restore RpsslGameApi.sln

# 3. Run the API
dotnet run --project RpsslGameApi.Application/RpsslGameApi.Application.csproj
```

The API will be available at `http://localhost:5000`.

---

### Run with Docker

```bash
# 1. Build the image
docker build -t rpssl-backend .

# 2. Run the container
docker run -p 5000:5000 rpssl-backend
```

The API will be available at `http://localhost:5000`.

---

## Configuration

The API uses the following settings in `appsettings.json`:

| Key | Description | Default |
|-----|-------------|---------|
| `RandomConfig.Url` | External random number service | `https://codechallenge.boohma.com/random` |

No additional environment variables are required.

---

## API Documentation

Swagger UI is available at:

```
http://localhost:5000/swagger
```

---

## Endpoints

| Method   | Endpoint | Description                        |
|----------|----------|------------------------------------|
| `GET`    | `/choices` | Returns all playable choices       |
| `GET`    | `/choice` | Returns a random computer choice   |
| `POST`   | `/play` | Plays a round against the computer |
| `GET`    | `/scoreboard` | Returns the game history           |
| `DELETE` | `/scoreboard` | Deletes the last scoreboards       |

---

## Project Structure

```
backend/
├── RpsslGameApi.Application/       # Entry point, controllers, filters, startup
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dockerfile
├── RpsslGameApi.Contracts/         # Request/response models
├── RpsslGameApi.Domain/            # Game logic and entities
├── RpsslGameApi.Infrastructure/    # External services (random API, persistence)
└── RpsslGameApi.sln
```

---

## CORS

The API allows requests from any `localhost` origin (any port), making it compatible with both the React dev server (`localhost:5173`) and the Docker frontend (`localhost:3000`).