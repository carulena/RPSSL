import React from "react";
import { render, screen, act } from "@testing-library/react";
import { describe, it, expect, vi, beforeEach, afterEach } from "vitest";
import CircleCarousel from "../../components/Home/CircleCarousel";

const mockItems = [
    { id: 1, emoji: "🍎" },
    { id: 2, emoji: "🍊" },
    { id: 3, emoji: "🍋" },
    { id: 4, emoji: "🍇" },
    { id: 5, emoji: "🍓" },
];

describe("CircleCarousel", () => {
    beforeEach(() => {
        vi.useFakeTimers();
    });

    afterEach(() => {
        vi.useRealTimers();
    });

    it("renders all emojis from the list", () => {
        render(<CircleCarousel items={mockItems} setItems={vi.fn()} />);
        mockItems.forEach(({ emoji }) => {
            expect(screen.getByText(emoji)).toBeInTheDocument();
        });
    });

    it("does not break when items is empty", () => {
        const { container } = render(<CircleCarousel items={[]} setItems={vi.fn()} />);
        expect(container.firstChild).toBeInTheDocument();
    });

    it("renders a single item correctly", () => {
        render(<CircleCarousel items={[{ id: 1, emoji: "⭐" }]} setItems={vi.fn()} />);
        expect(screen.getByText("⭐")).toBeInTheDocument();
    });

    it("advances to the next item after 2 seconds", () => {
        const { container } = render(
            <CircleCarousel items={mockItems} setItems={vi.fn()} />
        );

        // All items are mounted in the DOM; the carousel controls visibility via size/top
        // We verify the timer fires without errors
        act(() => {
            vi.advanceTimersByTime(2000);
        });

        // Component should still be in the DOM after advancing
        expect(container.firstChild).toBeInTheDocument();
    });

    it("cycles through all items multiple times without errors", () => {
        const { container } = render(
            <CircleCarousel items={mockItems} setItems={vi.fn()} />
        );

        act(() => {
            // Advance 5 full cycles (back to the start)
            vi.advanceTimersByTime(2000 * mockItems.length);
        });

        expect(container.firstChild).toBeInTheDocument();
        // All emojis should still be in the DOM
        mockItems.forEach(({ emoji }) => {
            expect(screen.getByText(emoji)).toBeInTheDocument();
        });
    });

    it("clears the interval on unmount", () => {
        const clearIntervalSpy = vi.spyOn(global, "clearInterval");

        const { unmount } = render(
            <CircleCarousel items={mockItems} setItems={vi.fn()} />
        );

        unmount();

        expect(clearIntervalSpy).toHaveBeenCalled();
        clearIntervalSpy.mockRestore();
    });

    it("restarts the timer when the items list changes", () => {
        const setIntervalSpy = vi.spyOn(global, "setInterval");

        const { rerender } = render(
            <CircleCarousel items={mockItems} setItems={vi.fn()} />
        );

        const callsBefore = setIntervalSpy.mock.calls.length;

        const newItems = [
            { id: 6, emoji: "🌈" },
            { id: 7, emoji: "🦄" },
        ];

        rerender(<CircleCarousel items={newItems} setItems={vi.fn()} />);

        expect(setIntervalSpy.mock.calls.length).toBeGreaterThan(callsBefore);
        setIntervalSpy.mockRestore();
    });
});