import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import StartButton from "../../components/Home/StartButton";

describe("StartButton", () => {
    it("renders children text", () => {
        render(<StartButton>Start</StartButton>);
        expect(screen.getByRole("button", { name: "Start" })).toBeInTheDocument();
    });

    it("calls onClick when clicked", () => {
        const handleClick = vi.fn();
        render(<StartButton onClick={handleClick}>Click Here</StartButton>);
        fireEvent.click(screen.getByRole("button", { name: "Click Here" }));
        expect(handleClick).toHaveBeenCalledTimes(1);
    });

    it("is disabled when disabled prop is passed", () => {
        render(<StartButton disabled>Disabled</StartButton>);
        expect(screen.getByRole("button", { name: "Disabled" })).toBeDisabled();
    });

    it("does not fire onClick when disabled", () => {
        const handleClick = vi.fn();
        render(
            <StartButton disabled onClick={handleClick}>
                No Click
            </StartButton>
        );
        fireEvent.click(screen.getByRole("button", { name: "No Click" }));
        expect(handleClick).not.toHaveBeenCalled();
    });

    it("forwards extra props to MUI Button", () => {
        render(<StartButton data-testid="my-button">Extra Props</StartButton>);
        expect(screen.getByTestId("my-button")).toBeInTheDocument();
    });

    it("renders with varied children content", () => {
        render(<StartButton>🚀 Launch</StartButton>);
        expect(screen.getByRole("button", { name: "🚀 Launch" })).toBeInTheDocument();
    });
});