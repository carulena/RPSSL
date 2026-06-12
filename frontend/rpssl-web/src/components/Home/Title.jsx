import React from "react";
import { Typography } from "@mui/material";

export default function Title({ children }) {
    return (
        <Typography
            sx={{
                fontFamily: "ITC Motter, sans-serif",
                fontSize: "5rem",
                lineHeight: 0.9,
                letterSpacing: "-0.04em",
                color: "#ff5a00",
            }}
        >
            {children}
        </Typography>
    );
}