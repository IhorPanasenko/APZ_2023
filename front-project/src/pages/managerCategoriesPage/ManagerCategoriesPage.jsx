import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Modal, Form } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import ManagerHeader from '../../components/headers/managerHeader/ManagerHeader';

const ManagerCategoriesPage = () => {
    const { t } = useTranslation();
    const [categories, setCategories] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [newCategory, setNewCategory] = useState({
        categoryName: '',
    });
    const [selectedCategory, setSelectedCategory] = useState({
        id:"",
        categoryName:""
    });

    const URL_GET_CATEGORIES = 'https://localhost:7082/api/Category/GetAll';
    const URL_CREATE_CATEGORY = 'https://localhost:7082/api/Category/Create';
    const URL_DELETE_CATEGORY = 'https://localhost:7082/api/Category/Delete?id=';
    const URL_UPDATE_CATEGORY = 'https://localhost:7082/api/Category/Update';

    const handleCloseModal = () => {
        setShowModal(false);
        setSelectedCategory(null);
    };

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleInputChange = (e) => {
        console.log(e.target.name + ":" + e.target.value)
        if (selectedCategory) {
            setSelectedCategory({
                id: selectedCategory.id,
                categoryName: e.target.value
            })
        }
        else {
            setNewCategory((prevState) => ({
                ...prevState,
                [e.target.name]: e.target.value,
            }));
        }
    };

    const handleCreateCategory = async () => {
        try {
            console.log(newCategory);
            await axios.post(URL_CREATE_CATEGORY, newCategory);
            getCategories();
            handleCloseModal();
        } catch (error) {
            console.error('Error creating category:', error);
        }
    };

    const handleDeleteCategory = async (categoryId) => {
        try {
            await axios.delete(URL_DELETE_CATEGORY + categoryId);
            getCategories();
        } catch (error) {
            console.error('Error deleting category:', error);
        }
    };

    const handleUpdateCategory = async () => {
        try {
            console.log(selectedCategory);
            await axios.put(URL_UPDATE_CATEGORY, selectedCategory);
            getCategories();
            handleCloseModal();
        } catch (error) {
            console.error('Error updating category:', error);
        }
    };

    const getCategories = async () => {
        try {
            const response = await axios.get(URL_GET_CATEGORIES);
            setCategories(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    useEffect(() => {
        getCategories();
    }, []);

    const handleEditCategory = (category) => {
        console.log(category);
        setSelectedCategory(category);
        handleShowModal();
    };

    return (
        <>
            <ManagerHeader />
            <Container>
                <h1 className="my-3">{t('managerCategoriesPage.categoriesList')}</h1>
                {categories?.reverse().map((category) => (
                    <Card key={category.id} className="my-3 bg-info">
                        <Card.Body className='d-flex justify-content-around'>
                            <Card.Title className='d-flex flex-column justify-content-center'>{category.categoryName}</Card.Title>
                            <div className="d-flex justify-content-around">
                                <Button
                                    className="p-3 m-2"
                                    variant="danger"
                                    onClick={() => handleDeleteCategory(category.id)}
                                >
                                    {t('managerCategoriesPage.delete')}
                                </Button>
                                <Button
                                    className="p-3 m-2"
                                    variant="primary"
                                    onClick={() => handleEditCategory(category)}
                                >
                                    {t('managerCategoriesPage.update')}
                                </Button>
                            </div>
                        </Card.Body>
                    </Card>
                ))}
                <div className="d-grid gap-2">
                    <Button className="my-3" variant="primary" size="lg" onClick={handleShowModal}>
                        {t('managerCategoriesPage.createCategory')}
                    </Button>
                </div>

                <Modal show={showModal} onHide={handleCloseModal}>
                    <Modal.Header closeButton>
                        <Modal.Title>
                            {selectedCategory
                                ? t('managerCategoriesPage.updateCategory')
                                : t('managerCategoriesPage.createCategory')}
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group controlId="categoryName">
                                <Form.Label>{t('managerCategoriesPage.categoryName')}</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="categoryName"
                                    value={selectedCategory ? selectedCategory.categoryName : newCategory.categoryName}
                                    onChange={handleInputChange}
                                />
                            </Form.Group>
                        </Form>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleCloseModal}>
                            {t('managerCategoriesPage.cancel')}
                        </Button>
                        {selectedCategory ? (
                            <Button variant="primary" onClick={handleUpdateCategory}>
                                {t('managerCategoriesPage.save')}
                            </Button>
                        ) : (
                            <Button variant="primary" onClick={handleCreateCategory}>
                                {t('managerCategoriesPage.create')}
                            </Button>
                        )}
                    </Modal.Footer>
                </Modal>
            </Container>
            <Footer />
        </>
    );
};

export default ManagerCategoriesPage;