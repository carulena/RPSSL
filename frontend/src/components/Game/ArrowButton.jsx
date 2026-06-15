import { Box } from "@mui/material";

export default function ArrowButton({ direction, onClick, disabled }) {
    return (
        <Box
            onClick={disabled ? undefined : onClick}
            sx={{
                cursor: disabled ? "default" : "pointer",
                width: 40,
                height: 40,
                borderRadius: "50%",
                border: "2px solid #B33E00",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                color: "#B33E00",
                fontSize: "20px",
                userSelect: "none",
                opacity: disabled ? 0.3 : 1,
                transition: "all 0.3s ease",
                flexShrink: 0,
                "&:hover": {
                    opacity: disabled ? 0.3 : 0.7,
                    backgroundColor: disabled ? "transparent" : "rgba(254, 89, 0, 0.1)",
                },
            }}
        >
            {direction === "left" ? "‹" : "›"}
        </Box>
    );
}