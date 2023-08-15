import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Modal, Form } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import ManagerHeader from '../../components/headers/managerHeader/ManagerHeader';

const URL_GET_AGENTS_BY_COMPANY = 'https://localhost:7082/api/Agent/GetAgentsByCompany?companyId=';
const URL_CREATE_AGENT = "https://localhost:7082/api/Agent/Create";
const URL_UPDATE_AGENT = "https://localhost:7082/api/Agent/Update";
const URL_DELETE_AGENT = "https://localhost:7082/api/Agent/Delete?id=";

const ManagerMainPage = () => {
    const { t } = useTranslation();
    const [agents, setAgents] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [newAgent, setNewAgent] = useState({
        firstName: '',
        secondName: '',
        phoneNumber: '',
        emailAddress: '',
        raiting: 0,
        companyId: localStorage.getItem('managerCompanyId'),
    });
    const [updatingAgentId, setUpdatingAgentId] = useState(null);

    const handleCloseModal = () => {
        setShowModal(false);
        setUpdatingAgentId(null);
        resetNewAgentFields();
    };

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleInputChange = (e) => {
        console.log(e.target.name+":"+e.target.value);

        setNewAgent((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };

    const handleCreateAgent = async () => {
        try {
            await axios.post(URL_CREATE_AGENT, newAgent);
            getAgents();
            handleCloseModal();
        } catch (error) {
            console.error('Error creating agent:', error);
        }
    };

    const handleUpdateAgent = async () => {
        try {
            const updatedAgent = { 
                id: updatingAgentId,
                firstName: newAgent.firstName,
                secondName: newAgent.secondName,
                phoneNumber: newAgent.phoneNumber,
                emailAddress: newAgent.emailAddress,
                raiting: newAgent.raiting,
                companyId: newAgent.companyId 
            };
            console.log(updatedAgent);
            await axios.put(URL_UPDATE_AGENT, updatedAgent);
            getAgents();
            handleCloseModal();
        } catch (error) {
            console.error('Error updating agent:', error);
        }
    };

    const handleDeleteAgent = async (agentId) => {
        try {
            await axios.delete(URL_DELETE_AGENT + agentId);
            getAgents();
        } catch (error) {
            console.error('Error deleting agent:', error);
        }
    };

    const handleEditAgent = (agent) => {
        setNewAgent(agent);
        setUpdatingAgentId(agent.id);
        handleShowModal();
    };

    const resetNewAgentFields = () => {
        setNewAgent({
            firstName: '',
            lastName: '',
            phoneNumber: '',
            emailAddress: '',
            raiting: 0,
            companyId: localStorage.getItem('managerCompanyId'),
        });
    };

    const getAgents = async () => {
        const companyId = localStorage.getItem('managerCompanyId');
        if (companyId) {
            try {
                const response = await axios.get(`${URL_GET_AGENTS_BY_COMPANY}${companyId}`);
                setAgents(response.data);
                console.log(response.data);
            } catch (error) {
                console.error('Error fetching agents:', error);
            }
        }
    };

    useEffect(() => {
        getAgents();
    }, []);

    return (
        <>
            <ManagerHeader />
            <Container>
                <div className="d-grid gap-2">
                    <Button variant="primary" className="my-3" size="lg" onClick={handleShowModal}>
                        {t('managerMainPage.createNewAgent')}
                    </Button>
                </div>
                <h1 className="my-2">{t('managerMainPage.agentsList')}</h1>
                {agents.map((agent) => (
                    <Card key={agent.id} className="my-3 bg-info">
                        <Card.Body>
                            <Card.Title>{`${agent.firstName} ${agent.secondName}`}</Card.Title>
                            <Card.Text>{`${t('managerMainPage.phoneNumber')}: ${agent.phoneNumber}`}</Card.Text>
                            <Card.Text>{`${t('managerMainPage.emailAddress')}: ${agent.emailAddress}`}</Card.Text>
                            <Card.Text>{`${t('managerMainPage.rating')}: ${agent.raiting}`}</Card.Text>
                            <Card.Text>{`${t('managerMainPage.companyName')}: ${agent.company.companyName}`}</Card.Text>
                            <div className="d-flex justify-content-around">
                                <Button className='p-5 pt-3 pb-3 m-2' variant="danger" onClick={() => handleDeleteAgent(agent.id)}>
                                    {t('managerMainPage.delete')}
                                </Button>
                                <Button className='p-5 pt-3 pb-3 m-2' variant="primary" onClick={() => handleEditAgent(agent)}>
                                    {t('managerMainPage.update')}
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                ))}

                <Modal show={showModal} onHide={handleCloseModal}>
                    <Modal.Header closeButton>
                        <Modal.Title>{updatingAgentId ? t('managerMainPage.updateAgent') : t('managerMainPage.createNewAgent')}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group controlId="formFirstName">
                                <Form.Label>{t('managerMainPage.firstName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="firstName"
                                    value={newAgent.firstName}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formLastName">
                                <Form.Label>{t('managerMainPage.lastName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="secondName"
                                    value={newAgent.secondName}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formPhoneNumber">
                                <Form.Label>{t('managerMainPage.phoneNumber')}</Form.Label>
                                <Form.Control
                                    type="tel"
                                    name="phoneNumber"
                                    value={newAgent.phoneNumber}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formEmailAddress">
                                <Form.Label>{t('managerMainPage.emailAddress')}</Form.Label>
                                <Form.Control
                                    type="email"
                                    name="emailAddress"
                                    value={newAgent.emailAddress}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formRating">
                                <Form.Label>{t('managerMainPage.rating')}</Form.Label>
                                <Form.Control
                                    type="number"
                                    name="raiting"
                                    min={0}
                                    max={10}
                                    value={newAgent.raiting}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleCloseModal}>
                            {t('managerMainPage.cancel')}
                        </Button>
                        {updatingAgentId ? (
                            <Button variant="primary" onClick={handleUpdateAgent}>
                                {t('managerMainPage.update')}
                            </Button>
                        ) : (
                            <Button variant="primary" onClick={handleCreateAgent}>
                                {t('managerMainPage.create')}
                            </Button>
                        )}
                    </Modal.Footer>
                </Modal>
            </Container>
            <Footer />
        </>
    );
};

export default ManagerMainPage;