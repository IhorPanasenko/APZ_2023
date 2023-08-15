import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Modal, Form } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import ManagerHeader from '../../components/headers/managerHeader/ManagerHeader';
import AdminHeader from '../../components/headers/adminHeader/AdminHeader';

const URL_GET_ALL_AGENTS = 'https://localhost:7082/api/Agent/GetAll';
const URL_CREATE_AGENT = "https://localhost:7082/api/Agent/Create";
const URL_UPDATE_AGENT = "https://localhost:7082/api/Agent/Update";
const URL_DELETE_AGENT = "https://localhost:7082/api/Agent/Delete?id=";
const URL_GET_COMPANIES = "https://localhost:7082/api/Company/GetAll"

const AdminAgentsPage = () => {
    const { t } = useTranslation();
    const [agents, setAgents] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [companies, setCompanies] = useState([]);
    const [newAgent, setNewAgent] = useState({
        firstName: '',
        secondName: '',
        phoneNumber: '',
        emailAddress: '',
        company: {},
        raiting: 0,
        companyId: ''
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
        console.log(e.target.name + ":" + e.target.value);

        setNewAgent((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };

    const handleCreateAgent = async () => {
        try {
            const createdAgent = {
                firstName: newAgent.firstName,
                secondName: newAgent.secondName,
                phoneNumber: newAgent.phoneNumber,
                emailAddress: newAgent.emailAddress,
                raiting: newAgent.raiting,
                companyId: newAgent.companyId
            };

            console.log(createdAgent);
            await axios.post(URL_CREATE_AGENT, createdAgent);
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
        console.log(agent);
        setNewAgent(agent);
        setNewAgent({
            company: agent.company,
            companyId: agent.companyId,
            emailAddress: agent.emailAddress,
            firstName: agent.firstName,
            id: agent.id,
            phoneNumber: agent.phoneNumber,
            secondName: agent.secondName,
            raiting: agent.raiting
        });
        console.log(newAgent);
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
            company: {},
            companyId: ''
        });
    };
    const getAgents = async () => {
       
            try {
                const response = await axios.get(URL_GET_ALL_AGENTS);
                setAgents(response.data);
                console.log(response.data);
            } catch (error) {
                console.error('Error fetching agents:', error);
            }
        
    };

    const getCompanies = async () => {

        try {
            const response = await axios.get(URL_GET_COMPANIES);
            setCompanies(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching agents:', error);
        }
    }

    useEffect(() => {
        getAgents();
        getCompanies();
    }, []);

    return (
        <>
            <AdminHeader />
            <Container>
                <div className="d-grid gap-2">
                    <Button variant="primary" className="my-3" size="lg" onClick={handleShowModal}>
                        {t('adminAgentsPage.createNewAgent')}
                    </Button>
                </div>
                <h1 className="my-2">{t('adminAgentsPage.agentsList')}</h1>
                {agents.map((agent) => (
                    <Card key={agent.id} className="my-3 bg-info">
                        <Card.Body>
                            <Card.Title>{`${agent.firstName} ${agent.secondName}`}</Card.Title>
                            <Card.Text>{`${t('adminAgentsPage.phoneNumber')}: ${agent.phoneNumber}`}</Card.Text>
                            <Card.Text>{`${t('adminAgentsPage.emailAddress')}: ${agent.emailAddress}`}</Card.Text>
                            <Card.Text>{`${t('adminAgentsPage.rating')}: ${agent.raiting}`}</Card.Text>
                            <Card.Text>{`${t('adminAgentsPage.companyName')}: ${agent.company.companyName}`}</Card.Text>
                            <div className="d-flex justify-content-around">
                                <Button className='p-5 pt-3 pb-3 m-2' variant="danger" onClick={() => handleDeleteAgent(agent.id)}>
                                    {t('adminAgentsPage.delete')}
                                </Button>
                                <Button className='p-5 pt-3 pb-3 m-2' variant="primary" onClick={() => handleEditAgent(agent)}>
                                    {t('adminAgentsPage.update')}
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                ))}

                <Modal show={showModal} onHide={handleCloseModal}>
                    <Modal.Header closeButton>
                        <Modal.Title>{updatingAgentId ? t('adminAgentsPage.updateAgent') : t('adminAgentsPage.createNewAgent')}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group controlId="formFirstName">
                                <Form.Label>{t('adminAgentsPage.firstName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="firstName"
                                    value={newAgent.firstName}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formLastName">
                                <Form.Label>{t('adminAgentsPage.lastName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="secondName"
                                    value={newAgent.secondName}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formPhoneNumber">
                                <Form.Label>{t('adminAgentsPage.phoneNumber')}</Form.Label>
                                <Form.Control
                                    type="tel"
                                    name="phoneNumber"
                                    value={newAgent.phoneNumber}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formEmailAddress">
                                <Form.Label>{t('adminAgentsPage.emailAddress')}</Form.Label>
                                <Form.Control
                                    type="email"
                                    name="emailAddress"
                                    value={newAgent.emailAddress}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formRating">
                                <Form.Label>{t('adminAgentsPage.rating')}</Form.Label>
                                <Form.Control
                                    type="number"
                                    name="raiting"
                                    min={0}
                                    max={10}
                                    value={newAgent.raiting}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId='formCompany'>
                                <Form.Label>{t("adminAgentsPage.company")}</Form.Label>
                                <Form.Control
                                    as={"select"}
                                    value={newAgent.company.companyName}
                                    onChange={(e) => setNewAgent({ ...newAgent, companyId: e.target.value })}
                                >
                                    {companies.map((company) => (
                                        <option key={company.id} value={company.id}>
                                            {company.companyName}
                                        </option>
                                    ))}
                                </Form.Control>
                            </Form.Group>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleCloseModal}>
                            {t('adminAgentsPage.cancel')}
                        </Button>
                        {updatingAgentId ? (
                            <Button variant="primary" onClick={handleUpdateAgent}>
                                {t('adminAgentsPage.update')}
                            </Button>
                        ) : (
                            <Button variant="primary" onClick={handleCreateAgent}>
                                {t('adminAgentsPage.create')}
                            </Button>
                        )}
                    </Modal.Footer>
                </Modal>
            </Container>
            <Footer />
        </>
    );
};
export default AdminAgentsPage