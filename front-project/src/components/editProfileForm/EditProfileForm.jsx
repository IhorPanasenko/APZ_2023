import React, { useState } from 'react';
import { Form, Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { Card } from 'react-bootstrap';

const EditProfileForm = ({
    email,
    userName,
    setUserName,
    firstName,
    setFirstName,
    lastName,
    setLastName,
    phoneNumber,
    setPhoneNumber,
    address,
    setAddress,
    birthdayDate,
    setBirthdayDate,
    onUpdateUser,
    onCancelUpdate
}) => {
    const { t } = useTranslation();
    const handleFormSubmit = (e) => {
        e.preventDefault();
        onUpdateUser();
    };

    function formatDate(dateString) {
        const date = new Date(dateString);
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
    
        return `${year}-${month}-${day}`;
      }

    return (
        <Card className="my-3 bg-warning border border-dark rounded border-2">
            <Card.Body>
                <h2>{t('personalUserPage.updateInfoTitle')}</h2>
                <Form onSubmit={handleFormSubmit}>
                    <Form.Group controlId="email">
                        <Form.Label>{t('personalUserPage.email')}</Form.Label>
                        <Form.Control
                            type="email"
                            value={email}
                            disabled
                            className='bg-dark text-light'
                        />
                    </Form.Group>
                    <Form.Group controlId="userName">
                        <Form.Label>{t('personalUserPage.userName')}</Form.Label>
                        <Form.Control
                            type="text"
                            value={userName}
                            onChange={(e) => setUserName(e.target.value)}
                        />
                    </Form.Group>
                    <Form.Group controlId="firstName">
                        <Form.Label>{t('personalUserPage.firstName')}</Form.Label>
                        <Form.Control
                            type="text"
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                        />
                    </Form.Group>
                    <Form.Group controlId="lastName">
                        <Form.Label>{t('personalUserPage.lastName')}</Form.Label>
                        <Form.Control
                            type="text"
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                        />
                    </Form.Group>
                    <Form.Group controlId="phoneNumber">
                        <Form.Label>{t('personalUserPage.phoneNumber')}</Form.Label>
                        <Form.Control
                            type="tel"
                            value={phoneNumber}
                            onChange={(e) => setPhoneNumber(e.target.value)}
                        />
                    </Form.Group>
                    <Form.Group controlId="address">
                        <Form.Label>{t('personalUserPage.address')}</Form.Label>
                        <Form.Control
                            type="text"
                            value={address}
                            onChange={(e) => setAddress(e.target.value)}
                        />
                    </Form.Group>
                    <Form.Group controlId="birthdayDate">
                        <Form.Label>{t('personalUserPage.birthdayDate')}</Form.Label>
                        <Form.Control
                            type="date"
                            value={formatDate(birthdayDate)}
                            onChange={(e) => setBirthdayDate(e.target.value)}
                        />
                    </Form.Group>
                    <div className="d-flex justify-content-center m-2 mb-0 mt-4">
                        <Button className="m-2 mb-2 mt-0 p-5 pt-3 pb-3" variant="primary" type="submit" onClick={(e) => onUpdateUser(e)}>
                            {t('personalUserPage.updateButton')}
                        </Button>
                        <Button className='m-2 mb-2 mt-0 p-5 pt-3 pb-3' variant="secondary" onClick={onCancelUpdate}>
                            {t('personalUserPage.cancelButton')}
                        </Button>
                    </div>
                </Form>
            </Card.Body>
        </Card>
    );
};

export default EditProfileForm;

