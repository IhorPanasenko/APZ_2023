import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card } from 'react-bootstrap';
import ManagerHeader from '../../components/headers/managerHeader/ManagerHeader';
import Footer from '../../components/footer/Footer';
import { useNavigate } from 'react-router-dom';

const URL_GET_COMPANY_USERS = "https://localhost:7082/api/UserPolicy/GetUsersByCompany?companyId="

const ManagerCompanyUsersPage = () => {
    const { t } = useTranslation();
    const navigate = useNavigate();
    const [users, setUsers] = useState([]);
    const companyId = localStorage.getItem('managerCompanyId');

    const getUsers = async () => {
        try {
            const response = await axios.get(
                URL_GET_COMPANY_USERS + companyId
            );
            console.log(response.data);
            setUsers(response.data);
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    };


    useEffect(() => {

        if (companyId) {
            getUsers();
        }
    }, [companyId]);


    const handleUserClick = (userId) => {
        navigate("/managerAllUserPolicies?userId="+userId);
    }

    return (
        <>
            <ManagerHeader />
            <Container>
                <h1 className="my-4">{t('managerCompanyUsersPage.userList')}</h1>
                <p><strong>{t("managerCompanyUsersPage.totalNumberOfUsers")}:</strong> {users.length}</p>
                {users.map((userPolicy) => (
                    <Card key={userPolicy.id} className="my-3 bg-info border border-success border-2" onClick={() => { handleUserClick(userPolicy.userId) }}>
                        <Card.Body>
                            <Card.Title>{`${userPolicy.appUser.firstName} ${userPolicy.appUser.lastName}`}</Card.Title>
                            <Card.Text>{`${t('managerCompanyUsersPage.email')}: ${userPolicy.appUser.email}`}</Card.Text>
                            <Card.Text>{`${t('managerCompanyUsersPage.phoneNumber')}: ${userPolicy.appUser.phoneNumber}`}</Card.Text>
                            <Card.Text>{`${t('managerCompanyUsersPage.address')}: ${userPolicy.appUser.address}`}</Card.Text>
                            <Card.Text>{t("managerCompanyUsersPage.usersDiscount")}: {userPolicy.appUser.discount}</Card.Text>
                            <Card.Text></Card.Text>
                        </Card.Body>
                    </Card>
                ))}
            </Container>
            <Footer />
        </>
    );
};

export default ManagerCompanyUsersPage;