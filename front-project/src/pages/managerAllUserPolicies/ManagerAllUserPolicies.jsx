import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card } from 'react-bootstrap';
import ManagerHeader from '../../components/headers/managerHeader/ManagerHeader';
import Footer from '../../components/footer/Footer';
import { useParams, useLocation } from 'react-router-dom';

const URL_GET_POLICIES_BY_USER = "https://localhost:7082/api/UserPolicy/GetByUserId?userId=";

const ManagerAllUserPolicies = () => {
    const { t } = useTranslation();
    const [users, setUsers] = useState([]);
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const userId = searchParams.get('userId');

    const getUsers = async () => {
        try {
            console.log(userId);
            const response = await axios.get(
                URL_GET_POLICIES_BY_USER + userId
            );
            console.log(response.data);
            setUsers(response.data);
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    };

    useEffect(() => {
        console.log(userId);
        if (userId) {
            getUsers();
        }
    }, []);

    function formatDate(dateString) {
        const date = new Date(dateString);
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');

        return `${year}-${month}-${day}`;
    }

    return (
        <>
            <ManagerHeader />
            <Container>
                <h1 className="my-4">{t('managerAllUserPolicies.policiesList')}</h1>
                <p><strong>{t("managerAllUserPolicies.totalNumberOfPolicies")}:</strong> {users.length}</p>
                <Card>
    
                    <Card.Body className='bg-info border border-success border-2 rounded'>
                        <Card.Header>
                            <Card.Text>
                                {t("managerAllUserPolicies.userTitle")}
                            </Card.Text>
                        </Card.Header>
                        <Card.Text>
                        <Card.Title>{`${users[0]?.appUser.firstName} ${users[0]?.appUser.lastName}`}</Card.Title>
                            <Card.Text>{`${t('managerAllUserPolicies.email')}: ${users[0]?.appUser.email}`}</Card.Text>
                            <Card.Text>{`${t('managerAllUserPolicies.phoneNumber')}: ${users[0]?.appUser.phoneNumber}`}</Card.Text>
                            <Card.Text>{`${t('managerAllUserPolicies.address')}: ${users[0]?.appUser.address}`}</Card.Text>
                            <Card.Text>{t("managerAllUserPolicies.usersDiscount")}: {users[0]?.appUser.discount}</Card.Text>
                        </Card.Text>
                    </Card.Body>
                </Card>
                {users.map((userPolicy) => (
                    <Card key={userPolicy.id} className="my-3 bg-warning border border-success border-2 rounded" >
                        <Card.Header>
                            <Card.Text>
                                {t("managerAllUserPolicies.insuranceTitle")}
                            </Card.Text>
                        </Card.Header>
                        <Card.Body>
                            <Card.Text>{t("managerAllUserPolicies.insuranceName")}: {userPolicy.policy.name} </Card.Text>
                            <Card.Text>{t("managerAllUserPolicies.price")}: {userPolicy.policy.price}</Card.Text>
                            <Card.Text>{t("managerAllUserPolicies.coverageAmount")}: {userPolicy.policy.coverageAmount}</Card.Text>
                            <Card.Text>{t("managerAllUserPolicies.startDate")}: {formatDate(userPolicy.startDate)}</Card.Text>
                            <Card.Text>{t("managerAllUserPolicies.endDate")}: {formatDate(userPolicy.endDate)}</Card.Text>
                            <Card.Text></Card.Text>
                        </Card.Body>
                    </Card>
                ))}
            </Container>
            <Footer />
        </>
    );
};

export default ManagerAllUserPolicies;