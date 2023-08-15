import React, { useState } from "react";
import { Form, Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import jwtDecode from "jwt-decode";

const URL_LOGIN = "https://localhost:7082/api/Account/Login";

function LoginForm() {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const [token, setToken] = useState("");

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
                URL_LOGIN,
                {
                    emailAddress:email,
                    password,
                }
            );

            console.log(response.data);
            setToken(response.data);
            let decoded = jwtDecode(response.data);
            console.log(decoded);
            const role= decoded.Role;
            console.log(role);
            localStorage.setItem("token", response.data);

            if (role === "user" || role ==="User") {
                navigate("/Home");
            } else if (role === "manager" || role === "Manager") {
                localStorage.setItem("managerCompanyId", "d833b399-0027-47c1-b4a1-cac346ed041c")
                navigate('/ManagerMainPage');
            } else if (role === "admin" || role === "Admin") {
                navigate("/AdminMainPage")
            }
        } catch (error) {
            setError(t("invalidCredentials"));
        }
    };

    function handleForgotPassword () {
        navigate("/ForgotPassword");
    }

    function handleRegister (){
        navigate("/Register");
    }

    return (
        <div className="bg-warning pb-5 pt-5 h-100 w-100">
            <div className="container bg-warning border border-dark rounded border-3 mb-5 mt-5 p-4">
                <h2>{t("login")}</h2>
                <Form onSubmit={handleSubmit} >
                    <Form.Group controlId="email" className="mt-2 mb-2">
                        <Form.Label>{t("emailAddress")}</Form.Label>
                        <Form.Control
                            type="email"
                            placeholder={t("emailAddress")}
                            value={email}
                            onChange={handleEmailChange}
                            required
                        />
                    </Form.Group>

                    <Form.Group controlId="password" className="mt-2 mb-3">
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

                    <div className="wrapper d-flex justify-content-between w-100">
                        <Button variant="primary" type="submit" className="p-4 pt-2 pb-2 m-1 mt-0 mb-0">
                            {t("loginButton")}
                        </Button>
                        <Button variant="info" className="p-4 pt-2 pb-2 m-1 mt-0 mb-0" onClick={()=>{handleRegister()}}>
                            {t("toRegisterButton")}
                        </Button>
                        <Button variant="info" className="p-4 pt-2 pb-2 m-1 mt-0 mb-0" onClick={() => { handleForgotPassword()}}>
                            {t("ForgotPassword")}
                        </Button>
                    </div>
                </Form>
            </div>
        </div>
    )
}

export default LoginForm