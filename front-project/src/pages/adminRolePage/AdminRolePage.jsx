import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Modal, Form } from 'react-bootstrap';
import AdminHeader from '../../components/headers/adminHeader/AdminHeader';
import Footer from '../../components/footer/Footer';

const URL_GET_ALL_ROLES = 'https://localhost:7082/api/Role/GetAll';
const URL_CREATE_ROLE = 'https://localhost:7082/api/Role/Create';
const URL_UPDATE_ROLE = 'https://localhost:7082/api/Role/Update';
const URL_DELETE_ROLE = 'https://localhost:7082/api/Role/Delete?id=';

const AdminRolePage = () => {
    const { t } = useTranslation();
    const [roles, setRoles] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [newRole, setNewRole] = useState({
        id: '',
        name: '',
    });
    const [updatingRoleId, setUpdatingRoleId] = useState(null);

    const handleCloseModal = () => {
        setShowModal(false);
        setUpdatingRoleId(null);
        resetNewRoleFields();
    };

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleInputChange = (e) => {
        setNewRole((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };

    const handleCreateRole = async () => {
        try {
            await axios.post(URL_CREATE_ROLE, newRole);
            getRoles();
            handleCloseModal();
        } catch (error) {
            console.error('Error creating role:', error);
        }
    };

    const handleUpdateRole = async () => {
        try {
            await axios.post(URL_UPDATE_ROLE, newRole);
            getRoles();
            handleCloseModal();
        } catch (error) {
            console.error('Error updating role:', error);
        }
    };

    const handleDeleteRole = async (roleId) => {
        try {
            await axios.delete(URL_DELETE_ROLE + roleId);
            getRoles();
        } catch (error) {
            console.error('Error deleting role:', error);
        }
    };

    const handleEditRole = (role) => {
        setNewRole(role);
        setUpdatingRoleId(role.id);
        handleShowModal();
    };

    const resetNewRoleFields = () => {
        setNewRole({
            id: '',
            name: '',
        });
    };

    const getRoles = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_ROLES);
            setRoles(response.data);
        } catch (error) {
            console.error('Error fetching roles:', error);
        }
    };

    useEffect(() => {
        getRoles();
    }, []);

    return (
        <>
            <AdminHeader />
            <Container>
                <h1 className="my-2">{t('adminRolePage.rolesList')}</h1>
                <div className="d-grid gap-2">
                    <Button variant="primary" className="my-3" size="lg" onClick={handleShowModal}>
                        {t('adminRolePage.createNewRole')}
                    </Button>
                </div>
                {roles.map((role) => (
                    <Card key={role.id} className="my-3 bg-info">
                        <Card.Body>
                            <Card.Title className='text-center'>{role.name}</Card.Title>
                            <div className="d-flex justify-content-center">
                                <Button className="p-3 m-2" variant="danger" onClick={() => handleDeleteRole(role.id)}>
                                    {t('adminRolePage.delete')}
                                </Button>
                                <Button className="p-3 m-2" variant="primary" onClick={() => handleEditRole(role)}>
                                    {t('adminRolePage.update')}
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                ))}

                <Modal show={showModal} onHide={handleCloseModal}>
                    <Modal.Header closeButton>
                        <Modal.Title>{updatingRoleId ? t('adminRolePage.updateRole') : t('adminRolePage.createNewRole')}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group controlId="formRoleName">
                                <Form.Label>{t('adminRolePage.roleName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="name"
                                    value={newRole.name}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleCloseModal}>
                            {t('adminRolePage.cancel')}
                        </Button>
                        {updatingRoleId ? (
                            <Button variant="primary" onClick={handleUpdateRole}>
                                {t('adminRolePage.update')}
                            </Button>
                        ) : (
                            <Button variant="primary" onClick={handleCreateRole}>
                                {t('adminRolePage.create')}
                            </Button>
                        )}
                    </Modal.Footer>
                </Modal>
            </Container>
            <Footer />
        </>
    );
};

export default AdminRolePage;