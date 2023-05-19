import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import ReactDOM from 'react-dom/client';
import App from './App';
import './localization/config';
import { I18nextProvider } from 'react-i18next';
import i18n from './localization/config';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <I18nextProvider i18n={i18n}>
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </I18nextProvider>
);

