import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { I18nextProvider } from "react-i18next";
import i18n from "./i18n";
import Login from "./pages/login/Login";

function App() {
  return (
    <I18nextProvider i18n={i18n}>
    <Router>
      <Switch>
        <Route path="/" component={Login} />
      </Switch>
    </Router>
  </I18nextProvider>
  );
}

export default App;
