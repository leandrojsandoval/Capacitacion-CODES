import React from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Home from "./routes/Home.js";
import Store from "./routes/Store.js"

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="store" element={<Store />} />
            </Routes>
        </Router>
    )
}

const root = createRoot(document.getElementById("app"));
root.render(<App />);