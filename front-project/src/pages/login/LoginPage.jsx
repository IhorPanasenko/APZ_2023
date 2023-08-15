import React from "react";
import NotAuthorizedHeader from "../../components/headers/notAuthorizedHeader/NotAuthorizedHeader";
import LoginForm from "../../components/loginForm/LoginForm";
import Footer from "../../components/footer/Footer";

const LoginPage = () => {

    return (
        <>
            <NotAuthorizedHeader />
            <LoginForm />
            <Footer />
        </>
    );
};

export default LoginPage;