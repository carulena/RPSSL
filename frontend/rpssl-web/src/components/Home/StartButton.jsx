import React from "react";
import { Button } from "@mui/material";

export default function StartButton({ children, ...props }) {
    return (
        <Button
            variant="outlined"
            {...props}
            sx={{
                border: "3px solid #ff5a00",
                color: "#ff5a00",
                fontFamily: "Poppins",
                fontWeight: 600,
                fontSize: "1rem",
                textTransform: "none",
                borderRadius: "999px",
                px: 4,
                py: 1.5,

                "&:hover": {
                    borderColor: "#ff5a00",
                    backgroundColor: "#ff5a00",
                    color: "#fff",
                },
            }}
        >
            {children}
        </Button>
    );
}