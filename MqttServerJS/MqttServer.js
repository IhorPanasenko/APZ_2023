const mqtt = require('mqtt');
const axios = require('axios');
const https = require('https');
https.globalAgent.options.rejectUnauthorized = false;

const mqttBroker = 'mqtt://172.29.208.1:1883';
const mqttTopic = 'topic1';
const mqttOptions = {
  clientId: '1',
  username: 'simatic',
  password: '12345',
};

const httpClient = axios.create({
  httpAgent: new https.Agent({ rejectUnauthorized: false }),
});

const applicationServerEndpoint = 'https://localhost:7082/api/PeriodicMeasurments/Create';

const mqttClient = mqtt.connect(mqttBroker, mqttOptions);

mqttClient.on('connect', () => {
  console.log('Connected to MQTT broker');

  mqttClient.subscribe(mqttTopic, (error) => {
    if (error) {
      console.error(`Failed to subscribe to MQTT topic: ${mqttTopic}`);
    } else {
      console.log(`Subscribed to MQTT topic: ${mqttTopic}`);
    }
  });
});

mqttClient.on('message', async (topic, message) => {
  console.log(`Received message on topic: ${topic}`);
  const payload = message.toString();
  console.log(`Message: ${payload}`);
  await sendPayloadToServer(payload);
});

async function sendPayloadToServer(payload) {
  try {
    const response = await httpClient.post(applicationServerEndpoint, payload, {
      headers: {
        'Content-Type': 'application/json',
      },
    });

    console.log('HTTP POST request succesfully sent to the application server');
  } catch (error) {
    console.error('Failed to send HTTP POST request to the application server:', error);
  }
}

console.log('MQTT service started.');