import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import Footer from '../../components/footer/Footer';
import UserHeader from '../../components/headers/userHeader/UserHeader';
import './CompaniesPage.css';

const URL_GET_ALL_COMPANIES = "https://localhost:7082/api/Company/GetAll";

const CompaniesPage = () => {
  const { t } = useTranslation();
  const [companies, setCompanies] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchCompanies = async () => {
      try {
        const response = await axios.get(URL_GET_ALL_COMPANIES);
        setCompanies(response.data);
        console.log(response.data);
      } catch (error) {
        console.error('Error fetching companies:', error);
      }
    };

    fetchCompanies();
  }, []);

  const handleCardClick = (companyId) => {
    navigate(`/Companies/${companyId}`);
  };

  return (
    <>
      <UserHeader />
      <div className="bg-info">
        <Container className="pt-3 pb-5 bg-info">
          <h1 className="my-4">{t('insuranceCompaniesPage.title')}</h1>
          {companies.map((company) => (
            <Card
              key={company.id}
              className={`my-3 bg-warning border border-dark rounded border-2 card-hover`} // Apply the CSS classes
              onClick={() => handleCardClick(company.id)}
            >
              <Card.Img variant="top" src={company.companyLogo} />
              <Card.Body>
                <Card.Title>{company.companyName}</Card.Title>
                <Card.Text>
                  <strong>{t('insuranceCompaniesPage.address')}:</strong> {company.address}
                </Card.Text>
                <Card.Text>
                  <strong>{t('insuranceCompaniesPage.phoneNumber')}:</strong> {company.phoneNumber}
                </Card.Text>
              </Card.Body>
            </Card>
          ))}
        </Container>
      </div>
      <Footer />
    </>
  );
};

export default CompaniesPage;