import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

const NotAuthorizedHeader = () => {
    const { t, i18n } = useTranslation();
    const [showMenu, setShowMenu] = useState(false);

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };

    const handleToggleMenu = () => {
        setShowMenu(!showMenu);
    };

    const handleLanguageChange = (event) => {
        const selectedLanguage = event.target.value;
        i18n.changeLanguage(selectedLanguage);
    };

    return (
        <header>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <Link className="navbar-brand" to="/">
                    Your Logo
                </Link>

                <button
                    className="navbar-toggler"
                    type="button"
                    onClick={handleToggleMenu}
                >
                    <span className="navbar-toggler-icon"></span>
                </button>

                <div
                    className={`collapse navbar-collapse ${showMenu ? 'show' : ''}`}
                    id="navbarSupportedContent"
                >
                    <ul className="navbar-nav ml-auto">
                        <li className="nav-item">
                            <Link className="nav-link" to="/registration">
                                {t('registration')}
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/authorization">
                                {t('authorization')}
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/about">
                                {t('about')}
                            </Link>
                        </li>
                    </ul>
                </div>
                <div className="ml-auto">
                    <select
                        className="form-select"
                        value={i18n.language}
                        onChange={handleLanguageChange}
                    >
                        <option value="en">English</option>
                        <option value="uk">Українська</option>
                    </select>
                </div>
            </nav>
        </header>
    );
};

export default NotAuthorizedHeader;