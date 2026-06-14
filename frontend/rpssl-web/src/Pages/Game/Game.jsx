import React, {useEffect, useState} from "react";
import { Box } from "@mui/material";
import Options from "../../components/Game/Options";
import Scoreboard from "../../components/Game/Scoreboard";
import Title from "../../components/Home/Title";
import Combat from "../../components/Game/Combat";
import Help from "../../components/Game/Help";
import {gameService} from "../../services/gameService";
import {optionsMap} from "../../constants/optionsMap";

export default function Game({ items }) {
    const [player, setPlayer] = useState(null);
    const [results, setResults] = useState([]);
    const [combatResult, setCombatResult] = useState(null);
    const [refresh, setRefresh] = useState(0);

    useEffect(() => {
        gameService.getScoreboard().then((games) => {
            const mapped = games.map((g) => ({
                player: optionsMap[g.player].name,
                emojiPlayer: optionsMap[g.player].emoji,
                computer: optionsMap[g.computer].name,
                emojiComputer: optionsMap[g.computer].emoji,
                result: g.result,
            }));
            setResults(mapped);
        });
    }, [refresh]);

    useEffect(() => {
        if (!player) return;

        const loadCombat = async () => {
            try {
                const response = await gameService.play(player);
                setCombatResult(response);
            }
            catch (error) {}
        };

        loadCombat();
    }, [player]);

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap:1 }}>
            <Scoreboard results={results} />

            <Box sx={{ paddingLeft: 2 }}>
                <Help url="https://www.samkass.com/theories/RPSSL.html">
                    How to Play 🤔
                </Help>
            </Box>

            {player === null ? (
                <Box sx={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    flexDirection: "column",
                    gap: 10,
                }}>
                    <Title sx={{ fontSize: "3rem" }}>Choose your fighter ⚔️</Title>
                    <Box sx={{
                        flex: 1,
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "center",
                    }}>
                        <Options items={items} setPlayer={setPlayer} />
                    </Box>
                </Box>
            ) : (
                <Combat result={combatResult} setPlayer={setPlayer} player={player} setRefresh={setRefresh} />
            )}
        </Box>
    );
}