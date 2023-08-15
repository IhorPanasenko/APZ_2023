import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Form } from 'react-bootstrap';

const URL_GET_BAD_HABITS = "https://localhost:7082/api/BadHabit/GetAll";
const URL_GET_USER_BAD_HABITS = `https://localhost:7082/api/UserBadHabit/GetByUserId?userId=`;
const URL_ADD_BAD_HABIT = "https://localhost:7082/api/UserBadHabit/Create";

const UserBadHabits = ({ userId, handleCloseBadHabit }) => {
    const { t } = useTranslation();
    const [badHabits, setBadHabits] = useState([]);
    const [allHabits, setAllHabits] = useState([]);
    const [selectedHabit, setSelectedHabit] = useState("");
    const [isLoading, setIsLoading] = useState(false);


    const getBadHabits = async () => {
        try {
            const response = await axios.get(URL_GET_BAD_HABITS);
            setAllHabits(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching bad habits:', error);
        }
    };

    const getUserBadHabits = async () => {
        try {
            const response = await axios.get(URL_GET_USER_BAD_HABITS + userId);
            setBadHabits(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching user bad habits:', error);
        }
    };

    const addBadHabit = async (habitId) => {
        try {
            var body = {
                userId: userId,
                badHabitId: habitId
            }
            console.log(body);
            setIsLoading(true);
            await axios.post(URL_ADD_BAD_HABIT, body);
            await getUserBadHabits();
            setIsLoading(false);

        } catch (error) {
            console.error('Error adding bad habit:', error);
            setIsLoading(false);
        }
    };

    useEffect(() => {
        console.log(userId);
        getBadHabits();
        getUserBadHabits();
    }, []);

    const handleHabitChange = (e) => {
        console.log(e.target.value);
        setSelectedHabit(e.target.value);
    };

    const handleAddHabit = () => {
        addBadHabit(selectedHabit);
    };

    const handleColapse = () => {
        handleCloseBadHabit()
    }

    return (
        <Card className="my-3 bg-warning border rounded border-dark border-2">
            <Card.Body>
                <h2>{t('userBadHabits.title')}</h2>
                {badHabits.length > 0 ? (
                    <ul>
                        {badHabits.map((habit) => (
                            <li key={habit.id}>
                                {habit.badHabit.name} - {habit.badHabit.level}
                            </li>
                        ))}
                    </ul>
                ) : (
                    <p>{t('userBadHabits.noHabits')}</p>
                )}
                <h3>{t('userBadHabits.addHabitTitle')}</h3>
                <Form.Group controlId="selectedHabit">
                    <Form.Label>{t('userBadHabits.selectHabit')}</Form.Label>
                    <Form.Control as="select" value={selectedHabit} onChange={handleHabitChange}>
                        <option value="">{t('userBadHabits.selectHabitPlaceholder')}</option>
                        {allHabits.map((habit) => (
                            <option key={habit.id} value={habit.id} >
                                {habit.name}
                            </option>
                        ))}
                    </Form.Control>
                </Form.Group>
                <div className="d-flex justify-content-center">
                    <Button className='m-2 p-5 pt-3 pb-3 ' variant="primary" onClick={handleAddHabit} disabled={isLoading}>
                        {isLoading ? t('userBadHabits.addingHabit') : t('userBadHabits.addButton')}
                    </Button>
                    <Button className='m-2 p-5 pt-3 pb-3' variant="primary" onClick={handleColapse} disabled={isLoading}>
                        {isLoading ? t('userBadHabits.addingHabit') : t('userBadHabits.colapse')}
                    </Button>
                </div>
            </Card.Body>
        </Card>
    );
};

export default UserBadHabits;
