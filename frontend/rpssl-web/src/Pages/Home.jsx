import React from "react";
import { Box } from "@mui/material";
import Title from "../components/Home/Title";
import StartButton from "../components/Home/StartButton";
import ItensAnimation from "../components/Home/CircleCarousel";

export default function Home() {
    return (
        <Box
            sx={{
                minHeight: "100vh",
                display: "grid",
                gridTemplateColumns: "2fr 1fr",
            }}
        >
            <Box
                sx={{
                    p: 10,
                    display: "flex",
                    flexDirection: "column",
                    alignItems: "center",
                    justifyContent: "center",
                    gap: 7,
                }}
            >
                <Title>LET'S PLAY?</Title>
                <StartButton>Start</StartButton>
            </Box>

            <Box
                sx={{
                    p: 4,
                }}
            >
                <ItensAnimation/>
            </Box>
        </Box>
    );
}