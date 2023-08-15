import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Form, Button, Container, Alert } from 'react-bootstrap';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const URL_RESET_PASSWORD = "https://localhost:7082/api/Account/ResetPassword";

const ResetPassword = () => {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [newPassword, setPassword] = useState('');
    const [confirmationNewPassword, setConfirmPassword] = useState('');
    const [code, setCode] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        const resetData = {
            email,
            newPassword,
            confirmationNewPassword,
            code
        };

        try {
            const response = await axios.post(URL_RESET_PASSWORD, resetData);
            // Redirect the user to a success page or another route
            console.log(response.data);
            navigate("/");
        } catch (error) {
            setError(error.response.data.message);
            console.error('Error resetting password:', error.response.data);
        }
    };

    return (
        <Container className='mt-5 p-3 border rounded border-5 bg-info border-success' >
            <h1>{t('resetPassword.title')}</h1>
            {error && <Alert variant="danger">{error}</Alert>}
            <Form onSubmit={handleSubmit}>
                <Form.Group className='mt-3' controlId="formEmail">
                    <Form.Label>{t('resetPassword.emailLabel')}</Form.Label>
                    <Form.Control
                        type="email"
                        placeholder={t('resetPassword.emailPlaceholder')}
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group className='mt-3' controlId="formPassword">
                    <Form.Label>{t('resetPassword.passwordLabel')}</Form.Label>
                    <Form.Control
                        type="password"
                        placeholder={t('resetPassword.passwordPlaceholder')}
                        value={newPassword}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group className='mt-3' controlId="formConfirmPassword">
                    <Form.Label>{t('resetPassword.confirmPasswordLabel')}</Form.Label>
                    <Form.Control
                        type="password"
                        placeholder={t('resetPassword.confirmPasswordPlaceholder')}
                        value={confirmationNewPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        required
                    />
                </Form.Group>

                <Form.Group className='mt-3' controlId="formCode">
                    <Form.Label>{t('resetPassword.codeLabel')}</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder={t('resetPassword.codePlaceholder')}
                        value={code}
                        onChange={(e) => setCode(e.target.value)}
                        required
                    />
                </Form.Group>
                <div className="mt-3 d-flex justify-content-center">
                    <Button className='p-5 pt-3 pb-3' variant="primary" type="submit">
                        {t('resetPassword.resetButton')}
                    </Button>
                </div>
            </Form>
        </Container>
    );
};

export default ResetPassword;

