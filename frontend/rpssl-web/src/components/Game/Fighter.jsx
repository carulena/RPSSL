import React, {useRef} from "react";
import { Box } from "@mui/material";
import {styles} from "../../layout/style"
export default function Fighter({ emojiRef, label }) {
    return (
        <Box sx={styles.fighter}>
            <span ref={emojiRef} style={styles.emoji} />
            <span style={styles.label}>{label}</span>
        </Box>
    );
}