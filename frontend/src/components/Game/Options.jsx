import React, { useEffect, useState } from "react";
import {Box, Typography} from "@mui/material";

export default function Options({ items, setPlayer }) {
    const [visible, setVisible] = useState(false);
    const [hoveredId, setHoveredId] = useState(null);

    useEffect(() => {
        if (items.length === 0) return;
        const timer = setTimeout(() => setVisible(true), 100);
        return () => clearTimeout(timer);
    }, [items]);

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "row",
                justifyContent: "center",
                alignItems: "center",
                gap: 2,
                opacity: visible ? 1 : 0,
                transition: "opacity 0.8s ease-in-out",
            }}
        >
            {items.map((item) => {
                const isHovered = hoveredId === item.id;
                const isSmall = hoveredId !== null && !isHovered;

                return (
                    <Box
                        onClick={() => {
                            setPlayer(item.id);
                        }}
                        key={item.id}
                        onMouseEnter={() => setHoveredId(item.id)}
                        onMouseLeave={() => setHoveredId(null)}
                        sx={{
                            width: 150,
                            height: 150,
                            borderRadius: "50%",
                            backgroundColor: isHovered ? "#39AAA6" : "#ff5a00",
                            display: "flex",
                            justifyContent: "center",
                            alignItems: "center",
                            cursor: "pointer",
                            transition: "all 0.3s ease",
                            transform: isHovered
                                ? "scale(1.4)"
                                : isSmall
                                    ? "scale(0.8)"
                                    : "scale(1)",
                        }}
                    >
                        <Typography sx={{ fontSize: "4rem", lineHeight: 1 }}>
                            {item.emoji}
                        </Typography>
                    </Box>
                );
            })}
        </Box>
    );
}