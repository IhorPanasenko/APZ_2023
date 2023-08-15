import React from 'react';
import { useTranslation } from 'react-i18next';
import { Container, Row, Col } from 'react-bootstrap';

const Footer = () => {
  const { t } = useTranslation();

  return (
    <footer className="bg-dark text-light py-3">
      <Container>
        <Row>
          <Col md={6}>
            <h5>{t('footer.aboutUs')}</h5>
            <p>{t('footer.aboutUsDescription')}</p>
          </Col>
          <Col md={6}>
            <h5>{t('footer.contactUs')}</h5>
            <address>
              123 {t('footer.address.street')}
              <br />
              {t('footer.address.city')}, {t('footer.address.zip')}
              <br />
              {t('footer.contact.phone')}: (099)-888-9999
              <br />
              {t('footer.contact.email')}: info@insurancebydiscount.com
            </address>
          </Col>
        </Row>
        <hr />
        <Row>
          <Col>
            <p className="text-center">
              &copy; {new Date().getFullYear()} Insurance by Discount. {t('footer.allRightsReserved')}
            </p>
          </Col>
        </Row>
      </Container>
    </footer>
  );
};

export default Footer;