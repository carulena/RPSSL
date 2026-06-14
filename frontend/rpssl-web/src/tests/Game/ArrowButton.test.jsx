import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import ArrowButton from "../../components/Game/ArrowButton";

describe("ArrowButton", () => {
    it("renders left arrow character", () => {
        render(<ArrowButton direction="left" onClick={vi.fn()} disabled={false} />);
        expect(screen.getByText("‹")).toBeInTheDocument();
    });

    it("renders right arrow character", () => {
        render(<ArrowButton direction="right" onClick={vi.fn()} disabled={false} />);
        expect(screen.getByText("›")).toBeInTheDocument();
    });

    it("calls onClick when not disabled", () => {
        const handleClick = vi.fn();
        render(<ArrowButton direction="right" onClick={handleClick} disabled={false} />);
        fireEvent.click(screen.getByText("›"));
        expect(handleClick).toHaveBeenCalledTimes(1);
    });

    it("does not call onClick when disabled", () => {
        const handleClick = vi.fn();
        render(<ArrowButton direction="right" onClick={handleClick} disabled={true} />);
        fireEvent.click(screen.getByText("›"));
        expect(handleClick).not.toHaveBeenCalled();
    });
});
