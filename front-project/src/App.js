import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./pages/login/Login";
import RegisterPage from "./pages/reister/RegisterPage";

function App() {
  return (
      <Router>
        <Routes>
          <Route path="/" Component={Login} />
          <Route path="/registration" Component={RegisterPage}/>
        </Routes>
      </Router>
  );
}

export default App;
