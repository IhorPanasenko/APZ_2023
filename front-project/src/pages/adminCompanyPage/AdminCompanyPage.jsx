import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Modal, Form, FormControl } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import AdminHeader from '../../components/headers/adminHeader/AdminHeader';

const URL_GET_ALL_COMPANIES = 'https://localhost:7082/api/Company/GetAll';
const URL_CREATE_COMPANY = 'https://localhost:7082/api/Company/Create';
const URL_UPDATE_COMPANY = 'https://localhost:7082/api/Company/Update';
const URL_DELETE_COMPANY = 'https://localhost:7082/api/Company/Delete?id=';
const URL_SEARCH_COMPANY_BY_NAME = 'https://localhost:7082/api/Company/SearchByName?searchString=';


const AdminCompanyPage = () => {
    const { t } = useTranslation();
    const [companies, setCompanies] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [searchString, setSearchString] = useState('');
    const [newCompany, setNewCompany] = useState({
        companyName: '',
        description: '',
        address: '',
        phoneNumber: '',
        emailAddress: '',
        websiteAddress: '',
        maxDiscountPercentage: 0,
    });
    const [updatingCompanyId, setUpdatingCompanyId] = useState(null);

    const handleCloseModal = () => {
        setShowModal(false);
        setUpdatingCompanyId(null);
        resetNewCompanyFields();
    };

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleInputChange = (e) => {
        setNewCompany((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
        }));
    };

    const handleCreateCompany = async () => {
        try {
            await axios.post(URL_CREATE_COMPANY, newCompany);
            getCompanies();
            handleCloseModal();
        } catch (error) {
            console.error('Error creating company:', error);
        }
    };

    const handleUpdateCompany = async () => {
        try {
            const updatedCompany = {
                id: updatingCompanyId,
                companyName: newCompany.companyName,
                description: newCompany.description,
                address: newCompany.address,
                phoneNumber: newCompany.phoneNumber,
                emailAddress: newCompany.emailAddress,
                websiteAddress: newCompany.websiteAddress,
                maxDiscountPercentage: newCompany.maxDiscountPercentage,
            };
            await axios.put(URL_UPDATE_COMPANY, updatedCompany);
            getCompanies();
            handleCloseModal();
        } catch (error) {
            console.error('Error updating company:', error);
        }
    };

    const handleDeleteCompany = async (companyId) => {
        try {
            await axios.delete(URL_DELETE_COMPANY + companyId);
            getCompanies();
        } catch (error) {
            console.error('Error deleting company:', error);
        }
    };

    const handleEditCompany = (company) => {
        setNewCompany(company);
        setUpdatingCompanyId(company.id);
        handleShowModal();
    };

    const handleSearchCompany = async () => {
        try {
            if(searchString == '' || searchString == null){
                getCompanies();
                return;
            }
            const response = await axios.get(URL_SEARCH_COMPANY_BY_NAME + searchString);
            setCompanies(response.data);
        } catch (error) {
            console.error('Error searching companies:', error);
        }
    };

    const resetNewCompanyFields = () => {
        setNewCompany({
            companyName: '',
            description: '',
            address: '',
            phoneNumber: '',
            emailAddress: '',
            websiteAddress: '',
            maxDiscountPercentage: 0,
        });
    };

    const getCompanies = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_COMPANIES);
            setCompanies(response.data);
        } catch (error) {
            console.error('Error fetching companies:', error);
        }
    };

    useEffect(() => {
        getCompanies();
    }, []);

    return (
        <>
            <AdminHeader />
            <Container>
                <div className=" mt-3 mb-3 d-flex hustify-content-between">
                    <FormControl
                        type="text"
                        placeholder={t('adminCompanyPage.searchPlaceholder')}
                        value={searchString}
                        onChange={(e) => setSearchString(e.target.value)}
                    />
                    <Button className='m-2 mt-0 mb-0 ' variant="primary" onClick={handleSearchCompany}>{t('adminCompanyPage.search')}</Button>
                </div>
                <div className="d-grid gap-2">
                    <Button variant="primary" className="my-3" size="lg" onClick={handleShowModal}>
                        {t('adminCompanyPage.createCompany')}
                    </Button>
                </div>
                <h1 className="my-2">{t('adminCompanyPage.companiesList')}</h1>
                {companies.map((company) => (
                    <Card key={company.id} className="my-3 bg-info">
                        <Card.Body>
                            <Card.Title>{company.companyName}</Card.Title>
                            <Card.Text>{`${t('adminCompanyPage.description')}: ${company.description}`}</Card.Text>
                            <Card.Text>{`${t('adminCompanyPage.address')}: ${company.address}`}</Card.Text>
                            <Card.Text>{`${t('adminCompanyPage.phoneNumber')}: ${company.phoneNumber}`}</Card.Text>
                            <Card.Text>{`${t('adminCompanyPage.emailAddress')}: ${company.emailAddress}`}</Card.Text>
                            <Card.Text>{`${t('adminCompanyPage.websiteAddress')}: ${company.websiteAddress}`}</Card.Text>
                            <Card.Text>{`${t('adminCompanyPage.maxDiscountPercentage')}: ${company.maxDiscountPercentage}`}</Card.Text>
                            <div className="d-flex justify-content-around">
                                <Button className="p-5 pt-3 pb-3 m-2" variant="danger" onClick={() => handleDeleteCompany(company.id)}>
                                    {t('adminCompanyPage.delete')}
                                </Button>
                                <Button className="p-5 pt-3 pb-3 m-2" variant="primary" onClick={() => handleEditCompany(company)}>
                                    {t('adminCompanyPage.update')}
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                ))}

                <Modal show={showModal} onHide={handleCloseModal}>
                    <Modal.Header closeButton>
                        <Modal.Title>{updatingCompanyId ? t('adminCompanyPage.updateCompany') : t('adminCompanyPage.createCompany')}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group controlId="formCompanyName">
                                <Form.Label>{t('adminCompanyPage.companyName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="companyName"
                                    value={newCompany.companyName}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formDescription">
                                <Form.Label>{t('adminCompanyPage.description')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="description"
                                    value={newCompany.description}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formAddress">
                                <Form.Label>{t('adminCompanyPage.address')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="address"
                                    value={newCompany.address}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formPhoneNumber">
                                <Form.Label>{t('adminCompanyPage.phoneNumber')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="phoneNumber"
                                    value={newCompany.phoneNumber}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formEmailAddress">
                                <Form.Label>{t('adminCompanyPage.emailAddress')}</Form.Label>
                                <Form.Control
                                    type="email"
                                    name="emailAddress"
                                    value={newCompany.emailAddress}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formWebsiteAddress">
                                <Form.Label>{t('adminCompanyPage.websiteAddress')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="websiteAddress"
                                    value={newCompany.websiteAddress}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                            <Form.Group controlId="formMaxDiscountPercentage">
                                <Form.Label>{t('adminCompanyPage.maxDiscountPercentage')}</Form.Label>
                                <Form.Control
                                    type="number"
                                    name="maxDiscountPercentage"
                                    value={newCompany.maxDiscountPercentage}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleCloseModal}>
                            {t('adminCompanyPage.cancel')}
                        </Button>
                        <Button variant="primary" onClick={updatingCompanyId ? handleUpdateCompany : handleCreateCompany}>
                            {updatingCompanyId ? t('adminCompanyPage.update') : t('adminCompanyPage.create')}
                        </Button>
                    </Modal.Footer>
                </Modal>
            </Container>
            <Footer />
        </>
    );
};

export default AdminCompanyPage;