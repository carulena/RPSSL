import React, {useEffect, useState} from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Game from "./pages/Game/Game";
import { gameService } from "./services/gameService";
import {optionsMap} from "./constants/optionsMap"

export default function App() {
    const [items, setItems] = useState([]);
    useEffect(() => {
        gameService.getChoices().then((choices) => {
            const mapped = choices.map((c) => ({
                id: c.id,
                name: c.name,
                image: optionsMap[c.id].image,
                emoji: optionsMap[c.id].emoji
            }));
            setItems(mapped);
        });
    }, []);
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home items={items} />} />
                <Route path="/game" element={<Game items={items} />} />
            </Routes>
        </BrowserRouter>
    );
}