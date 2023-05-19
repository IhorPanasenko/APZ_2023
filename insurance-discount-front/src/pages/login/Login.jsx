import React, { useState } from "react";
import { Form, Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import axios from "axios";

const Login = () => {
  const { t } = useTranslation();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    try {
      const response = await axios.post(
        "https://localhost:7082/api/Account/Login",
        {
          email,
          password,
        }
      );

      const { token, role } = response.data;
      // Handle the token and role here, e.g., store them in localStorage

      // Redirect to appropriate pages based on user role
      // You can use a router library like react-router-dom for navigation
      if (role === "user") {
        // Redirect to user page
      } else if (role === "manager") {
        // Redirect to manager page
      } else if (role === "admin") {
        // Redirect to admin page
      }
    } catch (error) {
      setError(t("invalidCredentials"));
    }
  };

  return (
    <div className="container">
      <h2>{t("login")}</h2>
      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="email">
          <Form.Label>{t("emailAddress")}</Form.Label>
          <Form.Control
            type="email"
            placeholder={t("emailAddress")}
            value={email}
            onChange={handleEmailChange}
            required
          />
        </Form.Group>

        <Form.Group controlId="password">
          <Form.Label>{t("password")}</Form.Label>
          <Form.Control
            type="password"
            placeholder={t("password")}
            value={password}
            onChange={handlePasswordChange}
            required
          />
        </Form.Group>

        {error && <div className="text-danger">{error}</div>}

        <Button variant="primary" type="submit">
          {t("loginButton")}
        </Button>
      </Form>
    </div>
  );
};

export default Login;
