import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { Form, Button, Container, Card, Table } from 'react-bootstrap';
import axios from 'axios';

const URL_GET_NUTRITIONS = 'https://localhost:7082/api/Nutrition/GetByUserId?userId=';
const URL_CREATE_NUTRITION = 'https://localhost:7082/api/Nutrition/Create';
const URL_DELETE_NUTRITION = 'https://localhost:7082/api/Nutrition/Delete?id=';

const UserNutritions = ({ userId, handleClose }) => {
  const { t } = useTranslation();
  const [nutritions, setNutritions] = useState([]);
  const [meal, setMeal] = useState('');
  const [food, setFood] = useState('');
  const [calories, setCalories] = useState('');
  const [fat, setFat] = useState('');
  const [protein, setProtein] = useState('');
  const [carbs, setCarbs] = useState('');

  useEffect(() => {
    fetchNutritions();
  }, []);

  const fetchNutritions = async () => {
    try {
      const response = await axios.get(`${URL_GET_NUTRITIONS}${userId}`);
      setNutritions(response.data);
    } catch (error) {
      console.error('Error fetching nutritions:', error);
    }
  };

  const handleCreate = async (e) => {
    e.preventDefault();

    const newNutrition = {
      userId,
      meal,
      food,
      calories,
      fat,
      protein,
      cards: carbs,
    };

    try {
      const response = await axios.post(URL_CREATE_NUTRITION, newNutrition);
      console.log(response.data);
      fetchNutritions();
      // Handle success and show appropriate message to the user
    } catch (error) {
      console.error('Error creating nutrition:', error);
      // Handle error and show appropriate message to the user
    }

    // Reset form fields after submission
    setMeal('');
    setFood('');
    setCalories('');
    setFat('');
    setProtein('');
    setCarbs('');
  };

  const handleDelete = async (id) => {
    try {
      const response = await axios.delete(`${URL_DELETE_NUTRITION}${id}`);
      console.log(response.data);
      fetchNutritions();
      // Handle success and show appropriate message to the user
    } catch (error) {
      console.error('Error deleting nutrition:', error);
      // Handle error and show appropriate message to the user
    }
  };

  const handleCloseClick = () =>{
    handleClose();
  }

  return (
    <Card className="bg-warning p-2 border border-dark border-2 rounded">
      <Container>
        <h1>{t('userNutritions.title')}</h1>
        <h3>{t('userNutritions.addNutrition')}</h3>
        <Form onSubmit={handleCreate}>
          <Form.Group className="mb-3" controlId="formMeal">
            <Form.Label>{t('userNutritions.meal')}</Form.Label>
            <Form.Control
              type="text"
              placeholder={t('userNutritions.mealPlaceholder')}
              value={meal}
              onChange={(e) => setMeal(e.target.value)}
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formFood">
            <Form.Label>{t('userNutritions.food')}</Form.Label>
            <Form.Control
              type="text"
              placeholder={t('userNutritions.foodPlaceholder')}
              value={food}
              onChange={(e) => setFood(e.target.value)}
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formCalories">
            <Form.Label>{t('userNutritions.calories')}</Form.Label>
            <Form.Control
              type="number"
              placeholder={t('userNutritions.caloriesPlaceholder')}
              value={calories}
              onChange={(e) => setCalories(e.target.value)}
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formFat">
            <Form.Label>{t('userNutritions.fat')}</Form.Label>
            <Form.Control
              type="number"
              placeholder={t('userNutritions.fatPlaceholder')}
              value={fat}
              onChange={(e) => setFat(e.target.value)}
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formProtein">
            <Form.Label>{t('userNutritions.protein')}</Form.Label>
            <Form.Control
              type="number"
              placeholder={t('userNutritions.proteinPlaceholder')}
              value={protein}
              onChange={(e) => setProtein(e.target.value)}
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formCarbs">
            <Form.Label>{t('userNutritions.carbs')}</Form.Label>
            <Form.Control
              type="number"
              placeholder={t('userNutritions.carbsPlaceholder')}
              value={carbs}
              onChange={(e) => setCarbs(e.target.value)}
            />
          </Form.Group>
          <div className="d-glex">
            <Button className="m-2 p-5 pt-3 pb-3" variant="primary" type="submit">
              {t('userNutritions.addButton')}
            </Button>
            <Button className="m-2 p-5 pt-3 pb-3" variant="secondary" onClick={()=>{handleCloseClick()}}>
              {t('userNutritions.colapse')}
            </Button>

          </div>
        </Form>

        <h3>{t('userNutritions.lastNutritions')}</h3>
        {nutritions.length > 0 ? (
          <Table striped bordered>
            <thead>
              <tr>
                <th>{t('userNutritions.meal')}</th>
                <th>{t('userNutritions.food')}</th>
                <th>{t('userNutritions.calories')}</th>
                <th>{t('userNutritions.fat')}</th>
                <th>{t('userNutritions.protein')}</th>
                <th>{t('userNutritions.carbs')}</th>
                <th>{t('userNutritions.actions')}</th>
              </tr>
            </thead>
            <tbody>
              {nutritions.slice(0, 5).map((nutrition) => (
                <tr key={nutrition.id}>
                  <td>{nutrition.meal}</td>
                  <td>{nutrition.food}</td>
                  <td>{nutrition.calories}</td>
                  <td>{nutrition.fat}</td>
                  <td>{nutrition.protein}</td>
                  <td>{nutrition.cards}</td>
                  <td>
                    <Button variant="danger" onClick={() => handleDelete(nutrition.id)}>
                      {t('userNutritions.deleteButton')}
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        ) : (
          <p>{t('userNutritions.noNutritions')}</p>
        )}
      </Container>
    </Card>
  );
};

export default UserNutritions;
