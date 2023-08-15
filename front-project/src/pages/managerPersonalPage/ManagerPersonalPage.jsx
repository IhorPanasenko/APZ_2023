import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Button, Modal, Form } from 'react-bootstrap';
import jwtDecode from 'jwt-decode';
import Footer from '../../components/footer/Footer';
import ManagerHeader from '../../components/headers/managerHeader/ManagerHeader';

const ManagerPersonalPage = () => {
    const { t } = useTranslation();
    const [managerInfo, setManagerInfo] = useState({});
    const [showModal, setShowModal] = useState(false);
    const [updatedInfo, setUpdatedInfo] = useState({
        firstName: '',
        lastName: '',
        address: '',
        birthdayDate: '',
        email: '',
        phoneNumber: '',
    });
    const [userId, setUserId] = useState("");

    const URL_GET_INFO = `https://localhost:7082/api/User/GetById?userId=`;
    const URL_UPDATE_INFO = 'https://localhost:7082/api/User/Update';

    const handleCloseModal = () => {
        setShowModal(false);
    };

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleInputChange = (e) => {
        console.log(e.target.name + ":" + e.target.value)
        setUpdatedInfo((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };

    const handleUpdateInfo = async () => {
        try {
            console.log(updatedInfo);
            const body = {
                id: managerInfo.id,
                firstName: updatedInfo.firstName,
                lastName: updatedInfo.lastName,
                birthdayDate: updatedInfo.birthdayDate,
                phoneNumber: updatedInfo.phoneNumber,
                email: updatedInfo.email,
                address: updatedInfo.address
            }
            const response = await axios.post(URL_UPDATE_INFO, body);
            console.log(response);
            getManagerInfo(managerInfo.id);
            handleCloseModal();
        } catch (error) {
            console.error('Error updating info:', error);
        }
    };

    const getManagerInfo = async (id) => {
        try {
            console.log(id);
            const response = await axios.get(URL_GET_INFO + id);
            setManagerInfo(response.data);
            console.log(response.data);
            setManagerUpdateInfo(response.data);
        } catch (error) {
            console.error('Error fetching manager info:', error);
        }
    };

    const setManagerUpdateInfo = (manager) => {
        updatedInfo.firstName = manager.firstName;
        updatedInfo.lastName = manager.lastName;
        updatedInfo.address = manager.address;
        updatedInfo.birthdayDate = manager.birthdayDate;
        updatedInfo.email = manager.email;
        updatedInfo.phoneNumber = manager.phoneNumber;
    }

    useEffect(() => {
        const token = localStorage.getItem("token");
        console.log(token);
        const decoded = jwtDecode(token);
        console.log(decoded);
        setUserId(decoded.UserId);

        getManagerInfo(decoded.UserId);
    }, []);

    function formatDate(dateString) {
        const date = new Date(dateString);
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');

        return `${year}-${month}-${day}`;
    }
    
    return (
        <>
            <ManagerHeader />
            <div className="d-flex justify-content-center bg-success">
                <Container className='m-4 p-4 pt-2 pb-2 border border-2 border-light rounded'>
                    <h1 className="my-3">{t('managerPersonalPage.personalInfo')}</h1>
                    <div>
                        <p>
                            <strong>{t('managerPersonalPage.firstName')}:</strong> {managerInfo.firstName}
                        </p>
                        <p>
                            <strong>{t('managerPersonalPage.lastName')}:</strong> {managerInfo.lastName}
                        </p>
                        <p>
                            <strong>{t('managerPersonalPage.address')}:</strong> {managerInfo.address}
                        </p>
                        <p>
                            <strong>{t('managerPersonalPage.birthdayDate')}:</strong> {formatDate(managerInfo.birthdayDate)}
                        </p>
                        <p>
                            <strong>{t('managerPersonalPage.email')}:</strong> {managerInfo.email}
                        </p>
                        <p>
                            <strong>{t('managerPersonalPage.phoneNumber')}:</strong> {managerInfo.phoneNumber}
                        </p>
                    </div>

                    <Button variant="primary" className="my-3" onClick={handleShowModal}>
                        {t('managerPersonalPage.updateInfo')}
                    </Button>

                    <Modal show={showModal} onHide={handleCloseModal}>
                        <Modal.Header closeButton>
                            <Modal.Title>{t('managerPersonalPage.updateInfo')}</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form>
                                <Form.Group controlId="formFirstName">
                                    <Form.Label>{t('managerPersonalPage.firstName')}</Form.Label>
                                    <Form.Control
                                        type="text"
                                        name="firstName"
                                        value={updatedInfo.firstName}
                                        onChange={handleInputChange}
                                    />
                                </Form.Group>
                                <Form.Group controlId="formLastName">
                                    <Form.Label>{t('managerPersonalPage.lastName')}</Form.Label>
                                    <Form.Control
                                        type="text"
                                        name="lastName"
                                        value={updatedInfo.lastName}
                                        onChange={handleInputChange}
                                    />
                                </Form.Group>
                                <Form.Group controlId="formAddress">
                                    <Form.Label>{t('managerPersonalPage.address')}</Form.Label>
                                    <Form.Control
                                        type="text"
                                        name="address"
                                        value={updatedInfo.address}
                                        onChange={handleInputChange}
                                    />
                                </Form.Group>
                                <Form.Group controlId="formBirthdayDate">
                                    <Form.Label>{t('managerPersonalPage.birthdayDate')}</Form.Label>
                                    <Form.Control
                                        type="date"
                                        name="birthdayDate"
                                        value={formatDate(updatedInfo.birthdayDate)}
                                        onChange={handleInputChange}
                                    />
                                </Form.Group>
                                <Form.Group controlId="formEmail">
                                    <Form.Label>{t('managerPersonalPage.email')}</Form.Label>
                                    <Form.Control
                                        type="email"
                                        name="email"
                                        value={updatedInfo.email}
                                        onChange={handleInputChange}
                                    />
                                </Form.Group>
                                <Form.Group controlId="formPhoneNumber">
                                    <Form.Label>{t('managerPersonalPage.phoneNumber')}</Form.Label>
                                    <Form.Control
                                        type="tel"
                                        name="phoneNumber"
                                        value={updatedInfo.phoneNumber}
                                        onChange={handleInputChange}
                                    />
                                </Form.Group>
                            </Form>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={handleCloseModal}>
                                {t('managerPersonalPage.cancel')}
                            </Button>
                            <Button variant="primary" onClick={handleUpdateInfo}>
                                {t('managerPersonalPage.save')}
                            </Button>
                        </Modal.Footer>
                    </Modal>
                </Container>
            </div>
            <Footer />
        </>
    );
};

export default ManagerPersonalPage;