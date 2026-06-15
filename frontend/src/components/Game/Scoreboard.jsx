import React, { useEffect, useRef, useState } from "react";
import { Box, Typography } from "@mui/material";
import ArrowButton from "./ArrowButton";
import Title from "../Home/Title";

import win from "../../assets/images/win.png";
import lose from "../../assets/images/lose.png";
import tie from "../../assets/images/tie.png";
import StartButton from "../Home/StartButton";
import {gameService} from "../../services/gameService";

const resultMap = { Win: win, Lose: lose, Tie: tie };

export default function Scoreboard({ results, setResults }) {
    const scrollRef = useRef(null);
    const [showLeft, setShowLeft]   = useState(false);
    const [showRight, setShowRight] = useState(false);
    const [animKey, setAnimKey]     = useState(0);

    const deleteScoreboard = () => {
        gameService.deleteScoreboard().then(() => {
            setResults([])
        })
    };
    const checkArrows = () => {
        const el = scrollRef.current;
        if (!el) return;
        setShowLeft(el.scrollLeft > 0);
        setShowRight(el.scrollLeft + el.clientWidth < el.scrollWidth - 1);
    };

    useEffect(() => {
        setAnimKey(k => k + 1);
        checkArrows();
    }, [results]);

    const scroll = (dir) => {
        scrollRef.current?.scrollBy({ left: dir === "right" ? 600 : -600, behavior: "smooth" });
    };
    if (results.length === 0) {
        return (
            <Box sx={{ display: "flex", alignItems: "center", gap: 5, borderBottom: "3px solid #BF6230",
                top: 0, position: "relative", width: "100vw", marginLeft: "calc(-50vw + 50%)", pb: 2 }}>
                <Title sx={{ color: "#BF6230", fontSize: "1rem", pb: 2, pt: 3, left: 50, position: "relative" }}>
                    No battles yet. Are you scared? 👀
                </Title>
            </Box>
        );
    }

    return (
        <Box sx={{ display: "flex", alignItems: "center", gap: 5, borderBottom: "3px solid #BF6230",
            top: -40, position: "relative", width: "100vw", marginLeft: "calc(-50vw + 50%)" }}>
            <Title sx={{ fontSize: "2rem", paddingLeft: 4, position: "relative", top: 20, color: "#BF6230" }}>
                Scoreboard
            </Title>

            <Box sx={{ display: "flex", alignItems: "center", width: "80%", height: 170 }}>
                <ArrowButton direction="left"  onClick={() => scroll("left")}  disabled={!showLeft} />

                <Box ref={scrollRef} onScroll={checkArrows} sx={{
                    display: "flex", flexDirection: "row", overflowX: "auto", flex: 1,
                    padding: "30px 0 0 0", scrollBehavior: "smooth",
                    "&::-webkit-scrollbar": { display: "none" },
                    msOverflowStyle: "none", scrollbarWidth: "none",
                }}>
                    {results.map((item, index) => (
                        <Box key={`${animKey}-${index}`} sx={{
                            display: "flex", flexDirection: "column", alignItems: "center",
                            padding: "0 2rem",
                            borderRight: index < results.length - 1 ? "3px solid #BF6230" : "none",
                            animation: `fallIn 0.4s ease ${index * 0.1}s both`,
                            "@keyframes fallIn": {
                                "0%":   { opacity: 0, transform: "translateY(-40px)" },
                                "100%": { opacity: 1, transform: "translateY(0)" },
                            },
                        }}>
                            <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
                                <Typography sx={{ fontSize: "4rem", lineHeight: 1 }}>{item.emojiPlayer}</Typography>
                                <Typography sx={{ fontSize: "2rem", lineHeight: 1 }}>🆚</Typography>
                                <Typography sx={{ fontSize: "4rem", lineHeight: 1 }}>{item.emojiComputer}</Typography>
                            </Box>
                            <img src={resultMap[item.result]} alt={item.result}
                                 style={{ width: 90, height: 32, objectFit: "contain" }} />
                        </Box>
                    ))}
                </Box>

                <ArrowButton direction="right" onClick={() => scroll("right")} disabled={!showRight} />
                <StartButton sx={{ fontSize: "0.75rem", px: 2, py: 0.8, border: "2px solid #ff5a00" }}
                             onClick={() => deleteScoreboard()}>
                    Delete scoreboard
                </StartButton>
            </Box>
        </Box>
    );
}