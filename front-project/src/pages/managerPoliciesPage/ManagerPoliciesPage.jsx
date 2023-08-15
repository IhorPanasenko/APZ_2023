import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Modal, Form } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import ManagerHeader from '../../components/headers/managerHeader/ManagerHeader';

const ManagerPoliciesPage = () => {
    const { t } = useTranslation();
    const [policies, setPolicies] = useState([]);
    const [categories, setCategories] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [policyData, setPolicyData] = useState({
        name: '',
        description: '',
        coverageAmount: '',
        price: '',
        timePeriod: '',
        companyId: localStorage.getItem('managerCompanyId'),
        categoryId: ''
    });

    const [policyUpdateData, setPolicyUpdateData] = useState({
        id: '',
        name: '',
        description: '',
        coverageAmount: '',
        price: '',
        timePeriod: '',
        companyId: localStorage.getItem('managerCompanyId'),
        categoryId: ''
    });
    const [isUpdateForm, setIsUpdateForm] = useState(false);

    const URL_GET_POLICIES = `https://localhost:7082/api/Policy/GetAll?companyId=${policyData.companyId}`;
    const URL_GET_CATEGORIES = 'https://localhost:7082/api/Category/GetAll';
    const URL_CREATE_POLICY = 'https://localhost:7082/api/Policy/Create';
    const URL_UPDATE_POLICY = 'https://localhost:7082/api/Policy/Update';
    const URL_DELETE_POLICY = 'https://localhost:7082/api/Policy/Delete?id=';

    const handleCloseModal = () => {
        setIsUpdateForm(false);
        setShowModal(false);
    };

    const handleShowModal = () => {
        setIsUpdateForm(false);
        setShowModal(true);
    };

    const handleUpdateClick = (policy) => {
        console.log(policy);
        setIsUpdateForm(true);
        setPolicyUpdateData({
            id: policy.id,
            name: policy.name,
            description: policy.description,
            coverageAmount: policy.coverageAmount,
            price: policy.price,
            timePeriod: policy.timePeriod,
            categoryId: policy.categoryId
        });
        console.log(policyUpdateData);
        setShowModal(true);
    };

    const handleInputChange = (e) => {
        setPolicyData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };

    const handleUpdateInputChange = (e) => {
        setPolicyUpdateData((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };


    const handleCreatePolicy = async () => {
        try {
            setIsUpdateForm(false);
            
            console.log(policyData);
            const response = await axios.post(URL_CREATE_POLICY, policyData);
            console.log(response.data);
            getPolicies();
            handleCloseModal();
        } catch (error) {
            console.error('Error creating policy:', error);
        }
    };

    const handleDeletePolicy = async (policyId) => {
        try {
            await axios.delete(URL_DELETE_POLICY + policyId);
            getPolicies();
        } catch (error) {
            console.error('Error deleting policy:', error);
        }
    };

    const handleUpdatePolicy = async () => {
        try {
            console.log(policyUpdateData);
            const response = await axios.put(URL_UPDATE_POLICY, policyUpdateData);
            console.log(response.data);
            getPolicies();
            setIsUpdateForm(false);
            handleCloseModal();
        } catch (error) {
            console.error('Error updating policy:', error);
        }
    };

    const getPolicies = async () => {
        try {
            const response = await axios.get(URL_GET_POLICIES);
            setPolicies(response.data);
        } catch (error) {
            console.error('Error getting policies:', error);
        }
    };

    const getCategories = async () => {
        try {
            const response = await axios.get(URL_GET_CATEGORIES);
            setCategories(response.data);
        } catch (error) {
            console.error('Error getting categories:', error);
        }
    };

    useEffect(() => {
        getPolicies();
        getCategories();
    }, []);

    return (
        <>
            <ManagerHeader />
            <Container className="mt-4">
                <h2>{t('managerPoliciesPage.title')}</h2>
                <div className="d-grid gap-2">
                    <Button variant="primary" size="lg" onClick={handleShowModal} className="mb-3 p-4 pt-3 pb-3">
                        {t('managerPoliciesPage.addPolicy')}
                    </Button>
                </div>
                {policies.reverse().map((policy) => (
                    <Card key={policy.id} className="mb-3 bg-info">
                        <Card.Body>
                            <Card.Title>{policy.name}</Card.Title>
                            <Card.Text>{t("managerPoliciesCard.description")}: {policy.description}</Card.Text>
                            <Card.Text>{t("managerPoliciesCard.coverageAmount")}: {policy.coverageAmount}</Card.Text>
                            <Card.Text>{t("managerPoliciesCard.price")}: {policy.price}</Card.Text>
                            <Card.Text>{t("managerPoliciesCard.timePeriod")}: {policy.timePeriod}</Card.Text>
                            <Card.Text>{t("managerPoliciesCard.company")}: {policy.company.companyName}</Card.Text>
                            <Card.Text>{t("managerPoliciesCard.address")}: {policy.company.address}</Card.Text>
                            <Card.Text>{t("managerPoliciesCard.category")}: {policy.category.categoryName}</Card.Text>

                            <div className="d-flex justify-content-around">
                                <Button
                                    variant="danger"
                                    onClick={() => handleDeletePolicy(policy.id)}
                                    className="mr-2 p-4 pt-2 pb-2"
                                >
                                    {t('managerPoliciesPage.delete')}
                                </Button>
                                <Button
                                    variant="primary"
                                    onClick={() => { handleUpdateClick(policy) }}
                                    className="mr-2 p-4 pt-2 pb-2"
                                >
                                    {t('managerPoliciesPage.update')}
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                ))}
            </Container>

            <Modal show={showModal} onHide={handleCloseModal}>
                <Modal.Header closeButton>
                    <Modal.Title>{t('managerPoliciesPage.modalTitle')}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group controlId="formName">
                            <Form.Label>{t('managerPoliciesPage.name')}</Form.Label>
                            {isUpdateForm ? (
                                <Form.Control
                                    type="text"
                                    name="name"
                                    value={policyUpdateData.name}
                                    onChange={handleUpdateInputChange}
                                />
                            ) : (
                                <Form.Control
                                    type="text"
                                    name="name"
                                    value={policyData.name}
                                    onChange={handleInputChange}
                                />
                            )}

                        </Form.Group>

                        <Form.Group controlId="formDescription">
                            <Form.Label>{t('managerPoliciesPage.description')}</Form.Label>
                            {isUpdateForm ? (
                                <Form.Control
                                    type="text"
                                    name="description"
                                    value={policyUpdateData.description}
                                    onChange={handleUpdateInputChange}
                                />
                            ) : (
                                <Form.Control
                                    type="text"
                                    name="description"
                                    value={policyData.description}
                                    onChange={handleInputChange}
                                />
                            )}

                        </Form.Group>

                        <Form.Group controlId="formCoverageAmount">
                            <Form.Label>{t('managerPoliciesPage.coverageAmount')}</Form.Label>
                            {isUpdateForm ? (
                                <Form.Control
                                    type="number"
                                    name="coverageAmount"
                                    value={policyUpdateData.coverageAmount}
                                    onChange={handleUpdateInputChange}
                                />
                            ) : (
                                <Form.Control
                                    type="number"
                                    name="coverageAmount"
                                    value={policyData.coverageAmount}
                                    onChange={handleInputChange}
                                />
                            )}

                        </Form.Group>

                        <Form.Group controlId="formPrice">
                            <Form.Label>{t('managerPoliciesPage.price')}</Form.Label>
                            {isUpdateForm ? (
                                <Form.Control
                                    type="number"
                                    name="price"
                                    value={policyUpdateData.price}
                                    onChange={handleUpdateInputChange}
                                />
                            ) : (
                                <Form.Control
                                    type="number"
                                    name="price"
                                    value={policyData.price}
                                    onChange={handleInputChange}
                                />
                            )}

                        </Form.Group>

                        <Form.Group controlId="formTimePeriod">
                            <Form.Label>{t('managerPoliciesPage.timePeriod')}</Form.Label>
                            {isUpdateForm ? (
                                <Form.Control
                                    type="number"
                                    name="timePeriod"
                                    value={policyUpdateData.timePeriod}
                                    onChange={handleUpdateInputChange}
                                />
                            ) : (
                                <Form.Control
                                    type="number"
                                    name="timePeriod"
                                    value={policyData.timePeriod}
                                    onChange={handleInputChange}
                                />
                            )}

                        </Form.Group>

                        <Form.Group controlId="formCategory">
                            <Form.Label>{t('managerPoliciesPage.category')}</Form.Label>
                            {isUpdateForm ? (
                                <Form.Control
                                    as="select"
                                    name="categoryId"
                                    value={policyUpdateData.categoryId}
                                    onChange={handleUpdateInputChange}
                                >
                                    <option value="">{t('managerPoliciesPage.selectCategory')}</option>
                                    {categories.map((category) => (
                                        <option key={category.id} value={category.id}>
                                            {category.categoryName}
                                        </option>
                                    ))}
                                </Form.Control>
                            ) : (
                                <Form.Control
                                    as="select"
                                    name="categoryId"
                                    value={policyData.categoryId}
                                    onChange={handleInputChange}
                                >
                                    <option value="">{t('managerPoliciesPage.selectCategory')}</option>
                                    {categories.map((category) => (
                                        <option key={category.id} value={category.id}>
                                            {category.categoryName}
                                        </option>
                                    ))}
                                </Form.Control>
                            )}

                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleCloseModal}>
                        {t('managerPoliciesPage.cancel')}
                    </Button>
                    {isUpdateForm ? (
                        <Button variant="primary" onClick={()=>{handleUpdatePolicy(policyUpdateData.id)}}>
                            {t('managerPoliciesPage.update')}
                        </Button>
                    ) : (
                        <Button variant="primary" onClick={handleCreatePolicy}>
                            {t('managerPoliciesPage.save')}
                        </Button>
                    )}

                </Modal.Footer>
            </Modal>

            <Footer />
        </>
    );
};

export default ManagerPoliciesPage;