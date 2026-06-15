# RPSSL Frontend

> Rock Paper Scissors Spock Lizard — web interface built with React + Vite.

---

## Tech Stack

- **React 18** with Vite
- **Material UI (MUI)** for components and styling
- **Vitest** + **React Testing Library** for unit tests

---

## Prerequisites

- Node.js 20+
- npm
- Docker (optional)

---

## Setup & Run

### Local Development

```bash
# 1. Navigate to the frontend folder
cd frontend/

# 2. Install dependencies
npm install

# 3. Start the development server
npm run dev
```

The app will be available at `http://localhost:8080`.


### Production Build

```bash
npm run build
```

The output will be in the `dist/` folder.

---

### Run with Docker

```bash
# 1. Build the image
docker build -t rpssl-frontend .

# 2. Run the container
docker run -p 3000:3000 rpssl-frontend
```

The app will be available at `http://localhost:3000`.

---

## Running Tests

```bash
# Run all tests once
npm run test

# Watch mode (re-runs on file changes)
npx vitest

# Visual UI
npx vitest --ui
```

---

## Project Structure

```
frontend/
├── public/
├── src/
│   ├── assets/
│   ├── components/
│   │   ├── Game/
│   │   │   ├── ArrowButton.jsx
│   │   │   ├── Combat.jsx
│   │   │   ├── Fighter.jsx
│   │   │   ├── Options.jsx
│   │   │   └── Scoreboard.jsx
│   │   └── Home/
│   │       ├── CircleCarousel.jsx
│   │       ├── StartButton.jsx
│   │       └── Title.jsx
│   ├── constants/
│   ├── hooks/
│   │   └── useSequence.js
│   ├── pages/
│   │   └── Game.jsx
│   └── services/
├── tests/
├── Dockerfile
├── nginx.conf
├── vitest.config.js
└── vitest.setup.js
```

---

## Game Rules

RPSSL extends classic Rock Paper Scissors with two extra options.

| Choice   | Beats              |
|----------|--------------------|
| ✂️ Scissors | 📄 Paper, 🦎 Lizard |
| 🪨 Rock     | ✂️ Scissors, 🦎 Lizard |
| 📄 Paper    | 🪨 Rock, 🖖 Spock   |
| 🦎 Lizard   | 🖖 Spock, 📄 Paper  |
| 🖖 Spock    | ✂️ Scissors, 🪨 Rock |

Full rules: [samkass.com/theories/RPSSL.html](http://www.samkass.com/theories/RPSSL.html)