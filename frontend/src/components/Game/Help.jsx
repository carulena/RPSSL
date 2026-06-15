import React from "react";
import { Box } from "@mui/material";

export default function Help({ children, url }) {
    return (
        <Box>
            <a
            href={url}
            target="_blank"
            rel="noreferrer"
            style={{
            color: "#ff5a00",
            fontFamily: "Poppins, sans-serif",
            fontWeight: 600,
            fontSize: "1rem",
            textDecoration: "none",
            borderBottom: "2px solid #ff5a00",
            paddingBottom: 2,
            transition: "opacity 0.2s ease",
        }}
            onMouseEnter={e => e.target.style.opacity = "0.7"}
            onMouseLeave={e => e.target.style.opacity = "1"}
            >
            {children}
        </a>
</Box>
);
}