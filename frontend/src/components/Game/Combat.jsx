import React, {useEffect, useRef} from "react";
import { Box } from "@mui/material";
import { optionsMap } from "../../constants/optionsMap";
import Fighter from "./Fighter";
import {css, styles} from "../../layout/style"
import StartButton from "../Home/StartButton";
import {useSequence} from "../../hooks/useSequence";

const ALL_KEYS = [1, 2, 3, 4, 5];

export default function Combat({ result, setPlayer, player, setRefresh}) {
    const playerRef = useRef(null);
    const computerRef = useRef(null);
    const vsRef = useRef(null);
    const badgeRef = useRef(null);
    const btnRef = useRef(null);
    const { after, loop, cancel, stopLoop } = useSequence();

    useEffect(() => {
        cancel();
        btnRef.current.style.opacity = "0";
        btnRef.current.style.animation = "none";
        if (!result) return;

        const playerEmoji   = optionsMap[player].emoji;
        const computerEmoji = optionsMap[result.computer].emoji;
        const outcome       = result.results.toLowerCase();

        const pe    = playerRef.current;
        const ce    = computerRef.current;
        const vs    = vsRef.current;
        const badge = badgeRef.current;

        [pe, ce, vs, badge].forEach(el => {
            el.style.animation = "none";
            el.style.opacity   = "0";
            el.style.transform = "none";
        });

        pe.textContent     = playerEmoji;
        pe.style.opacity   = "1";
        pe.style.animation = "slideInLeft 0.4s ease both";

        let i = 0;
        after(700,  () => { ce.style.opacity = "1"; loop(140, () => { ce.textContent = optionsMap[ALL_KEYS[i++ % 5]].emoji; }); });
        after(2100, () => {
            stopLoop();
            ce.textContent = computerEmoji;
            ce.style.animation = "slideInRight 0.35s ease both";
            after(200, () => {
                vs.style.opacity = "1";
                vs.style.animation = "fadeUp 0.3s ease both";
            });
        });        after(2900, () => { pe.style.animation = "smashLeft 0.25s ease";  ce.style.animation = "smashRight 0.25s ease"; });
        after(3200, () => { if (outcome === "win") ce.style.animation = "fallDown 0.5s ease forwards"; if (outcome === "lose") pe.style.animation = "fallDown 0.5s ease forwards"; });
        after(3750, () => {
            badge.textContent   = outcome === "win" ? "🏆 you win!" : outcome === "lose" ? "💀 you lose!" : "🤝 tie!";
            badge.className     = `combat-badge combat-badge--${outcome}`;
            badge.style.opacity = "1"; badge.style.animation = "fadeUp 0.35s ease both";
            if (outcome === "win")  ce.style.cssText = "opacity:1; font-size:150px; line-height:1; display:block;";
            if (outcome === "lose") pe.style.cssText = "opacity:1; font-size:150px; line-height:1; display:block;";
            btnRef.current.style.opacity   = "1";
            btnRef.current.style.animation = "fadeUp 0.35s ease both";
        });
        after(4000, () => setRefresh(r => r + 1));

        return cancel;
    }, [result]);

    return (
        <Box sx={styles.root}>
            <style>{css}</style>
            <Box sx={styles.arena}>
                <Fighter label={"YOU"} emojiRef={playerRef} />
                <span ref={vsRef} style={{ fontSize: 50, opacity: 0, color: "#888" }}>vs</span>
                <Fighter label={"COMPUTER"} emojiRef={computerRef} />
            </Box>
            <div ref={badgeRef} style={{ opacity: 0 }} />
            <div ref={btnRef} style={{ opacity: 0 }}>
                <StartButton onClick={() => setPlayer(null)}>PLAY AGAIN</StartButton>
            </div>
        </Box>
    );
}
