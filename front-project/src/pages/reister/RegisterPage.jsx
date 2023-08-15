import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import NotAuthorizedHeader from '../../components/headers/notAuthorizedHeader/NotAuthorizedHeader';
import RegisterForm from '../../components/registerForm/RegisterForm';
import Footer from '../../components/footer/Footer';

const RegisterPage = () => {

  return (
    <>
    <NotAuthorizedHeader/>
     <RegisterForm/>
     <Footer/>
    </>
  );
};

export default RegisterPage;