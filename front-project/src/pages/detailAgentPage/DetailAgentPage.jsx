import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card, Button, Form } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import Footer from '../../components/footer/Footer';
import UserHeader from '../../components/headers/userHeader/UserHeader';

const URL_GET_AGENT_BY_ID = "https://localhost:7082/api/Agent/GetById?id=";
const URL_ADD_RAITING = "https://localhost:7082/api/AgentRaiting/Create";

const DetailAgentPage = () => {
    const { t } = useTranslation();
    const [agent, setAgent] = useState(null);
    const { agentId } = useParams();
    console.log("agentId is: =" + agentId);
    const [currentRaiting, setCurerentRaiting] = useState(10);

    const getAgent = async () => {
        try {
            const response = await axios.get(`${URL_GET_AGENT_BY_ID}${agentId}`);
            setAgent(response.data);
            console.log(response.data);
        } catch (error) {
            console.error('Error fetching agent:', error);
        }
    };

    useEffect(() => {
        getAgent();
    }, [agentId]);

    const renderRatingStars = () => {
        if ((agent !== null || agent !== undefined) && (agent.raitnig !== null || agent.raiting !== undefined)) {
            const fullStars = Math.floor(agent.raiting);
            console.log("FullStars = " + fullStars);
            const halfStar = agent.raiting - fullStars >= 0.5;
            const emptyStars = 10 - fullStars - (halfStar ? 1 : 0);

            const stars = [];

            // Full stars
            for (let i = 0; i < fullStars; i++) {
                stars.push(<i key={i} className="bi bi-star-fill text-light"></i>);
            }

            // Half star
            if (halfStar) {
                stars.push(<i key={fullStars} className="bi bi-star-half text-light"></i>);
            }

            // Empty stars
            for (let i = 0; i < emptyStars; i++) {
                stars.push(<i key={fullStars + i + 1} className="bi bi-star text-light"></i>);
            }
            console.log(stars);
            return stars;
        }

        return null;
    };

    async function handleSetRaiting(e) {
        e.preventDefault();
        console.log("CurrentRaiting: "+currentRaiting)
        try {
            const response = await axios.post(URL_ADD_RAITING, {
                agentId,
                singleRaiting: currentRaiting
            });

            console.log('Rating set successfully:', response.data);
            getAgent();

        } catch (error) {
            console.error('Error setting rating:', error);
        }
    }

    return (
        <>
            <UserHeader />
            <div className="bg-info">
                <Container className="pt-3 pb-5 bg-info">
                    <h1 className="my-4">{t('detailAgentPage.title')}</h1>
                    {agent && (
                        <Card className="my-3 bg-warning border border-dark rounded border-2">
                            <Card.Body>
                                <Card.Title><strong>{`${agent.firstName} ${agent.secondName}`}</strong></Card.Title>
                                <Card.Text>
                                    <strong>{t('detailAgentPage.phoneNumber')}:</strong> {agent.phoneNumber}
                                </Card.Text>
                                <Card.Text>
                                    <strong>{t('detailAgentPage.email')}:</strong> {agent.emailAddress}
                                </Card.Text>
                                <Card.Text>
                                    <strong>{t('detailAgentPage.rating')}:</strong> {renderRatingStars()}
                                </Card.Text>
                                <Card.Title>
                                    <strong>{t('detailAgentPage.worksInto')}</strong>
                                </Card.Title>
                                <Card.Text>
                                    <strong >{t('detailAgentPage.company')}:</strong>{' '}
                                    <a className='text-dark' href={`/companies/${agent.company.id}`}>{agent.company.companyName}</a>
                                </Card.Text>
                                <Card.Text>
                                    <strong>{t('detailAgentPage.address')}:</strong> {agent.company.address}
                                </Card.Text>
                                <Card.Text>
                                    <strong>{t('detailAgentPage.description')}:</strong> {agent.company.description}
                                </Card.Text>
                            </Card.Body>
                            <Card.Footer>
                                <Form >
                                    <Form.Group controlId="email" className="mt-2 mb-2">
                                        <Form.Label>{t("detailAgentPage.setRaitingTitle")}</Form.Label>
                                        <Form.Control
                                            type="number"
                                            placeholder="10"
                                            min={0}
                                            max={10}
                                            onChange={(e) => { setCurerentRaiting(e.target.value) }}
                                            required
                                        />
                                    </Form.Group>
                                    <Button variant="light" size="lg" className='p-5 pt-2 pb-2 mt-2 mb-2 border rounded border-dark' onClick={(e) => { handleSetRaiting(e) }}>Give raiting</Button>
                                </Form>
                            </Card.Footer>
                        </Card>
                    )}
                </Container>
            </div>
            <Footer />
        </>
    );
}
export default DetailAgentPage;