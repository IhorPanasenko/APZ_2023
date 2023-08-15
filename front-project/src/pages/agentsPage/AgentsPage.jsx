import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import { Container, Card } from 'react-bootstrap';
import { useHistory, useNavigate } from 'react-router-dom';
import Footer from '../../components/footer/Footer';
import UserHeader from '../../components/headers/userHeader/UserHeader';
import './AgentsPage.css'; 

const URL_GET_ALL_AGENTS = "https://localhost:7082/api/Agent/GetAll";

const AgentsPage = () => {
  const { t } = useTranslation();
  const [agents, setAgents] = useState([]);
  const navigate = useNavigate();

  const getAgents = async () => {
    try {
      const response = await axios.get(URL_GET_ALL_AGENTS);
      setAgents(response.data);
      console.log(response.data);
    } catch (error) {
      console.error('Error fetching agents:', error);
    }
  };

  useEffect(() => {
    getAgents();
  }, []);

  const handleCardClick = (agentId) => {
    console.log(agentId);
    localStorage.setItem("agentId", agentId);
    navigate(`/Agents/${agentId}`);
  };

  return (
    <>
      <UserHeader />
      <div className="bg-info">
        <Container className="pt-3 pb-5 bg-info">
          <h1 className="my-4">{t('insuranceAgentsPage.title')}</h1>
          {agents.map((agent) => (
            <Card
              key={agent.id}
              className="my-3 bg-warning border border-dark rounded border-2 agent-card"
              onClick={() => handleCardClick(agent.id)}
            >
              <Card.Body>
                <Card.Title>{`${agent.firstName} ${agent.secondName}`}</Card.Title>
                <Card.Text>
                  <strong>{t('insuranceAgentsPage.company')}:</strong>{' '}
                  <a href={`/companies/${agent.company.id}`}>{agent.company.companyName}</a>
                </Card.Text>
                <Card.Text>
                  <strong>{t('insuranceAgentsPage.phoneNumber')}:</strong> {agent.phoneNumber}
                </Card.Text>
                <Card.Text>
                  <strong>Raiting:</strong> {agent.raiting}/10
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

export default AgentsPage;