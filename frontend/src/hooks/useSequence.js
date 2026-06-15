// hooks/useSequence.js
import { useRef } from "react";

export function useSequence() {
    const timers   = useRef([]);
    const interval = useRef(null);

    const after = (ms, fn) => {
        const t = setTimeout(fn, ms);
        timers.current.push(t);
    };

    const loop = (ms, fn) => {
        interval.current = setInterval(fn, ms);
    };

    const stopLoop = () => clearInterval(interval.current);

    const cancel = () => {
        timers.current.forEach(clearTimeout);
        timers.current = [];
        clearInterval(interval.current);
    };

    return { after, loop, stopLoop, cancel };
}