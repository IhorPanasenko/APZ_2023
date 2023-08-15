import React from 'react';
import { useTranslation } from 'react-i18next';
import { Container, Card, Row, Col } from 'react-bootstrap';
import UserHeader from '../../components/headers/userHeader/UserHeader';
import Footer from '../../components/footer/Footer';
import introductionImage from '../../images/Logo.png';
import satisfiedCustomersImage from '../../images/Benefits.jpg';
import './AboutPage.css';

const InsuranceDiscountsPage = () => {
  const { t } = useTranslation();

  return (
    <>
      <UserHeader />
      <div className="bg-info">
        <Container className="pt-3 pb-5 bg-info">
          <h1 className="my-4">{t('insuranceDiscountsPage.introduction')}</h1>
          <Card className="mb-4 bg-warning">
            <Card.Body>
              <Row>
                <Col md={4} className="text-center mt-1 mb-2">
                  <img src={introductionImage} alt="Introduction" className="img-fluid" />
                </Col>
                <Col md={8}>
                  <p>{t('insuranceDiscountsPage.description')}</p>
                </Col>
              </Row>
            </Card.Body>
          </Card>
          <Card className="mb-4 bg-warning">
            <Card.Body>
              <h2>{t('insuranceDiscountsPage.benefits.title')}</h2>
              <ul>
                <li>{t('insuranceDiscountsPage.benefits.item1')}</li>
                <li>{t('insuranceDiscountsPage.benefits.item2')}</li>
                <li>{t('insuranceDiscountsPage.benefits.item3')}</li>
              </ul>
            </Card.Body>
          </Card>
          <Card className='bg-warning'>
            <Card.Body>
              <h2>{t('insuranceDiscountsPage.satisfiedCustomers.title')}</h2>
              <Row>
                <Col className="text-center mt-4">
                  <img src={satisfiedCustomersImage} alt="Satisfied Customers" className="img-fluid border rounded border-dark" />
                </Col>
              </Row>
              <Row>
                <Col md={6} className='mt-3'>
                  <Card>
                    <Card.Body>
                      <Card.Title>{t('insuranceDiscountsPage.satisfiedCustomers.customer1.name')}</Card.Title>
                      <Card.Text>{t('insuranceDiscountsPage.satisfiedCustomers.customer1.feedback')}</Card.Text>
                    </Card.Body>
                  </Card>
                </Col>
                <Col md={6} className='mt-3'>
                  <Card>
                    <Card.Body>
                      <Card.Title>{t('insuranceDiscountsPage.satisfiedCustomers.customer2.name')}</Card.Title>
                      <Card.Text>{t('insuranceDiscountsPage.satisfiedCustomers.customer2.feedback')}</Card.Text>
                    </Card.Body>
                  </Card>
                </Col>
              </Row>
            </Card.Body>
          </Card>
        </Container>
      </div>
      <Footer />
    </>
  );
};

export default InsuranceDiscountsPage;