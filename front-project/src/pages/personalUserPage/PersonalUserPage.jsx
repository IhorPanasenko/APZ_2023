import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Form } from 'react-bootstrap';
import { useParams, useNavigate } from 'react-router-dom';
import Footer from '../../components/footer/Footer';
import UserHeader from '../../components/headers/userHeader/UserHeader';
import jwtDecode from 'jwt-decode';
import UserInsurances from '../../components/usersInsurances/UsersInsurances';
import EditProfileForm from '../../components/editProfileForm/EditProfileForm';
import UserBadHabits from '../../components/userBadHabits/UserBadHabits';
import LifeIndicators from '../../components/lifeIndicators/LifeIndicators';
import UserNutritions from '../../components/userNutritions/UserNutritions';
import UserActivities from '../../components/userActivities/UserActivities';

const URL_GET_USER_BY_ID = "https://localhost:7082/api/User/GetById?userId=";
const URL_UPDATE_USER = "https://localhost:7082/api/User/Update";
const URL_RECALCULATE_DISCOUNT = "https://localhost:7082/api/User/CalculateDiscount?userId=";
const TEST_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6Imlob3IucGFuYXNlbmtvMUBudXJlLnVhLmNvbSIsIlVzZXJOYW1lIjoiSWhvcjEyMyIsIlVzZXJJZCI6IjY0N2E1ZjI5LWNkZWEtNDFkYi1hZmMzLWQzZmI0NDI1ZTdhMSIsImV4cCI6MTY4NzUyOTk1NSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA4MiIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcwODIifQ.7h_DLXovvbbTx0vJqU0cQdEc7g5C5ziO3ioV3kRXa78";
const URL_FORGOT_PASSWORD = "https://localhost:7082/api/Account/ForgotPassword"

