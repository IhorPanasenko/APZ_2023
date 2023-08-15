import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card } from 'react-bootstrap';
import Footer from '../../components/footer/Footer';
import UserHeader from '../../components/headers/userHeader/UserHeader';
import { useParams } from 'react-router-dom';

const URL_GET_COMPANY_BY_ID = "https://localhost:7082/api/Company/GetById?id=";

const DetailCompanyPage = () => {
  const { t } = useTranslation();
  const [company, setCompany] = useState(null);
  const {companyId} = useParams();
  console.log(companyId);

  const getCompany = async () => {
    try {
      const response = await axios.get(URL_GET_COMPANY_BY_ID+companyId);
      setCompany(response.data);
      console.log(response.data);
    } catch (error) {
      console.error('Error fetching company:', error);
    }
  };

  useEffect(() => {
    getCompany();
  }, []);

  return (
    <>
      <UserHeader />
      <div className="bg-info pb-4">
        <Container className="pt-3 bg-info">
          {company ? (
            <Card className="my-3 bg-warning border border-dark rounded border-2">
              <Card.Img variant="top" src={company.companyLogo} />
              <Card.Body>
                <Card.Title>{company.title}</Card.Title>
                <Card.Text>{company.description}</Card.Text>
                <Card.Text>
                  <strong>{t('detailCompanyPage.maxDiscount')}:</strong> {company.maxDiscountPercentage}%
                </Card.Text>
                <Card.Text>
                  <strong>{t('detailCompanyPage.address')}:</strong> {company.address}
                </Card.Text>
                <Card.Text>
                  <strong>{t('detailCompanyPage.phoneNumber')}:</strong> {company.phoneNumber}
                </Card.Text>
                <Card.Text>
                  <strong>{t('detailCompanyPage.email')}:</strong> {company.emailAddress}
                </Card.Text>
                <Card.Text>
                  <strong>{t('detailCompanyPage.website')}:</strong>{' '}
                  <a href={company.websiteAddress}>{company.websiteAddress}</a>
                </Card.Text>
              </Card.Body>
            </Card>
          ) : (
            <p>{t('detailCompanyPage.loading')}</p>
          )}
        </Container>
      </div>
      <Footer />
    </>
  );
};

export default DetailCompanyPage;
