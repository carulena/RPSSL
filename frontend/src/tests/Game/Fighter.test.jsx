import React, { createRef } from "react";
import { render, screen } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import Fighter from "../../components/Game/Fighter";

vi.mock("../../constants/style", () => ({
    styles: { fighter: {}, emoji: {}, label: {} },
}));

describe("Fighter", () => {
    it("renders the label text", () => {
        render(<Fighter label="YOU" emojiRef={createRef()} />);
        expect(screen.getByText("YOU")).toBeInTheDocument();
    });

    it("renders COMPUTER label", () => {
        render(<Fighter label="COMPUTER" emojiRef={createRef()} />);
        expect(screen.getByText("COMPUTER")).toBeInTheDocument();
    });

    it("attaches emojiRef to the emoji span", () => {
        const ref = createRef();
        render(<Fighter label="YOU" emojiRef={ref} />);
        expect(ref.current).not.toBeNull();
        expect(ref.current.tagName).toBe("SPAN");
    });

    it("renders without label without crashing", () => {
        const { container } = render(<Fighter emojiRef={createRef()} />);
        expect(container.firstChild).toBeInTheDocument();
    });
});
