import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Modal, Form } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import AdminHeader from '../../components/headers/adminHeader/AdminHeader';

const AdminCategoryPage = () => {
  const { t } = useTranslation();
  const [categories, setCategories] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [newCategory, setNewCategory] = useState({ categoryName: '' });
  const [updatingCategoryId, setUpdatingCategoryId] = useState(null);

  const URL_GET_ALL_CATEGORIES = 'https://localhost:7082/api/Category/GetAll';
  const URL_CREATE_CATEGORY = 'https://localhost:7082/api/Category/Create';
  const URL_UPDATE_CATEGORY = 'https://localhost:7082/api/Category/Update';
  const URL_DELETE_CATEGORY = 'https://localhost:7082/api/Category/Delete?id=';

  const handleCloseModal = () => {
    setShowModal(false);
    setUpdatingCategoryId(null);
    resetNewCategoryFields();
  };

  const handleShowModal = () => {
    setShowModal(true);
  };

  const handleInputChange = (e) => {
    setNewCategory((prevState) => ({
      ...prevState,
      [e.target.name]: e.target.value,
    }));
  };

  const handleCreateCategory = async () => {
    try {
      await axios.post(URL_CREATE_CATEGORY, newCategory);
      getCategories();
      handleCloseModal();
    } catch (error) {
      console.error('Error creating category:', error);
    }
  };

  const handleUpdateCategory = async () => {
    try {
      const updatedCategory = {
        id: updatingCategoryId,
        categoryName: newCategory.categoryName,
      };
      await axios.put(URL_UPDATE_CATEGORY, updatedCategory);
      getCategories();
      handleCloseModal();
    } catch (error) {
      console.error('Error updating category:', error);
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

  const handleEditCategory = (category) => {
    setNewCategory(category);
    setUpdatingCategoryId(category.id);
    handleShowModal();
  };

  const resetNewCategoryFields = () => {
    setNewCategory({ categoryName: '' });
  };

  const getCategories = async () => {
    try {
      const response = await axios.get(URL_GET_ALL_CATEGORIES);
      setCategories(response.data);
    } catch (error) {
      console.error('Error fetching categories:', error);
    }
  };

  useEffect(() => {
    getCategories();
  }, []);

  return (
    <>
    <AdminHeader/>
      <Container>
        <div className="d-grid gap-2">
          <Button variant="primary" className="my-3" size="lg" onClick={handleShowModal}>
            {t('adminCategoryPage.createNewCategory')}
          </Button>
        </div>
        <h1 className="my-2">{t('adminCategoryPage.categoryList')}</h1>
        {categories.map((category) => (
          <Card key={category.id} className="my-3 bg-info text-center">
            <Card.Body>
              <Card.Title>{category.categoryName}</Card.Title>
              <div className="d-flex justify-content-center">
                <Button
                  className="p-4 pt-2 pb-2 m-2"
                  variant="danger"
                  onClick={() => handleDeleteCategory(category.id)}
                >
                  {t('adminCategoryPage.delete')}
                </Button>
                <Button
                  className="p-4 pt-2 pb-2 m-2"
                  variant="warning"
                  onClick={() => handleEditCategory(category)}
                >
                  {t('adminCategoryPage.update')}
                </Button>
              </div>
            </Card.Body>
          </Card>
        ))}
      </Container>

      <Modal show={showModal} onHide={handleCloseModal}>
        <Modal.Header closeButton>
          <Modal.Title>
            {updatingCategoryId ? t('adminCategoryPage.updateCategory') : t('adminCategoryPage.createCategory')}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form.Group controlId="formCategoryName">
            <Form.Label>{t('adminCategoryPage.categoryName')}</Form.Label>
            <Form.Control
              type="text"
              name="categoryName"
              value={newCategory.categoryName}
              onChange={handleInputChange}
              required
            />
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseModal}>
            {t('adminCategoryPage.cancel')}
          </Button>
          <Button variant="primary" onClick={updatingCategoryId ? handleUpdateCategory : handleCreateCategory}>
            {updatingCategoryId ? t('adminCategoryPage.update') : t('adminCategoryPage.create')}
          </Button>
        </Modal.Footer>
      </Modal>
      <Footer/>
    </>
  );
};

export default AdminCategoryPage;
