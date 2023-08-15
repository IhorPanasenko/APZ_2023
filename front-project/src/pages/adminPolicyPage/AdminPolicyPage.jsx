import React, { useState, useEffect } from 'react';
import { Container, Card, Button, Modal, Form } from 'react-bootstrap';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import AdminHeader from '../../components/headers/adminHeader/AdminHeader';
import Footer from '../../components/footer/Footer';

const AdminPolicyPage = () => {
    const { t } = useTranslation();

    const URL_GET_ALL_POLICIES = 'https://localhost:7082/api/Policy/GetAll';
    const URL_CREATE_POLICY = 'https://localhost:7082/api/Policy/Create';
    const URL_DELETE_POLICY = 'https://localhost:7082/api/Policy/Delete?id=';
    const URL_UPDATE_POLICY = 'https://localhost:7082/api/Policy/Update';
    const URL_GET_ALL_COMPANIES = 'https://localhost:7082/api/Company/GetAll';
    const URL_GET_ALL_CATEGORIES = 'https://localhost:7082/api/Category/GetAll';

    const [policies, setPolicies] = useState([]);
    const [companies, setCompanies] = useState([]);
    const [categories, setCategories] = useState([]);
    const [newPolicy, setNewPolicy] = useState({
        name: '',
        description: '',
        coverageAmount: '',
        price: '',
        timePeriod: '',
        companyId: '',
        categoryId: '',
    });
    const [showModal, setShowModal] = useState(false);
    const [updatingPolicyId, setUpdatingPolicyId] = useState(null);

    const handleCloseModal = () => {
        setShowModal(false);
        setNewPolicy({
            name: '',
            description: '',
            coverageAmount: '',
            price: '',
            timePeriod: '',
            companyId: '',
            categoryId: '',
        });
        setUpdatingPolicyId(null);
    };

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleInputChange = (e) => {
        setNewPolicy({ ...newPolicy, [e.target.name]: e.target.value });
    };

    const handleCreatePolicy = async () => {
        try {
            await axios.post(URL_CREATE_POLICY, newPolicy);
            handleCloseModal();
            getPolicies();
        } catch (error) {
            console.error('Error creating policy:', error);
        }
    };

    const handleEditPolicy = (policy) => {
        setNewPolicy({ ...policy });
        setUpdatingPolicyId(policy.id);
        handleShowModal();
    };

    const handleUpdatePolicy = async () => {
        try {
            const body = {
                id: updatingPolicyId,
                name: newPolicy.name,
                descroption: newPolicy.description,
                coverageAmount: newPolicy.coverageAmount,
                price: newPolicy.price,
                timePeriod: newPolicy.timePeriod,
                companyId: newPolicy.companyId,
                categoryId: newPolicy.categoryId
            }
            await axios.put(URL_UPDATE_POLICY, body);
            handleCloseModal();
            getPolicies();
        } catch (error) {
            console.error('Error updating policy:', error);
        }
    };

    const handleDeletePolicy = async (policyId) => {
        try {
            await axios.delete(`${URL_DELETE_POLICY}${policyId}`);
            getPolicies();
        } catch (error) {
            console.error('Error deleting policy:', error);
        }
    };

    const getPolicies = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_POLICIES);
            setPolicies(response.data);
        } catch (error) {
            console.error('Error getting policies:', error);
        }
    };

    const getCompanies = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_COMPANIES);
            setCompanies(response.data);
        } catch (error) {
            console.error('Error getting companies:', error);
        }
    };

    const getCategories = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_CATEGORIES);
            setCategories(response.data);
        } catch (error) {
            console.error('Error getting categories:', error);
        }
    };

    useEffect(() => {
        getPolicies();
        getCompanies();
        getCategories();
    }, []);

    return (
        <>
            <AdminHeader />
            <Container>
                <h1>{t('adminPoliciesPage.title')}</h1>
                <div className="d-grid gap-2">
                    <Button variant="primary" className="my-3" size="lg"  onClick={handleShowModal}>
                    {t('adminPoliciesPage.createPolicy')}
                    </Button>
                </div>
                <h2 className='pt-3'>{t('adminPoliciesPage.policyList')}</h2>
                {policies.map((policy) => (
                    <Card className="mt-4 mb-4 bg-info border border-success border-2 rounded" key={policy.id}>
                        <Card.Header>
                            <Card.Title><strong>{policy.name}</strong></Card.Title>
                        </Card.Header>
                        <Card.Body>
                            <Card.Text>{t('adminPoliciesPage.description')}: {policy.description}</Card.Text>
                            <Card.Text>{t('adminPoliciesPage.coverageAmount')}: {policy.coverageAmount}</Card.Text>
                            <Card.Text>{t('adminPoliciesPage.price')}: {policy.price}</Card.Text>
                            <Card.Text>{t('adminPoliciesPage.timePeriod')}: {policy.timePeriod}</Card.Text>
                            <Card.Text>{t('adminPoliciesPage.company')}: {policy.company.companyName}</Card.Text>
                            <Card.Text>{t('adminPoliciesPage.category')}: {policy.category.categoryName}</Card.Text>
                            <Card.Text>
                                <div className="d-flex justify-content-around">
                                    <Button className='p-5 pt-2 pb-2 m-1' variant="warning" onClick={() => handleEditPolicy(policy)}>
                                        {t('adminPoliciesPage.edit')}
                                    </Button>
                                    <Button
                                        variant="danger"
                                        className="p-5 pt-2 pb-2 m-1"
                                        onClick={() => handleDeletePolicy(policy.id)}
                                    >
                                        {t('adminPoliciesPage.delete')}
                                    </Button>
                                </div>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                ))}
            </Container >
            <Modal show={showModal} onHide={handleCloseModal}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        {updatingPolicyId ? t('adminPoliciesPage.updatePolicy') : t('adminPoliciesPage.createPolicy')}
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="formName">
                            <Form.Label>{t('adminPoliciesPage.name')}</Form.Label>
                            <Form.Control
                                type="text"
                                name="name"
                                value={newPolicy.name}
                                onChange={handleInputChange}
                            />
                        </Form.Group>
                        <Form.Group controlId="formDescription">
                            <Form.Label>{t('adminPoliciesPage.description')}</Form.Label>
                            <Form.Control
                                as="textarea"
                                name="description"
                                value={newPolicy.description}
                                onChange={handleInputChange}
                            />
                        </Form.Group>
                        <Form.Group controlId="formCoverageAmount">
                            <Form.Label>{t('adminPoliciesPage.coverageAmount')}</Form.Label>
                            <Form.Control
                                type="text"
                                name="coverageAmount"
                                value={newPolicy.coverageAmount}
                                onChange={handleInputChange}
                            />
                        </Form.Group>
                        <Form.Group controlId="formPrice">
                            <Form.Label>{t('adminPoliciesPage.price')}</Form.Label>
                            <Form.Control
                                type="text"
                                name="price"
                                value={newPolicy.price}
                                onChange={handleInputChange}
                            />
                        </Form.Group>
                        <Form.Group controlId="formTimePeriod">
                            <Form.Label>{t('adminPoliciesPage.timePeriod')}</Form.Label>
                            <Form.Control
                                type="text"
                                name="timePeriod"
                                value={newPolicy.timePeriod}
                                onChange={handleInputChange}
                            />
                        </Form.Group>
                        <Form.Group controlId="formCompany">
                            <Form.Label>{t('adminPoliciesPage.company')}</Form.Label>
                            <Form.Control
                                as="select"
                                name="companyId"
                                value={newPolicy.companyId}
                                onChange={handleInputChange}
                            >
                                <option value="">{t('adminPoliciesPage.selectCompany')}</option>
                                {companies.map((company) => (
                                    <option key={company.id} value={company.id}>
                                        {company.companyName}
                                    </option>
                                ))}
                            </Form.Control>
                        </Form.Group>
                        <Form.Group controlId="formCategory">
                            <Form.Label>{t('adminPoliciesPage.category')}</Form.Label>
                            <Form.Control
                                as="select"
                                name="categoryId"
                                value={newPolicy.categoryId}
                                onChange={handleInputChange}
                            >
                                <option value="">{t('adminPoliciesPage.selectCategory')}</option>
                                {categories.map((category) => (
                                    <option key={category.id} value={category.id}>
                                        {category.categoryName}
                                    </option>
                                ))}
                            </Form.Control>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleCloseModal}>
                        {t('adminPoliciesPage.cancel')}
                    </Button>
                    {updatingPolicyId ? (
                        <Button variant="primary" onClick={handleUpdatePolicy}>
                            {t('adminPoliciesPage.update')}
                        </Button>
                    ) : (
                        <Button variant="primary" onClick={handleCreatePolicy}>
                            {t('adminPoliciesPage.create')}
                        </Button>
                    )}
                </Modal.Footer>
            </Modal>
            <Footer />
        </>
    );
};

export default AdminPolicyPage;