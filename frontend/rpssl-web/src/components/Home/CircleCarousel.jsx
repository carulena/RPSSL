import React from "react";
import { Box } from "@mui/material";
import { useEffect, useState } from "react";
import rock from "../../assets/images/rock.png";
import paper from "../../assets/images/paper.png";
import scissors from "../../assets/images/scissors.png";
import lizard from "../../assets/images/lizard.png";
import spock from "../../assets/images/spock.png";

const items = [
    rock,
    paper,
    scissors,
    lizard,
    spock,
];

export default function CircleCarousel() {
    const [active, setActive] = useState(0);

    useEffect(() => {
        const timer = setInterval(() => {
            setActive((prev) => (prev + 1) % items.length);
        }, 2000);

        return () => clearInterval(timer);
    }, []);

    return (
        <Box
            sx={{
                position: "relative",
                width: 300,
                height: 600,
            }}
        >
            {items.map((img, index) => {
                const relative =
                    (index - active + items.length) % items.length;

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
                        key={index}
                        sx={{
                            position: "absolute",
                            left: "50%",
                            top: pos.top,
                            width: pos.size,
                            height: pos.size,
                            borderRadius: "50%",
                            backgroundColor: "#ff5a00",

                            transform: "translateX(-50%)",
                            transition:
                                "all 0.8s cubic-bezier(.4,0,.2,1)",

                            display: "flex",
                            justifyContent: "center",
                            alignItems: "center",

                            overflow: "hidden",
                        }}
                    >
                        <img
                            src={img}
                            alt=""
                            style={{
                                width: "150px",
                                height: "150px",
                                objectFit: "contain",
                                position: "absolute",
                                left: "50%",
                                top: "50%",
                                transform: "translate(-50%, -50%)",
                            }}
                        />
                    </Box>
                );
            })}
        </Box>
    );
}