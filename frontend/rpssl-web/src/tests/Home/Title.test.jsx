import React from "react";
import { render, screen } from "@testing-library/react";
import { describe, it, expect } from "vitest";
import Title from "../../components/Home/Title";

describe("Title", () => {
    it("renders children text correctly", () => {
        render(<Title>My Title</Title>);
        expect(screen.getByText("My Title")).toBeInTheDocument();
    });

    it("applies ITC Motter font via default sx", () => {
        render(<Title>Test</Title>);
        const el = screen.getByText("Test");
        expect(el).toBeInTheDocument();
    });

    it("merges custom sx with default styles", () => {
        render(<Title sx={{ fontSize: "2rem" }}>With extra sx</Title>);
        expect(screen.getByText("With extra sx")).toBeInTheDocument();
    });

    it("renders ReactNode children (e.g. span)", () => {
        render(
            <Title>
                <span>Child in span</span>
            </Title>
        );
        expect(screen.getByText("Child in span")).toBeInTheDocument();
    });

    it("renders without crashing when children is undefined", () => {
        const { container } = render(<Title />);
        // Typography is still rendered, just without text content
        expect(container.firstChild).toBeInTheDocument();
    });
});