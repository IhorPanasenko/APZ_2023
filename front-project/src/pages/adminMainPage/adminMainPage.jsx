import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Table, Button, Modal, Form, Card } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import AdminHeader from '../../components/headers/adminHeader/AdminHeader';

const API_URL = "https://localhost:7082/api";
const URL_GET_USERS = `${API_URL}/User/GetAll`;
const URL_GET_ROLES = `${API_URL}/Role/GetAll`;
const URL_CREATE_USER = `${API_URL}/Account/Register`;
const URL_DELETE_USER = `${API_URL}/User/Delete?email=`;
const URL_UPDATE_USER = `${API_URL}/User/Update`;

const AdminMainPage = () => {
    const { t } = useTranslation();
    const [users, setUsers] = useState([]);
    const [roles, setRoles] = useState([]);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [showUpdateModal, setShowUpdateModal] = useState(false);
    const [selectedUser, setSelectedUser] = useState(null);
    const [newUser, setNewUser] = useState({
        email: '',
        userName: '',
        password: '',
        confirmPassword: '',
        role: ''
    });
    const [updatedUser, setUpdatedUser] = useState({
        id: '',
        firstName: '',
        lastName: '',
        userName: '',
        birthdayDate: '',
        email: '',
        phoneNumber: '',
        address: ''
    });

    const getUsers = async () => {
        try {
            const response = await axios.get(URL_GET_USERS);
            setUsers(response.data);
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    };

    const getRoles = async () => {
        try {
            const response = await axios.get(URL_GET_ROLES);
            setRoles(response.data);
        } catch (error) {
            console.error('Error fetching roles:', error);
        }
    };

    useEffect(() => {
        getUsers();
        getRoles();
    }, []);

    const handleCreateUser = async () => {
        try {
            console.log(newUser)
            const response = await axios.post(URL_CREATE_USER, newUser);
            setShowCreateModal(false);
            setNewUser({
                email: '',
                userName: '',
                password: '',
                confirmPassword: '',
                role: ''
            });
            console.log(response.data);
            getUsers();
        } catch (error) {
            console.error('Error creating user:', error);
        }
    };

    const handleDeleteUser = async (email) => {
        try {
            console.log(email);
            const response = await axios.delete(URL_DELETE_USER + email);
            console.log(response.data);
            getUsers();
        } catch (error) {
            console.error('Error deleting user:', error);
        }
    };

    const handleUpdateUser = async () => {
        try {
            console.log(updatedUser);
            console.log(URL_UPDATE_USER);
            const response = await axios.post(URL_UPDATE_USER, updatedUser);
            setShowUpdateModal(false);
            setUpdatedUser({
                id: '',
                firstName: '',
                lastName: '',
                userName: '',
                birthdayDate: '',
                email: '',
                phoneNumber: '',
                address: ''
            });
            console.log(response.data);
            getUsers();
        } catch (error) {
            console.error('Error updating user:', error);
        }
    };

    const handleCreateModalShow = () => {
        console.log(showCreateModal);
        setShowCreateModal(true);
        
    };

    const handleCreateModalClose = () => {
        setShowCreateModal(false);
    };

    const handleUpdateModalShow = (user) => {
        setSelectedUser(user);
        setShowUpdateModal(true);
        setUpdatedUser({
            id: user.id,
            firstName: user.firstName,
            lastName: user.lastName,
            userName: user.userName,
            birthdayDate: user.birthdayDate,
            email: user.email,
            phoneNumber: user.phoneNumber,
            address: user.address
        });
    };

    const handleUpdateModalClose = () => {
        setShowUpdateModal(false);
        setUpdatedUser({
            id: '',
            firstName: '',
            lastName: '',
            userName: '',
            birthdayDate: '',
            email: '',
            phoneNumber: '',
            address: ''
        });
    };

    function formatDate(dateString) {
        const date = new Date(dateString);
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');

        return `${year}-${month}-${day}`;
    }

    return (
        <>
            <AdminHeader />
            <Container>
                <h1 className="my-4">{t('adminMainPage.userList')}</h1>
                <div className="d-grid gap-2 m-2">
                    <Button variant="primary" size="lg" onClick={handleCreateModalShow}>
                        {t('adminMainPage.createUser')}
                    </Button>
                </div>
                {users.map((user) => (
                    <Card key={user.id} className="my-3 bg-info border border-success border-2 rounded">
                        <Card.Body>
                            <Card.Title>{`${user.firstName} ${user.lastName}`}</Card.Title>
                            <Card.Text>{`${t('adminMainPage.email')}: ${user.email}`}</Card.Text>
                            <Card.Text>{`${t('adminMainPage.phoneNumber')}: ${user.phoneNumber}`}</Card.Text>
                            <Card.Text>{t("adminMainPage.birthdayDate")}: {formatDate(user.birthdayDate)}</Card.Text>
                            <Card.Text>{`${t('adminMainPage.address')}: ${user.address}`}</Card.Text>
                            <Card.Text>{t("adminMainPage.role")}: {user.userRoles[0]}</Card.Text>
                            <Card.Text></Card.Text>
                        </Card.Body>
                        <Card.Footer>
                            <div className="d-flex justify-content-center">
                                <Button className='m-1 p-5 pt-2 pb-2' variant="warning" onClick={() => handleUpdateModalShow(user)}>
                                    {t('adminMainPage.edit')}
                                </Button>
                                <Button className='m-1 p-5 pt-2 pb-2' variant="danger" onClick={() => handleDeleteUser(user.email)}>
                                    {t('adminMainPage.delete')}
                                </Button>
                            </div>
                        </Card.Footer>
                    </Card>
                ))}

                {/* Create User Modal */}
                <Modal show={showCreateModal} onHide={handleCreateModalClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>{t('adminMainPage.createUser')}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group controlId="formEmail">
                                <Form.Label>{t('adminMainPage.email')}</Form.Label>
                                <Form.Control
                                    type="email"
                                    placeholder={t('adminMainPage.enterEmail')}
                                    value={newUser.email}
                                    onChange={(e) => setNewUser({ ...newUser, email: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formUserName">
                                <Form.Label>{t('adminMainPage.userName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder={t('adminMainPage.enterUserName')}
                                    value={newUser.userName}
                                    onChange={(e) => setNewUser({ ...newUser, userName: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formPassword">
                                <Form.Label>{t('adminMainPage.password')}</Form.Label>
                                <Form.Control
                                    type="password"
                                    placeholder={t('adminMainPage.enterPassword')}
                                    value={newUser.password}
                                    onChange={(e) => setNewUser({ ...newUser, password: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formConfirmPassword">
                                <Form.Label>{t('adminMainPage.confirmPassword')}</Form.Label>
                                <Form.Control
                                    type="password"
                                    placeholder={t('adminMainPage.confirmPassword')}
                                    value={newUser.confirmPassword}
                                    onChange={(e) => setNewUser({ ...newUser, confirmPassword: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formRole">
                                <Form.Label>{t('adminMainPage.role')}</Form.Label>
                                <Form.Control
                                    as="select"
                                    value={newUser.role.name}
                                    onChange={(e) => setNewUser({ ...newUser, role: e.target.value })}
                                >
                                    {roles.map((role) => (
                                        <option key={role.id} value={role.name}>
                                            {role.name}
                                        </option>
                                    ))}
                                </Form.Control>
                            </Form.Group>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleCreateModalClose}>
                            {t('adminMainPage.close')}
                        </Button>
                        <Button variant="primary" onClick={handleCreateUser}>
                            {t('adminMainPage.create')}
                        </Button>
                    </Modal.Footer>
                </Modal>

                {/* Update User Modal */}
                <Modal show={showUpdateModal} onHide={handleUpdateModalClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>{t('adminMainPage.editUser')}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group controlId="formFirstName">
                                <Form.Label>{t('adminMainPage.firstName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder={t('adminMainPage.enterFirstName')}
                                    value={updatedUser.firstName}
                                    onChange={(e) => setUpdatedUser({ ...updatedUser, firstName: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formLastName">
                                <Form.Label>{t('adminMainPage.lastName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder={t('adminMainPage.enterLastName')}
                                    value={updatedUser.lastName}
                                    onChange={(e) => setUpdatedUser({ ...updatedUser, lastName: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formUserName">
                                <Form.Label>{t('adminMainPage.userName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder={t('adminMainPage.enterUserName')}
                                    value={updatedUser.userName}
                                    onChange={(e) => setUpdatedUser({ ...updatedUser, userName: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formBirthdayDate">
                                <Form.Label>{t('adminMainPage.birthdayDate')}</Form.Label>
                                <Form.Control
                                    type="date"
                                    value={formatDate(updatedUser.birthdayDate)}
                                    onChange={(e) => setUpdatedUser({ ...updatedUser, birthdayDate: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formEmail">
                                <Form.Label>{t('adminMainPage.email')}</Form.Label>
                                <Form.Control
                                    type="email"
                                    placeholder={t('adminMainPage.enterEmail')}
                                    value={updatedUser.email}
                                    onChange={(e) => setUpdatedUser({ ...updatedUser, email: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formPhoneNumber">
                                <Form.Label>{t('adminMainPage.phoneNumber')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder={t('adminMainPage.enterPhoneNumber')}
                                    value={updatedUser.phoneNumber}
                                    onChange={(e) => setUpdatedUser({ ...updatedUser, phoneNumber: e.target.value })}
                                />
                            </Form.Group>
                            <Form.Group controlId="formAddress">
                                <Form.Label>{t('adminMainPage.address')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder={t('adminMainPage.enterAddress')}
                                    value={updatedUser.address}
                                    onChange={(e) => setUpdatedUser({ ...updatedUser, address: e.target.value })}
                                />
                            </Form.Group>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleUpdateModalClose}>
                            {t('adminMainPage.close')}
                        </Button>
                        <Button variant="primary" onClick={handleUpdateUser}>
                            {t('adminMainPage.update')}
                        </Button>
                    </Modal.Footer>
                </Modal>
            </Container>
            <Footer />
        </>
    );
};

export default AdminMainPage;