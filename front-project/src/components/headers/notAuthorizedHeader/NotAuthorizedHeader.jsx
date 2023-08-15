import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

const NotAuthorizedHeader = () => {
    const { t, i18n } = useTranslation();
    const [showMenu, setShowMenu] = useState(false);

    // const changeLanguage = (lng) => {
    //     i18n.changeLanguage(lng);
    // };

    const handleToggleMenu = () => {
        setShowMenu(!showMenu);
    };

    useState(()=>{
        const currentLanguage = localStorage.getItem("currentLanguage");
        if(currentLanguage !== i18n.language){
            i18n.changeLanguage(currentLanguage);
        }
    },[])

    const handleLanguageChange = (event) => {
        const selectedLanguage = event.target.value;
        i18n.changeLanguage(selectedLanguage);
        localStorage.setItem("currentLanguage", selectedLanguage);
    };

    return (
        <header className=''>
            <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
                <Link className="navbar-brand m-4 mb-0 mt-0" to="/">
                    IDC
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
                            <Link className="nav-link" to="/Register">
                                {t('registration')}
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/">
                                {t('authorization')}
                            </Link>
                        </li>
                    </ul>
                </div>
                <div className="ml-auto">
                    <select
                        className="form-select mt-0 mb-0"
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