const PersonalUserPage = () => {
  const { t } = useTranslation();
  const [user, setUser] = useState(null);
  const [userId, setUserId] = useState("");
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [address, setAddress] = useState('');
  const [birthdayDate, setBirthdayDate] = useState('');
  const [discount, setDiscount] = useState('');
  const [userName, setUserName] = useState('');
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [isEditMode, setIsEditMode] = useState(false);
  const [isRendered, setIsRendered] = useState(false);
  const [isBadHabits, setIsBadHabits] = useState(false);
  const [isLifeIndicators, setIsLifeIndicators] = useState(false);
  const [isNutritions, setIsNutritions] = useState(false);
  const [isActivities, setIsActivities] = useState(false);

  const navigate = useNavigate();

  const getUser = async (userId) => {
    try {
      const response = await axios.get(`${URL_GET_USER_BY_ID}${userId}`);
      setUser(response.data);

    } catch (error) {
      console.error('Error fetching user:', error);
    }
  };

  const setValues = () => {
    console.log(user);
    setFirstName(user?.firstName);
    setAddress(user?.address);
    setBirthdayDate(user?.birthdayDate);
    setDiscount(user?.discount);
    setEmail(user?.email);
    setLastName(user?.lastName);
    setPhoneNumber(user?.phoneNumber);
    setUserName(user?.userName);
  }

  useEffect(() => {
    const token = localStorage.getItem('token');
    console.log(token);
    var tokenStructure = jwtDecode(token);
    console.log(tokenStructure);
    var userId = tokenStructure?.UserId;
    setUserId(userId);

    console.log(userId);
    getUser(userId);
    setIsRendered(true);

  }, []);

  const handleUpdateUser = async (e) => {
    e.preventDefault();

    const updatedUser = {
      id: user.id,
      firstName,
      lastName,
      userName,
      birthdayDate,
      email,
      phoneNumber,
      address
    };
    console.log(updatedUser);

    try {
      const response = await axios.post(URL_UPDATE_USER, updatedUser);
      console.log(response.data);

      getUser(userId);
    } catch (error) {
      console.error('Error updating user:', error);
    }
  };

  const handleCancelUpdate = () => {
    setIsEditMode(false);
  };

  const handleUpdateClick = () => {
    console.log(isEditMode);
    if (!isEditMode) {
      setValues();
      setIsEditMode(true);
    }
    else {
      setIsEditMode(false);
    }
  };

  const handleBadHabitsClick = () => {
    if (!isBadHabits) {
      setIsBadHabits(true);
    }
    else {
      setIsBadHabits(false);
    }
  }

  const handleCloseBadHabit = () => {
    setIsBadHabits(false);
  }

  const handleNutritionClick = () => {
    if (!isNutritions) {
      setIsNutritions(true);
    }
    else {
      setIsNutritions(false);
    }
  }

  const handleCloseNutritions = () => {
    setIsNutritions(false);
  }

  const handleActivitiesClick = () => {
    if (!isActivities) {
      setIsActivities(true);
    }
    else {
      setIsActivities(false);
    }
  }

  const handleActivitiesClose = () => {
    setIsActivities(false);
  }

  const handleIndicatorsClick = () => {
    if (!isLifeIndicators) {
      setIsLifeIndicators(true);
    } else {
      setIsLifeIndicators(false);
    }
  }

  const handleLifeIndicatorsColapse = () => {
    setIsLifeIndicators(false);
  }

  const handleRecalculateDiscountClick = async () => {
    try {
      console.log(userId);
      const response = await axios.post(URL_RECALCULATE_DISCOUNT + userId);
      console.log(response.data);
      getUser(userId);
    } catch (error) {
      console.error('Error updating user:', error);
    }
  }

  const handleForgotPassword = async () => {
    console.log(user.email);
    
     const data = {
      email: user.email
    };

    try {
      const response = await axios.post(
        URL_FORGOT_PASSWORD,
        data
      );
      console.log(response.data);
      alert("Check your email");
    } catch (error) {
      console.error('Error resetting password:', error);
    }
  }

  function formatDate(dateString) {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  if (isRendered) {
    return (
      <>
        <UserHeader />
        <div className="bg-info">
          <Container className="pt-3 pb-5 bg-info">
            <h1 className="my-4">{t('personalUserPage.title')}</h1>
            {user && (
              <>
                <Card className="my-3 bg-warning border border-dark rounded border-2">
                  <Card.Body>
                    <h2>{t('personalUserPage.userInfoTitle')}</h2>
                    <p>
                      <strong>{t('personalUserPage.firstName')}:</strong> {user.firstName}
                    </p>
                    <p>
                      <strong>{t('personalUserPage.lastName')}:</strong> {user.lastName}
                    </p>
                    <p>
                      <strong>{t('personalUserPage.address')}:</strong> {user.address}
                    </p>
                    <p>
                      <strong>{t('personalUserPage.birthdayDate')}:</strong> {formatDate(user.birthdayDate)}
                    </p>
                    <p>
                      <strong>{t('personalUserPage.discount')}:</strong> {user.discount}
                    </p>
                    <p>
                      <strong>{t('personalUserPage.userName')}:</strong> {user.userName}
                    </p>
                    <p>
                      <strong>{t('personalUserPage.email')}:</strong> {user.email}
                    </p>
                    <p>
                      <strong>{t('personalUserPage.phoneNumber')}:</strong> {user.phoneNumber}
                    </p>
                    <div className="d-flex m-2 justify-content-center">
                      <Button className='m-2 mb-2 mt-0 p-4 pt-3 pb-3' variant="primary" onClick={() => handleUpdateClick()}>
                        {t('personalUserPage.updateInfoButton')}
                      </Button>
                      <Button className='m-2 mb-2 mt-0 p-4 pt-3 pb-3' variant="primary" onClick={() => handleForgotPassword()}>
                        {t('personalUserPage.changePasswordButton')}
                      </Button>
                    </div>
                    <div className="d-flex m-2 justify-content-center">
                      <Button className='m-2 mb-2 mt-0 p-4 pt-3 pb-3' variant="primary" onClick={() => handleBadHabitsClick()}>
                        {t('personalUserPage.badHabitsButton')}
                      </Button>
                      <Button className='m-2 mb-2 mt-0 p-4 pt-3 pb-3' variant="primary" onClick={() => handleNutritionClick()}>
                        {t('personalUserPage.NutritionButton')}
                      </Button>
                    </div>
                    <div className="d-flex m-2 justify-content-center">
                      <Button className='m-2 mb-2 mt-0 p-4 pt-3 pb-3' variant="primary" onClick={() => handleActivitiesClick()}>
                        {t('personalUserPage.Activities')}
                      </Button>
                      <Button className='m-2 mb-2 mt-0 p-4 pt-3 pb-3' variant="primary" onClick={() => handleIndicatorsClick()}>
                        {t('personalUserPage.IndicatorsButton')}
                      </Button>
                    </div>
                    <div className="d-grid gap-2">
                      <Button variant="primary" size="lg" onClick={() => handleRecalculateDiscountClick()}>
                        {t('personalUserPage.RecalculateDiscount')}
                      </Button>
                    </div>
                  </Card.Body>
                </Card>
                {isEditMode && (
                  <EditProfileForm
                    email={email}
                    userName={userName}
                    setUserName={setUserName}
                    firstName={firstName}
                    setFirstName={setFirstName}
                    lastName={lastName}
                    setLastName={setLastName}
                    phoneNumber={phoneNumber}
                    setPhoneNumber={setPhoneNumber}
                    address={address}
                    setAddress={setAddress}
                    birthdayDate={birthdayDate}
                    setBirthdayDate={setBirthdayDate}
                    onUpdateUser={handleUpdateUser}
                    onCancelUpdate={handleCancelUpdate}
                  />
                )}
                {isBadHabits && (
                  <UserBadHabits
                    userId={userId}
                    handleCloseBadHabit={handleCloseBadHabit} />
                )}
                {isLifeIndicators && (
                  <LifeIndicators
                    userId={userId}
                    LifeIndicatorsColapseClick={handleLifeIndicatorsColapse}
                  />
                )}
                {isNutritions && (
                  <UserNutritions
                    userId={userId}
                    handleClose={handleCloseNutritions}
                  />
                )}
                {isActivities && (
                  <UserActivities
                    userId={userId}
                    handleClose={handleActivitiesClose}
                  />
                )}
              </>
            )}
            <UserInsurances userId={user?.id} />
          </Container>
        </div>
        <Footer />
      </>
    );
  } else {
    return <h1>Please wait. Loading...</h1>
  }
};

export default PersonalUserPage;