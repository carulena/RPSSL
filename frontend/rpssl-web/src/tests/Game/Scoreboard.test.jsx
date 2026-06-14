import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import Scoreboard from "../../components/Game/Scoreboard";

// Mock image imports
vi.mock("../../assets/images/win.png", () => ({ default: "win.png" }));
vi.mock("../../assets/images/lose.png", () => ({ default: "lose.png" }));
vi.mock("../../assets/images/tie.png", () => ({ default: "tie.png" }));

// Mock child components
vi.mock("../Home/Title", () => ({
    default: ({ children }) => <span>{children}</span>,
}));
vi.mock("./ArrowButton", () => ({
    default: ({ direction, onClick, disabled }) => (
        <button onClick={onClick} disabled={disabled} data-testid={`arrow-${direction}`}>
            {direction}
        </button>
    ),
}));

const mockResults = [
    { emojiPlayer: "✂️", emojiComputer: "🪨", result: "Lose" },
    { emojiPlayer: "🪨", emojiComputer: "📄", result: "Lose" },
    { emojiPlayer: "📄", emojiComputer: "✂️", result: "Lose" },
];

describe("Scoreboard", () => {
    it("shows empty state message when results is empty", () => {
        render(<Scoreboard results={[]} />);
        expect(screen.getByText(/no battles yet/i)).toBeInTheDocument();
    });

    it("renders Scoreboard title", () => {
        render(<Scoreboard results={[]} />);
        expect(screen.getByText("Scoreboard")).toBeInTheDocument();
    });

    it("renders player and computer emojis for each result", () => {
        render(<Scoreboard results={mockResults} />);
        expect(screen.getAllByText("✂️").length).toBeGreaterThan(0);
        expect(screen.getAllByText("🪨").length).toBeGreaterThan(0);
    });

    it("renders a vs icon for each battle", () => {
        render(<Scoreboard results={mockResults} />);
        expect(screen.getAllByText("🆚").length).toBe(mockResults.length);
    });

    it("renders result images with correct alt text", () => {
        render(<Scoreboard results={mockResults} />);
        const imgs = screen.getAllByRole("img");
        imgs.forEach((img) => {
            expect(["Win", "Lose", "Tie"]).toContain(img.getAttribute("alt"));
        });
    });

    it("renders left and right arrow buttons", () => {
        render(<Scoreboard results={mockResults} />);
        expect(screen.getByText("‹")).toBeInTheDocument();
        expect(screen.getByText("›")).toBeInTheDocument();
    });

    it("left arrow is disabled initially (no scroll)", () => {
        render(<Scoreboard results={mockResults} />);
        expect(screen.getByText("‹")).toBeInTheDocument();
    });
});
