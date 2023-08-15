import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card } from 'react-bootstrap';
import {useNavigate } from 'react-router-dom';
import Footer from '../../components/footer/Footer';
import UserHeader from '../../components/headers/userHeader/UserHeader';
import './CategoriesPage.css'

const URL_GET_ALL_CATEGORIES = "https://localhost:7082/api/Category/GetAll";

const CategoriesPage = () => {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const [categories, setCategories] = useState([]);

    const fetchCategories = async () => {
        try {
            const response = await axios.get(URL_GET_ALL_CATEGORIES);
            setCategories(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    useEffect(() => {
        fetchCategories();
    }, []);

    const handleCategoryClick = (categoryId) => {
        console.log(categoryId);
        navigate("/Home?categoryId="+categoryId)
    };

    return (
        <>
            <UserHeader />
            <div className="bg-info">
                <Container className="pt-3 pb-5 bg-info">
                    <h1 className="my-4">{t('categoriesPage.title')}</h1>
                    {categories.map((category) => (
                        <Card
                            key={category.categoryId}
                            className="my-3 bg-warning border border-dark rounded border-2 grow-card"
                            onClick={() => handleCategoryClick(category.id)}
                        >
                            <Card.Body>
                                <Card.Title>{category.categoryName}</Card.Title>
                            </Card.Body>
                        </Card>
                    ))}
                </Container>
            </div>
            <Footer />
        </>
    );
};

export default CategoriesPage;