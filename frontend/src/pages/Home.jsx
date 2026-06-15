import React from "react";
import { Box } from "@mui/material";
import Title from "../components/Home/Title";
import StartButton from "../components/Home/StartButton";
import CircleCarousel from "../components/Home/CircleCarousel";
import { useNavigate } from "react-router-dom";

export default function Home({ items, setItems }) {
    const navigate = useNavigate();
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
                <Title sx={{fontSize: "5rem"}}>LET'S PLAY?</Title>
                <StartButton onClick={() => navigate("/game")}>Start</StartButton>
            </Box>

            <Box
                sx={{
                    p: 4,
                }}
            >
                <CircleCarousel items={items} setItems={setItems} />
            </Box>
        </Box>
    );
}