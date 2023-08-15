import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Form, Button, Container, Card } from 'react-bootstrap';
import axios from 'axios';

const URL_FORGOT_PASSWORD = "https://localhost:7082/api/Account/ForgotPassword"

const ForgotPassword = () => {
  const { t } = useTranslation();
  const [email, setEmail] = useState('');

  const handleResetPassword = async (e) => {
    e.preventDefault();

    const data = {
      email: email
    };

    try {
      const response = await axios.post(
        URL_FORGOT_PASSWORD,
        data
      );
      console.log(response.data);
      alert("Check your email");
      // Handle success and show appropriate message to the user
    } catch (error) {
      console.error('Error resetting password:', error);
      // Handle error and show appropriate message to the user
    }
  };

  return (
    <Container className=''>
      <Card className="mt-4 p-4 bg-warning border rounded border-dark border-4">
        <h2 className="text-center">{t('forgotPassword.title')}</h2>
        <Form onSubmit={handleResetPassword}>
          <Form.Group controlId="formEmail" className="mb-3">
            <Form.Label>{t('forgotPassword.emailLabel')}</Form.Label>
            <Form.Control
              type="email"
              placeholder={t('forgotPassword.emailPlaceholder')}
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </Form.Group>
          <Button variant="primary" type="submit" className="mt-2">
            {t('forgotPassword.resetButton')}
          </Button>
        </Form>
      </Card>
    </Container>
  );
};

export default ForgotPassword;
