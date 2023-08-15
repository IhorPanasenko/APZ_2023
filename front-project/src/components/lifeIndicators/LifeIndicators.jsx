import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Form, Button, Container, Card, Table } from 'react-bootstrap';
import axios from 'axios';

const URL_GET_STATIC_MEASUREMENTS = 'https://localhost:7082/api/StaticMeasurments/GetByuserId?userId=';
const URL_UPDATE_STATIC_MEASUREMENTS = 'https://localhost:7082/api/StaticMeasurments/Update';
const URL_GET_PERIODIC_MEASUREMENTS = 'https://localhost:7082/api/PeriodicMeasurments/GetByUserId?userId=';

const LifeIndicators = ({ userId, LifeIndicatorsColapseClick }) => {
  const { t } = useTranslation();
  const [measurements, setMeasurements] = useState({});
  const [height, setHeight] = useState('');
  const [weight, setWeight] = useState('');
  const [waist, setWaist] = useState('');
  const [measurmentDate, setMeasurementDate] = useState(getCurrentDate());
  const [periodicMeasurements, setPeriodicMeasurements] = useState([]);
  const [averageMeasurements, setAverageMeasurements] = useState({});

  function getCurrentDate() {
    const currentDate = new Date();
    return formatDate(currentDate);
  }

  function formatDate(currentDate) {
    const year = currentDate.getFullYear();
    const month = String(currentDate.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const day = String(currentDate.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  function convertDate(dateString) {
    let date = Date.parse(dateString);
    return date;
  }

  useEffect(() => {
    fetchMeasurements();
    fetchPeriodicMeasurements();
  }, []);

  const fetchMeasurements = async () => {
    try {
      console.log(userId);
      const URL = URL_GET_STATIC_MEASUREMENTS+userId;
      console.log(URL);
      const response = await axios.get(URL);
      setMeasurements(response.data);
      console.log(response.data);
    } catch (error) {
      console.error('Error fetching measurements:', error);
    }
  };

  const fetchPeriodicMeasurements = async () => {
    try {
      console.log(userId);
      const URL = URL_GET_PERIODIC_MEASUREMENTS + userId;
      const response = await axios.get(URL);
      setPeriodicMeasurements(response.data);
      calculateAverageMeasurements(response.data);
    } catch (error) {
      console.error('Error fetching periodic measurements:', error);
    }
  };

  const calculateAverageMeasurements = (periodicMeasurements) => {
    // Calculate average values by Day, Week, and Month
    const averageValues = {
      day: {
        pulse: 0,
        glucose: 0,
        cholesterol: 0,
        bloodPreasure: 0,
        count: 0
      },
      week: {
        pulse: 0,
        glucose: 0,
        cholesterol: 0,
        bloodPreasure: 0,
        count: 0
      },
      month: {
        pulse: 0,
        glucose: 0,
        cholesterol: 0,
        bloodPreasure: 0,
        count: 0
      }
    };

    periodicMeasurements.forEach((measurement) => {
      const measurementDate = new Date(measurement.mesurmentDate);
      const currentDate = new Date();

      // Calculate average by day
      if (measurementDate.toDateString() === currentDate.toDateString()) {
        averageValues.day.pulse += measurement.pulse;
        averageValues.day.glucose += measurement.glucose;
        averageValues.day.cholesterol += measurement.cholesterol;
        averageValues.day.bloodPreasure += measurement.bloodPreasure;
        averageValues.day.count += 1;
      }

      // Calculate average by week
      const currentWeek = getWeekNumber(currentDate);
      const measurementWeek = getWeekNumber(measurementDate);
      if (measurementWeek === currentWeek) {
        averageValues.week.pulse += measurement.pulse;
        averageValues.week.glucose += measurement.glucose;
        averageValues.week.cholesterol += measurement.cholesterol;
        averageValues.week.bloodPreasure += measurement.bloodPreasure;
        averageValues.week.count += 1;
      }

      // Calculate average by month
      if (
        measurementDate.getFullYear() === currentDate.getFullYear() &&
        measurementDate.getMonth() === currentDate.getMonth()
      ) {
        averageValues.month.pulse += measurement.pulse;
        averageValues.month.glucose += measurement.glucose;
        averageValues.month.cholesterol += measurement.cholesterol;
        averageValues.month.bloodPreasure += measurement.bloodPreasure;
        averageValues.month.count += 1;
      }
    });

    // Calculate averages
    averageValues.day.pulse /= averageValues.day.count;
    averageValues.day.glucose /= averageValues.day.count;
    averageValues.day.cholesterol /= averageValues.day.count;
    averageValues.day.bloodPreasure /= averageValues.day.count;

    averageValues.week.pulse /= averageValues.week.count;
    averageValues.week.glucose /= averageValues.week.count;
    averageValues.week.cholesterol /= averageValues.week.count;
    averageValues.week.bloodPreasure /= averageValues.week.count;

    averageValues.month.pulse /= averageValues.month.count;
    averageValues.month.glucose /= averageValues.month.count;
    averageValues.month.cholesterol /= averageValues.month.count;
    averageValues.month.bloodPreasure /= averageValues.month.count;

    setAverageMeasurements(averageValues);
    console.log(averageValues);
  };

  const getWeekNumber = (date) => {
    const oneJan = new Date(date.getFullYear(), 0, 1);
    const millisecondsInWeek = 604800000;
    return Math.ceil(((date - oneJan) / millisecondsInWeek) + oneJan.getDay() / 7);
  };

  const handleUpdate = async (e, id) => {
    e.preventDefault();

    const updatedMeasurement = {
      id,
      height,
      weight,
      waist,
      measurmentDate
    };
    console.log(updatedMeasurement);

    try {
      const response = await axios.put(URL_UPDATE_STATIC_MEASUREMENTS, updatedMeasurement);
      console.log(response.data);
      await fetchMeasurements();
    } catch (error) {
      console.error('Error updating measurements:', error);
    }
  };

  function DateStringToNormal(dateString) {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  function colapseClick(e){
    e.preventDefault();
    LifeIndicatorsColapseClick();
  }

  return (
    <Card className="bg-warning p-2 border border-dark border-2 rounded">
      <Container className="bg-warning">
        <h1>{t('lifeIndicators.title')}</h1>
        {measurements.length > 0 ? (
          <div>
            <h3>{t('lifeIndicators.currentMeasurements')}</h3>
            <p>
              <strong>{t('lifeIndicators.height')}:</strong> {measurements[0].height}
            </p>
            <p>
              <strong>{t('lifeIndicators.weight')}:</strong> {measurements[0].weight}
            </p>
            <p>
              <strong>{t('lifeIndicators.waist')}:</strong> {measurements[0].waist}
            </p>
          </div>
        ) : (
          <p>{t('lifeIndicators.noMeasurements')}</p>
        )}

        <h3>{t('lifeIndicators.updateMeasurements')}</h3>
        <Form onSubmit={(e) => { handleUpdate(e, measurements[0].id) }}>
          <Form.Group className="mb-3" controlId="formHeight">
            <Form.Label>{t('lifeIndicators.height')}</Form.Label>
            <Form.Control
              type="number"
              min={50}
              max={300}
              placeholder={t('lifeIndicators.heightPlaceholder')}
              value={height}
              onChange={(e) => setHeight(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formWeight">
            <Form.Label>{t('lifeIndicators.weight')}</Form.Label>
            <Form.Control
              type="number"
              min={30}
              max={400}
              placeholder={t('lifeIndicators.weightPlaceholder')}
              value={weight}
              onChange={(e) => setWeight(e.target.value)}
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formWaist">
            <Form.Label>{t('lifeIndicators.waist')}</Form.Label>
            <Form.Control
              type="number"
              min={30}
              max={200}
              placeholder={t('lifeIndicators.waistPlaceholder')}
              value={waist}
              onChange={(e) => setWaist(e.target.value)}
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formMeasurementDate">
            <Form.Label>{t('lifeIndicators.dateOfMeasurement')}</Form.Label>
            <Form.Control
              type="date"
              value={measurmentDate}
              onChange={(e) => setMeasurementDate(e.target.value)}
            />
          </Form.Group>
          <div className="d-flex justify-content-center">
            <Button className='m-3 p-4 pt-3 pb-3' variant="primary" type="submit" onClick={(e)=>{handleUpdate(e, measurements[0].id)}}>
              {t('lifeIndicators.updateButton')}
            </Button>
            <Button className='m-3 p-4 pt-3 pb-3' variant="secondary" onClick={(e)=>{colapseClick(e)}}>
              {t('lifeIndicators.colapse')}
            </Button>
          </div>
        </Form>

        <h3>{t('lifeIndicators.periodicMeasurements')}</h3>
        <Table className='text-center' striped bordered hover>
          <thead>
            <tr>
              <th>{t('lifeIndicators.date')}</th>
              <th>{t('lifeIndicators.pulse')}</th>
              <th>{t('lifeIndicators.glucose')}</th>
              <th>{t('lifeIndicators.cholesterol')}</th>
              <th>{t('lifeIndicators.bloodPressure')}</th>
            </tr>
          </thead>
          <tbody>
            {
              periodicMeasurements.sort((a, b) => convertDate(a.mesurmentDate) - convertDate(b.mesurmentDate)).reverse().slice(0, 7).map((measurement) => (
                <tr key={measurement?.id}>
                  <td>{DateStringToNormal(measurement?.mesurmentDate)}</td>
                  <td>{measurement?.pulse}</td>
                  <td>{measurement?.glucose}</td>
                  <td>{measurement?.cholesterol}</td>
                  <td>{measurement?.bloodPreasure}</td>
                </tr>
              ))}
          </tbody>
        </Table>

        <h4>{t('lifeIndicators.averageMeasurements')}</h4>
        <h5>{t('lifeIndicators.averageByDay')}</h5>
        <p>
          <strong>{t('lifeIndicators.pulse')}:</strong> {averageMeasurements?.day?.pulse}
        </p>
        <p>
          <strong>{t('lifeIndicators.glucose')}:</strong> {averageMeasurements.day?.glucose}
        </p>
        <p>
          <strong>{t('lifeIndicators.cholesterol')}:</strong> {averageMeasurements.day?.cholesterol}
        </p>
        <p>
          <strong>{t('lifeIndicators.bloodPressure')}:</strong> {averageMeasurements.day?.bloodPreasure}
        </p>

        <h5>{t('lifeIndicators.averageByWeek')}</h5>
        <p>
          <strong>{t('lifeIndicators.pulse')}:</strong> {averageMeasurements.week?.pulse}
        </p>
        <p>
          <strong>{t('lifeIndicators.glucose')}:</strong> {averageMeasurements.week?.glucose}
        </p>
        <p>
          <strong>{t('lifeIndicators.cholesterol')}:</strong> {averageMeasurements.week?.cholesterol}
        </p>
        <p>
          <strong>{t('lifeIndicators.bloodPressure')}:</strong> {averageMeasurements.week?.bloodPreasure}
        </p>

        <h5>{t('lifeIndicators.averageByMonth')}</h5>
        <p>
          <strong>{t('lifeIndicators.pulse')}:</strong> {averageMeasurements.month?.pulse}
        </p>
        <p>
          <strong>{t('lifeIndicators.glucose')}:</strong> {averageMeasurements.month?.glucose}
        </p>
        <p>
          <strong>{t('lifeIndicators.cholesterol')}:</strong> {averageMeasurements.month?.cholesterol}
        </p>
        <p>
          <strong>{t('lifeIndicators.bloodPressure')}:</strong> {averageMeasurements.month?.bloodPreasure}
        </p>
      </Container>
    </Card>
  );
};

export default LifeIndicators;