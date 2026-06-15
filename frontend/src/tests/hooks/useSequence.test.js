import { renderHook, act } from "@testing-library/react";
import { describe, it, expect, vi, beforeEach, afterEach } from "vitest";
import { useSequence } from "../../hooks/useSequence";

describe("useSequence", () => {
    beforeEach(() => vi.useFakeTimers());
    afterEach(() => vi.useRealTimers());

    it("after() calls callback after the given delay", () => {
        const { result } = renderHook(() => useSequence());
        const cb = vi.fn();

        act(() => { result.current.after(500, cb); });
        expect(cb).not.toHaveBeenCalled();

        act(() => { vi.advanceTimersByTime(500); });
        expect(cb).toHaveBeenCalledTimes(1);
    });

    it("after() can schedule multiple callbacks independently", () => {
        const { result } = renderHook(() => useSequence());
        const cb1 = vi.fn();
        const cb2 = vi.fn();

        act(() => {
            result.current.after(200, cb1);
            result.current.after(800, cb2);
        });

        act(() => { vi.advanceTimersByTime(200); });
        expect(cb1).toHaveBeenCalledTimes(1);
        expect(cb2).not.toHaveBeenCalled();

        act(() => { vi.advanceTimersByTime(600); });
        expect(cb2).toHaveBeenCalledTimes(1);
    });

    it("loop() calls callback repeatedly at the given interval", () => {
        const { result } = renderHook(() => useSequence());
        const cb = vi.fn();

        act(() => { result.current.loop(100, cb); });
        act(() => { vi.advanceTimersByTime(350); });

        expect(cb).toHaveBeenCalledTimes(3);
    });

    it("stopLoop() stops the interval", () => {
        const { result } = renderHook(() => useSequence());
        const cb = vi.fn();

        act(() => { result.current.loop(100, cb); });
        act(() => { vi.advanceTimersByTime(250); });
        act(() => { result.current.stopLoop(); });
        act(() => { vi.advanceTimersByTime(300); });

        expect(cb).toHaveBeenCalledTimes(2);
    });

    it("cancel() clears all pending timeouts", () => {
        const { result } = renderHook(() => useSequence());
        const cb1 = vi.fn();
        const cb2 = vi.fn();

        act(() => {
            result.current.after(300, cb1);
            result.current.after(600, cb2);
            result.current.cancel();
        });

        act(() => { vi.advanceTimersByTime(600); });
        expect(cb1).not.toHaveBeenCalled();
        expect(cb2).not.toHaveBeenCalled();
    });

    it("cancel() also stops a running loop", () => {
        const { result } = renderHook(() => useSequence());
        const cb = vi.fn();

        act(() => { result.current.loop(100, cb); });
        act(() => { vi.advanceTimersByTime(150); });
        act(() => { result.current.cancel(); });
        act(() => { vi.advanceTimersByTime(300); });

        expect(cb).toHaveBeenCalledTimes(1);
    });

    it("cancel() can be called multiple times without errors", () => {
        const { result } = renderHook(() => useSequence());

        expect(() => {
            act(() => {
                result.current.cancel();
                result.current.cancel();
            });
        }).not.toThrow();
    });
});