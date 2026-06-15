import React from "react";
import { render, screen, fireEvent, act } from "@testing-library/react";
import { describe, it, expect, vi, beforeEach, afterEach } from "vitest";
import Options from "../../components/Game/Options";

const mockItems = [
    { id: 1, emoji: "✂️" },
    { id: 2, emoji: "🪨" },
    { id: 3, emoji: "📄" },
];

describe("Options", () => {
    beforeEach(() => vi.useFakeTimers());
    afterEach(() => vi.useRealTimers());

    it("renders all item emojis", () => {
        render(<Options items={mockItems} setPlayer={vi.fn()} />);
        expect(screen.getByText("✂️")).toBeInTheDocument();
        expect(screen.getByText("🪨")).toBeInTheDocument();
        expect(screen.getByText("📄")).toBeInTheDocument();
    });

    it("calls setPlayer with the correct id on click", () => {
        const setPlayer = vi.fn();
        render(<Options items={mockItems} setPlayer={setPlayer} />);
        fireEvent.click(screen.getByText("✂️").closest("div"));
        expect(setPlayer).toHaveBeenCalledWith(1);
    });

    it("calls setPlayer with correct id for each item", () => {
        const setPlayer = vi.fn();
        render(<Options items={mockItems} setPlayer={setPlayer} />);

        fireEvent.click(screen.getByText("🪨").closest("div"));
        expect(setPlayer).toHaveBeenCalledWith(2);

        fireEvent.click(screen.getByText("📄").closest("div"));
        expect(setPlayer).toHaveBeenCalledWith(3);
    });

    it("becomes visible after 100ms", () => {
        const { container } = render(<Options items={mockItems} setPlayer={vi.fn()} />);
        const wrapper = container.firstChild;
        // Initially opacity 0
        expect(wrapper).toHaveStyle({ opacity: "0" });

        act(() => { vi.advanceTimersByTime(150); });

        expect(wrapper).toHaveStyle({ opacity: "1" });
    });

    it("does not set visible timer when items is empty", () => {
        const setTimeoutSpy = vi.spyOn(global, "setTimeout");
        render(<Options items={[]} setPlayer={vi.fn()} />);
        expect(setTimeoutSpy).not.toHaveBeenCalled();
        setTimeoutSpy.mockRestore();
    });

    it("renders nothing when items array is empty", () => {
        const { container } = render(<Options items={[]} setPlayer={vi.fn()} />);
        expect(container.firstChild?.children.length).toBe(0);
    });
});
