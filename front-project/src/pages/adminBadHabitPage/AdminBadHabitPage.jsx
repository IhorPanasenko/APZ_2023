import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Modal, Form } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import ManagerHeader from '../../components/headers/managerHeader/ManagerHeader';
import AdminHeader from '../../components/headers/adminHeader/AdminHeader';

const URL_GET_ALL_BAD_HABITS = 'https://localhost:7082/api/BadHabit/GetAll';
const URL_CREATE_BAD_HABIT = 'https://localhost:7082/api/BadHabit/Create';
const URL_UPDATE_BAD_HABIT = 'https://localhost:7082/api/BadHabit/Update';
const URL_DELETE_BAD_HABIT = 'https://localhost:7082/api/BadHabit/Delete?id=';

const AdminBadHabitPage = () => {
    const { t } = useTranslation();
    const [badHabits, setBadHabits] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [newBadHabit, setNewBadHabit] = useState({
        name: '',
        level: '',
    });
    const [updatingBadHabitId, setUpdatingBadHabitId] = useState(null);

    const handleCloseModal = () => {
        setShowModal(false);
        setUpdatingBadHabitId(null);
        resetNewBadHabitFields();
    };

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleInputChange = (e) => {
        setNewBadHabit((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };

    const handleCreateBadHabit = async () => {
        try {
            await axios.post(URL_CREATE_BAD_HABIT, newBadHabit);
            getBadHabits();
            handleCloseModal();
        } catch (error) {
            console.error('Error creating bad habit:', error);
        }
    };

    const handleUpdateBadHabit = async () => {
        try {
            const updatedBadHabit = {
                id: updatingBadHabitId,
                name: newBadHabit.name,
                level: newBadHabit.level,
            };
            await axios.put(URL_UPDATE_BAD_HABIT, updatedBadHabit);
            getBadHabits();
            handleCloseModal();
        } catch (error) {
            console.error('Error updating bad habit:', error);
        }
    };

    const handleDeleteBadHabit = async (badHabitId) => {
        try {
            await axios.delete(URL_DELETE_BAD_HABIT + badHabitId);
            getBadHabits();
        } catch (error) {
            console.error('Error deleting bad habit:', error);
        }
    };

    const handleEditBadHabit = (badHabit) => {
        setNewBadHabit(badHabit);
        setUpdatingBadHabitId(badHabit.id);
        handleShowModal();
    };

    const resetNewBadHabitFields = () => {
        setNewBadHabit({
            name: '',
            level: '',
        });
    };

    const getBadHabits = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_BAD_HABITS);
            setBadHabits(response.data);
        } catch (error) {
            console.error('Error fetching bad habits:', error);
        }
    };

    useEffect(() => {
        getBadHabits();
    }, []);

    return (
        < >
            <AdminHeader />
            <div className="bg-info pt-3 pb-3">
                <Container className="pt-2 bg-info">
                    <Card>
                        <Card.Header>{t('adminBadHabitPage.badHabitsList')}</Card.Header>
                        <Card.Body>
                            <div className="d-grid gap-2">
                                <Button variant="primary" size="lg" onClick={handleShowModal}>
                                    {t('adminBadHabitPage.createNewBadHabit')}
                                </Button>
                            </div>
                            <table className="table mt-3 text-center">
                                <thead>
                                    <tr>
                                        <th>{t('adminBadHabitPage.badHabitName')}</th>
                                        <th>{t('adminBadHabitPage.badHabitLevel')}</th>
                                        <th>{t('adminBadHabitPage.actions')}</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {badHabits.map((badHabit) => (
                                        <tr key={badHabit.id}>
                                            <td>{badHabit.name}</td>
                                            <td>{badHabit.level}</td>
                                            <td>
                                                <Button
                                                    variant="primary"
                                                    onClick={() => handleEditBadHabit(badHabit)}
                                                    className='p-4 pt-2 pb-2 m-1 mt-0 mb-0'
                                                >
                                                    {t('adminBadHabitPage.update')}
                                                </Button>
                                                <Button
                                                    variant="danger"
                                                    onClick={() => handleDeleteBadHabit(badHabit.id)}
                                                    className="p-4 pt-2 pb-2 m-1 mt-0 mb-0"
                                                >
                                                    {t('adminBadHabitPage.delete')}
                                                </Button>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </Card.Body>
                    </Card>
                </Container>
            </div>

            {/* Modal for creating/updating bad habits */}
            <Modal show={showModal} onHide={handleCloseModal}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        {updatingBadHabitId
                            ? t('adminBadHabitPage.updateBadHabit')
                            : t('adminBadHabitPage.createBadHabit')}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>{t('adminBadHabitPage.badHabitName')}</Form.Label>
                            <Form.Control
                                type="text"
                                name="name"
                                value={newBadHabit.name}
                                onChange={handleInputChange}
                            />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>{t('adminBadHabitPage.badHabitLevel')}</Form.Label>
                            <Form.Control
                                type="number"
                                min="1"
                                max="100"
                                name="level"
                                value={newBadHabit.level}
                                onChange={handleInputChange}
                            />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleCloseModal}>
                        {t('adminBadHabitPage.cancel')}
                    </Button>
                    {updatingBadHabitId ? (
                        <Button variant="primary" onClick={handleUpdateBadHabit}>
                            {t('adminBadHabitPage.update')}
                        </Button>
                    ) : (
                        <Button variant="primary" onClick={handleCreateBadHabit}>
                            {t('adminBadHabitPage.create')}
                        </Button>
                    )}
                </Modal.Footer>
            </Modal>

            <Footer />
        </>
    );
};

export default AdminBadHabitPage;
