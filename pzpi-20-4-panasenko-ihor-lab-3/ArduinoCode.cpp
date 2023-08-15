#include <WiFi.h>
#include "DHTesp.h"
#include <HTTPClient.h>
#include <ArduinoJson.h>
#include <RTClib.h>
#include <PubSubClient.h>

//#include <Adafruit_Sensor.h>

const char* serverUrl = "https://localhost:7082/api/PeriodicMeasurments/Create";

const char* ssid = "Wokwi-GUEST";
const char* password = "";

const int DHT_PIN1 = 15;
const int DHT_PIN2 = 2;

// Define the LED pin
const int ledPin = 12;

const char* userId = "647a5f29-cdea-41db-afc3-d3fb4425e7a1";

const unsigned long measurementInterval = 100000;
unsigned long lastMeasurementTime = -1*measurementInterval;
const int minPulse = 40;
const int maxPulse = 200;

// MQTT broker details
const char* mqttServer = "mqtt.eclipse.org";
const int mqttPort = 1883; // MQTT default port
const char* mqttUsername = "simatic";
const char* mqttPassword = "12345";
char* topic = "topic1";

StaticJsonDocument<200> measurementData;
DHTesp dhtSensor1;
DHTesp dhtSensor2;
RTC_DS1307 rtc;
WiFiClient wifiClient;

PubSubClient client(wifiClient);

void setup() {
  Serial.begin(115200);
  pinMode(ledPin, OUTPUT);
  dhtSensor1.setup(DHT_PIN1, DHTesp::DHT22);
  dhtSensor2.setup(DHT_PIN1, DHTesp::DHT22);
  connectToWifi();

   if (! rtc.begin()) {
    Serial.println("Couldn't find RTC");
    Serial.flush();
    abort();
  }

  // Connect to MQTT broker
  client.setServer(mqttServer, mqttPort);
  client.setCallback(callback);
  //connectToMqtt();
}

void loop() {
    unsigned long currentTime = millis();

    if (currentTime - lastMeasurementTime >= measurementInterval) {
      lastMeasurementTime = currentTime;
      char* measurementDataJson = getDataFromSensors();
      Serial.println(measurementDataJson);
      Serial.println("----------------------");
      sendMeasurementData(measurementDataJson);
      delay(1000);
    }
    delay(10000);
}

void connectToWifi(){
  WiFi.begin(ssid, password);

  while(WiFi.status() != WL_CONNECTED){
    digitalWrite(ledPin, LOW);
    Serial.println("Connecting to Wi-Fi...");

    for(int i=0; i<6; i++){
      digitalWrite(ledPin, HIGH);
      delay(400);
      digitalWrite(ledPin, LOW);
      delay(400);
    }
  }

  Serial.println();
  Serial.println("Connected to Wi-Fi.");
  digitalWrite(ledPin, HIGH);
  Serial.println(WiFi.localIP());
}

void connectToMqtt(){
  while (!client.connected()) {
    Serial.println("Connecting to MQTT broker...");

    if (client.connect("ESP32Client", mqttUsername, mqttPassword)) {
      Serial.println("Connected to MQTT broker");
    } 
    else {
      Serial.print("Failed, rc=");
      Serial.print(client.state());
      Serial.println(" Retrying in 5 seconds...");
      delay(5000);
    }
  }
}


int getRandomNumber(int minVal, int maxVal) {
  randomSeed(analogRead(0)); 
  int randomNumber = random(minVal, maxVal + 1);
  return randomNumber;
}

String getFormattedDateTime() {
  String dateTime;
  DateTime now = rtc.now();
  dateTime = String(now.year()) + "-" + String(now.month()) + "-" + String(now.day()) + "T";
  dateTime += String(now.hour()) + ":" + String(now.minute()) + ":" + String(now.second())+"Z";
  return String(dateTime);
}

char* getDataFromSensors(){
  TempAndHumidity dataSensor1 = dhtSensor1.getTempAndHumidity();
  TempAndHumidity dataSensor2 = dhtSensor1.getTempAndHumidity();
  measurementData.clear();
  measurementData["pulse"] = getRandomNumber(minPulse+dataSensor1.temperature, maxPulse);
  measurementData["glucose"] = dataSensor1.humidity;
  measurementData["cholesterol"] = dataSensor2.temperature;
  measurementData["bloodPreasure"] = dataSensor2.humidity;
  measurementData["measurementDate"] = getFormattedDateTime();
  measurementData["userId"] = userId;
  char measurementDataJson[1000];
  serializeJson(measurementData, measurementDataJson);
  Serial.println(measurementDataJson);
  return measurementDataJson;
}

void sendMeasurementData(char* data) {
  client.publish(topic, data);
  delay(5000);
}

void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Message arrived on topic: ");
  Serial.println(topic);
  Serial.print("Message: ");

  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }

  Serial.println();
}

void reconnect() {
  while (!client.connected()) {
    Serial.println("Reconnecting to MQTT broker...");

    if (client.connect("ESP32Client", mqttUsername, mqttPassword)) {
      Serial.println("Connected to MQTT broker");
      client.subscribe(topic);
    } 
    else {
      Serial.print("Failed, rc=");
      Serial.print(client.state());
      Serial.println(" Retrying in 5 seconds...");
      delay(5000);
    }
  }
}