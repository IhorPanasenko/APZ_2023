import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

const URL_REGISTER_USER = "https://localhost:7082/api/Account/Register";

function RegisterForm() {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const role = "User";
    const [error, setError] = useState('');

    const handleRegister = (e) => {
        e.preventDefault();

        if (!email || !userName || !password || !confirmPassword) {
            setError(t('registrationPage.requiredFieldsError'));
            return;
        }

        if (password !== confirmPassword) {
            setError(t('registrationPage.passwordMismatchError'));
            return;
        }

        const passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/;
        if (!passwordRegex.test(password)) {
            setError(t('registrationPage.passwordFormatError'));
            return;
        }

        fetch(URL_REGISTER_USER, {
            method: 'POST',
            body: JSON.stringify({ email, userName, password,confirmPassword,role }),
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then((response) => {
                if (response.ok) {
                    navigate("/");
                } else {
                    setError(t('registrationPage.registrationError'));
                }
            })
            .catch((error) => {
                console.log('Registration error:', error);
                setError(t('registrationPage.registrationError'));
            });
    };

    return (
        <div className="bg-warning pt-5 pb-5">
            <div className="container p-3 pt-5 pb-5 bg-warning border border-dark rounded border-3">
                <h2>{t('registrationPage.title')}</h2>
                <form onSubmit={handleRegister}>
                    <div className="form-group mt-2 mb-2">
                        <label htmlFor="email">{t('registrationPage.email')}</label>
                        <input
                            type="email"
                            className="form-control"
                            id="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    <div className="form-group mt-2 mb-2">
                        <label htmlFor="userName">{t('registrationPage.userName')}</label>
                        <input
                            type="text"
                            className="form-control"
                            id="userName"
                            value={userName}
                            onChange={(e) => setUserName(e.target.value)}
                        />
                    </div>
                    <div className="form-group mt-2 mb-2">
                        <label htmlFor="password">{t('registrationPage.password')}</label>
                        <input
                            type="password"
                            className="form-control"
                            id="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>
                    <div className="form-group mt-2 mb-3">
                        <label htmlFor="confirmPassword">{t('registrationPage.confirmPassword')}</label>
                        <input
                            type="password"
                            className="form-control"
                            id="confirmPassword"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                        />
                    </div>
                    {error && <div className="alert alert-danger">{error}</div>}
                    <button type="submit" className="btn btn-primary">
                        {t('registrationPage.register')}
                    </button>
                </form>
            </div>
        </div>
    )
}

export default RegisterForm