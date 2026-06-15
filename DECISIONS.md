# Technical Decisions

## Architecture

The backend was structured using a layered architecture to keep responsibilities separated and the codebase easier to understand, test, and maintain. Controllers are responsible for handling HTTP requests, services contain the application flow, repositories isolate external data access, and mappers handle transformations between domain objects and response contracts.

This structure also supports SOLID principles by keeping classes focused on a single responsibility and depending on abstractions where useful. For this project, the goal was to keep the design organized without adding unnecessary complexity.

## Backend Testing

NUnit was chosen for backend tests because I already had previous experience with the library. This made it easier to write focused tests for the game rules, services, mappers, and repository behavior while keeping the testing setup familiar and productive.

## Frontend Stack

The frontend was built with Vite, React, and Material UI. I chose these tools because I have previous experience with them and they allow fast development, a simple project setup, reusable components, and a clean UI structure.

Vite was used for its fast development server and straightforward build process. React was used to model the game UI with components, state, and user interactions. Material UI helped speed up layout and styling while keeping the interface consistent.

## Frontend Testing

Vitest was chosen for frontend testing because it integrates well with Vite and provides a fast, modern test runner. I also had previous experience with the tool, which made it a practical choice for testing components and hooks in the React application.

## Docker

Docker was added to make it easier to run the backend and frontend together in a consistent environment. This helps validate the integration between both parts of the application and reduces differences between local setups.

## Scoreboard Storage

The scoreboard results are stored in memory because this is a small and simple application. Adding a database such as SQLite would increase the setup and implementation complexity without bringing much value for this challenge.

The scoreboard is meant to show recent game history during the current application session, so the results do not need to be persisted after the application is stopped or restarted.

## Trade-offs

The project intentionally avoids unnecessary infrastructure or complex patterns. The focus was to deliver a working, readable, and testable implementation of the game while keeping the structure practical for the size of the challenge.
