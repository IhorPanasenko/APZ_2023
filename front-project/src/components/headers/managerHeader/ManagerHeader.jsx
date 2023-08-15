import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import axios from 'axios';


function ManagerHeader() {
    const { t, i18n } = useTranslation();
    const [showMenu, setShowMenu] = useState(false);


    const handleToggleMenu = () => {
        setShowMenu(!showMenu);
    };

    useState(() => {
        const currentLanguage = localStorage.getItem("currentLanguage");
        if (currentLanguage !== i18n.language) {
            i18n.changeLanguage(currentLanguage);
        }
    }, [])

    const handleLanguageChange = (event) => {
        const selectedLanguage = event.target.value;
        i18n.changeLanguage(selectedLanguage);
        localStorage.setItem("currentLanguage", selectedLanguage);
    };

    const handleLogOut = async () =>{
        localStorage.removeItem("token");
        try {
            await axios.post('https://localhost:7082/api/Account/LogOut');
            // Perform any additional actions after successful logout, such as clearing user data or redirecting to a login page
          } catch (error) {
            console.error('Error logging out:', error);
            // Handle error and show appropriate message to the user
          }
    }

    return (
        <header className=''>
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark pt-3">
                <Link className="navbar-brand m-4 mt-0 mb-0" to="/ManagerMainPage">
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
                            <Link className="nav-link" to="/ManagerPersonalPage">
                                {t('managerHeader.personalPage')}
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/ManagerMainPage">
                                {t('managerHeader.insuranceAgents')}
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/ManagerPoliciesPage">
                                {t('managerHeader.insurances')}
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/ManagerCompanyUsers">
                                {t('managerHeader.companyUsers')}
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/ManageraCategoriesPage">
                                {t('managerHeader.categories')}
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/" onClick={() => { handleLogOut() }}>
                                {t('logOut')}
                            </Link>
                            /</li>
                    </ul>
                </div>
                <div className="ml-auto m-4 mt-0 mb-0">
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
}

export default ManagerHeader;
