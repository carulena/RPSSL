import React, { useEffect } from "react";
import { Box } from "@mui/material";
export default function CircleCarousel({ items }) {
    const [active, setActive] = React.useState(0);

    useEffect(() => {
        if (items.length === 0) return;

        const timer = setInterval(() => {
            setActive((prev) => (prev + 1) % items.length);
        }, 2000);

        return () => clearInterval(timer);
    }, [items]);

    return (
        <Box sx={{ position: "relative", width: 300, height: 600 }}>
            {items.map((item, index) => {
                const relative = (index - active + items.length) % items.length;

                const positions = [
                    { top: 600, size: 0 },
                    { top: 500, size: 90 },
                    { top: 220, size: 200 },
                    { top: 50, size: 90 },
                    { top: -200, size: 0 },
                ];

                const pos = positions[relative];

                return (
                        <Box
                            key={item.id}
                            sx={{
                                position: "absolute",
                                left: "50%",
                                top: pos.top,
                                width: pos.size,
                                height: pos.size,
                                borderRadius: "50%",
                                backgroundColor: "#ff5a00",
                                transform: "translateX(-50%)",
                                transition: "all 0.8s cubic-bezier(.4,0,.2,1)",
                                display: "flex",
                                justifyContent: "center",
                                alignItems: "center",
                                overflow: "hidden",
                                fontSize: pos.size * 0.5,
                            }}
                        >
                            {item.emoji}
                        </Box>
                );
            })}
        </Box>
    );
}