import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import NotAuthorizedHeader from '../../components/headers/notAuthorizedHeader/NotAuthorizedHeader';

const RegisterPage = () => {
  const { t } = useTranslation();

  const [email, setEmail] = useState('');
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
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

    // Make API request to register the user
    // Replace with your own code to make the API call
    fetch('https://localhost:7082/api/Account/Register', {
      method: 'POST',
      body: JSON.stringify({ email, userName, password }),
      headers: {
        'Content-Type': 'application/json',
      },
    })
      .then((response) => {
        if (response.ok) {
          // Registration successful, navigate to the login page
          // Replace '/login' with the actual login page route
          window.location.href = '/login';
        } else {
          // Handle registration error
          setError(t('registrationPage.registrationError'));
        }
      })
      .catch((error) => {
        console.log('Registration error:', error);
        setError(t('registrationPage.registrationError'));
      });
  };

  return (
    <>
    <NotAuthorizedHeader/>
      <div className="container mt-5">
        <h2>{t('registrationPage.title')}</h2>
        <form onSubmit={handleRegister}>
          <div className="form-group">
            <label htmlFor="email">{t('registrationPage.email')}</label>
            <input
              type="email"
              className="form-control"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>
          <div className="form-group">
            <label htmlFor="userName">{t('registrationPage.userName')}</label>
            <input
              type="text"
              className="form-control"
              id="userName"
              value={userName}
              onChange={(e) => setUserName(e.target.value)}
            />
          </div>
          <div className="form-group">
            <label htmlFor="password">{t('registrationPage.password')}</label>
            <input
              type="password"
              className="form-control"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
          <div className="form-group">
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
    </>
  );
};

export default RegisterPage;
