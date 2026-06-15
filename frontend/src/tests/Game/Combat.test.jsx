import React from "react";
import { render, screen, fireEvent
} from "@testing-library/react";
import { describe, it, expect, vi, beforeEach, afterEach } from "vitest";
import Combat from "../../components/Game/Combat";

// Mock constants
vi.mock("../../constants/style", () => ({
    styles: { root: {}, arena: {}, fighter: {}, emoji: {}, label: {} },
    css: "",
}));

vi.mock("../../constants/optionsMap", () => ({
    optionsMap: {
        1: { emoji: "✂️" },
        2: { emoji: "🪨" },
        3: { emoji: "📄" },
        4: { emoji: "🦎" },
        5: { emoji: "🖖" },
    },
}));

// Mock child components
vi.mock("./Fighter", () => ({
    default: ({ label, emojiRef }) => <div><span ref={emojiRef} /><span>{label}</span></div>,
}));
vi.mock("../Home/StartButton", () => ({
    default: ({ children, onClick }) => <button onClick={onClick}>{children}</button>,
}));

// Mock useSequence hook
const mockAfter = vi.fn();
const mockLoop = vi.fn();
const mockCancel = vi.fn();
const mockStopLoop = vi.fn();

vi.mock("../../hooks/useSequence", () => ({
    useSequence: () => ({
        after: mockAfter,
        loop: mockLoop,
        cancel: mockCancel,
        stopLoop: mockStopLoop,
    }),
}));

describe("Combat", () => {
    beforeEach(() => {
        vi.useFakeTimers();
        vi.clearAllMocks();
    });

    afterEach(() => {
        vi.useRealTimers();
    });

    it("renders YOU and COMPUTER labels", () => {
        render(
            <Combat result={null} setPlayer={vi.fn()} player={1} setRefresh={vi.fn()} />
        );
        expect(screen.getByText("YOU")).toBeInTheDocument();
        expect(screen.getByText("COMPUTER")).toBeInTheDocument();
    });

    it("renders the PLAY AGAIN button", () => {
        render(
            <Combat result={null} setPlayer={vi.fn()} player={1} setRefresh={vi.fn()} />
        );
        expect(screen.getByRole("button", { name: "PLAY AGAIN" })).toBeInTheDocument();
    });

    it("calls setPlayer(null) when PLAY AGAIN is clicked", () => {
        const setPlayer = vi.fn();
        render(
            <Combat result={null} setPlayer={setPlayer} player={1} setRefresh={vi.fn()} />
        );
        fireEvent.click(screen.getByRole("button", { name: "PLAY AGAIN" }));
        expect(setPlayer).toHaveBeenCalledWith(null);
    });

    it("calls cancel on mount when result is null", () => {
        render(
            <Combat result={null} setPlayer={vi.fn()} player={1} setRefresh={vi.fn()} />
        );
        expect(mockCancel).toHaveBeenCalled();
    });

    it("starts animation sequence when result is provided", () => {
        render(
            <Combat
                result={{ computer: 2, result: "Win" }}
                setPlayer={vi.fn()}
                player={1}
                setRefresh={vi.fn()}
            />
        );
        expect(mockAfter).toHaveBeenCalled();
    });

    it("renders vs text", () => {
        render(
            <Combat result={null} setPlayer={vi.fn()} player={1} setRefresh={vi.fn()} />
        );
        expect(screen.getByText("vs")).toBeInTheDocument();
    });
});
