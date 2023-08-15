import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Card } from 'react-bootstrap';
import './UsersInsurances.css';

const UserInsurances = ({ userId }) => {
  const { t } = useTranslation();
  const [insurances, setInsurances] = useState([]);

  useEffect(() => {
    const fetchInsurances = async () => {
      try {
        const response = await axios.get(`https://localhost:7082/api/UserPolicy/GetByUserId?userId=${userId}`);
        setInsurances(response.data);
        console.log(response.data);

      } catch (error) {
        console.error('Error fetching insurances:', error);
      }
    };

    fetchInsurances();
  }, [userId]);

  const formatDate = (date) => {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(date).toLocaleDateString(undefined, options);
  };

  return (
    <Card className='mt-3 p-3 bg-warning border rounded border-2 border-dark'>
      <div className=''>
        <h2>{t('userInsurances.title')}</h2>
        {insurances.map((insurance) => (
          <Card
            key={insurance.id}
            className="my-3"
            style={{ cursor: 'pointer' }}

          >
            <Card.Body className='bg-warning'>
              <Card.Title>{insurance.policy.name}</Card.Title>
              <Card.Text>
                <strong>{t('userInsurances.description')}:</strong> {insurance.policy.description}
              </Card.Text>
              <Card.Text>
                <strong>{t('userInsurances.startDate')}:</strong> {formatDate(insurance.startDate)}
              </Card.Text>
              <Card.Text>
                <strong>{t('userInsurances.endDate')}:</strong> {formatDate(insurance.endDate)}
              </Card.Text>
              <Card.Text>
                <strong>{t('userInsurances.coverageAmount')}:</strong> {insurance.policy.coverageAmount}
              </Card.Text>
              <Card.Text>
                <strong>{t('userInsurances.price')}:</strong> {insurance.policy.price}
              </Card.Text>
              <Card.Text>
                <strong>{t('userInsurances.timePeriod')}:</strong> {insurance.policy.timePeriod}
              </Card.Text>
            </Card.Body>
          </Card>
        ))}
      </div>
    </Card>
  );
};

export default UserInsurances;
