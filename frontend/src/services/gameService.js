const BASE_URL = "http://localhost:8080";

export const gameService = {
    async getChoices() {
        const response = await fetch(`${BASE_URL}/choices`);

        if (!response.ok) {
            throw new Error("Failed to fetch choices");
        }

        return response.json();
    },

    async getChoice() {
        const response = await fetch(`${BASE_URL}/choice`);

        if (!response.ok) {
            throw new Error("Failed to fetch choice");
        }

        return response.json();
    },

    async play(player) {
        const response = await fetch(`${BASE_URL}/play`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ player }),
        });

        if (!response.ok) {
            throw new Error("Failed to play");
        }

        return response.json();
    },

    async getScoreboard() {
        const response = await fetch(`${BASE_URL}/scoreboard`);

        if (!response.ok) {
            throw new Error("Failed to fetch scoreboard");
        }

        return response.json();
    },

    async deleteScoreboard() {
        const response = await fetch(`${BASE_URL}/scoreboard`, {
            method: "DELETE",
        });

        if (!response.ok) {
            throw new Error("Failed to delete scoreboard");
        }

        return response.json();
    },
};