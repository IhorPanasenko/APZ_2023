import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Form, Button, Table, Modal, Card } from 'react-bootstrap';
import axios from 'axios';

const URL_GET_ACTIVITIES = 'https://localhost:7082/api/Activity/GetByuserId?userId=';
const URL_DELETE_ACTIVITY = 'https://localhost:7082/api/Activity/Delete?id=';
const URL_CREATE_ACTIVITY = 'https://localhost:7082/api/Activity/Create';

const activityTypes = [
    'Walking',
    'Running',
    'Swimming',
    "Stratching",
    "Aerobic",
    "Football",
    "Cycling",
    "Flexibility",
    "Bike riding"
];

const UserActivities = ({ userId, handleClose }) => {
    const { t } = useTranslation();
    const [activities, setActivities] = useState([]);
    const [showAddModal, setShowAddModal] = useState(false);
    const [newActivity, setNewActivity] = useState({
        type: '',
        duration: '',
        distance: '',
        calories: '',
    });

    useEffect(() => {
        getActivities();
    }, []);

    const getActivities = async () => {
        try {
            console.log(userId);
            const full_get_url = URL_GET_ACTIVITIES+userId;
            console.log(full_get_url);
            const response = await axios.get(full_get_url);
            console.log(response.data);
            setActivities(response.data);
        } catch (error) {
            console.error('Error fetching activities:', error);
        }
    };

    const handleDelete = async (id) => {
        try {
            await axios.delete(`${URL_DELETE_ACTIVITY}${id}`);
            await getActivities();
            // Handle success and show appropriate message to the user
        } catch (error) {
            console.error('Error deleting activity:', error);
            // Handle error and show appropriate message to the user
        }
    };

    const handleAddActivity = async (e) => {
        e.preventDefault();

        try {
            const body = {
                userId: userId,
                type: newActivity.type,
                duration: newActivity.duration,
                calories: newActivity.calories,
            }

            console.log(body);

            const response = await axios.post(URL_CREATE_ACTIVITY, body);
            console.log(response);
            getActivities();
            setNewActivity({
                type: '',
                duration: '',
                distance: '',
                calories: '',
            });
            setShowAddModal(false);
            // Handle success and show appropriate message to the user
        } catch (error) {
            console.error('Error adding activity:', error);
            // Handle error and show appropriate message to the user
        }
    };

    const handleModalShow = () => {
        setShowAddModal(true);
    };

    const handleModalClose = () => {
        setShowAddModal(false);
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewActivity((prevActivity) => ({ ...prevActivity, [name]: value }));
    };

    const handleCloseActivities = () => {
        handleClose();
    }

    return (
        <Card className='mt-3 mb-3 p-3 bg-warning border rounded border-2 border-dark'>
            <div>
                <h1>{t('userActivities.title')}</h1>

                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>{t('userActivities.type')}</th>
                            <th>{t('userActivities.duration')}</th>
                            <th>{t('userActivities.distance')}</th>
                            <th>{t('userActivities.calories')}</th>
                            <th>{t('userActivities.actions')}</th>
                        </tr>
                    </thead>
                    <tbody>
                        {activities.length > 0 ? (
                            activities.reverse().slice(0,7).map((activity) => (
                                <tr key={activity.id}>
                                    <td>{activity.type}</td>
                                    <td>{activity.duration}</td>
                                    <td>{activity.distance}</td>
                                    <td>{activity.calories}</td>
                                    <td>
                                        <Button variant="danger" onClick={() => handleDelete(activity.id)}>
                                            {t('userActivities.deleteButton')}
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={5}>{t('userActivities.noActivities')}</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
                <div className="d-flex justify-content-center">
                    <Button className='m-2 p-5 pt-3 pb-3' variant="primary" onClick={handleModalShow}>
                        {t('userActivities.addButton')}
                    </Button>
                    <Button className='m-2 p-5 pt-3 pb-3 text-warning' variant="secondary" onClick={() => { handleCloseActivities() }}>
                        {t('userActivities.close')}
                    </Button>
                </div>
                <Modal show={showAddModal} onHide={handleModalClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>{t('userActivities.addActivity')}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form onSubmit={handleAddActivity}>
                            <Form.Group controlId="formActivityType">
                                <Form.Label>{t('userActivities.type')}</Form.Label>
                                <Form.Control
                                    as="select"
                                    name="type"
                                    value={newActivity.type}
                                    onChange={handleInputChange}
                                >
                                    <option value="">{t('userActivities.selectType')}</option>
                                    {activityTypes.map((type) => (
                                        <option key={type} value={type}>
                                            {type}
                                        </option>
                                    ))}
                                </Form.Control>
                            </Form.Group>

                            <Form.Group className = "mt-2" controlId="formDuration">
                                <Form.Label>{t('userActivities.duration')}</Form.Label>
                                <Form.Control
                                    type="number"
                                    name="duration"
                                    value={newActivity.duration}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>

                            <Form.Group className = "mt-2" controlId="formDistance">
                                <Form.Label>{t('userActivities.distance')}</Form.Label>
                                <Form.Control
                                    type="number"
                                    name="distance"
                                    value={newActivity.distance}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>

                            <Form.Group className = "mt-2" controlId="formCalories">
                                <Form.Label>{t('userActivities.calories')}</Form.Label>
                                <Form.Control
                                    type="number"
                                    name="calories"
                                    value={newActivity.calories}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Button  className = "mt-2 p-5 pt-3 pb-3"variant="primary" type="submit">
                                {t('userActivities.addButton')}
                            </Button>
                        </Form>
                    </Modal.Body>
                </Modal>
            </div>
        </Card>
    );
};

export default UserActivities;
