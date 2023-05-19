import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import './localization/config';
import { I18nextProvider } from 'react-i18next';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <I18nextProvider>
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </I18nextProvider>
);

