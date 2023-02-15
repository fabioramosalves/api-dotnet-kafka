cluster in kafka

https://confluent.cloud/environments/env-099735/clusters/lkc-r56rd9/topics/PassangerTopic/message-viewer



--------------------------

POST

https://localhost:7014/Topic/Passenger

{
  "passenger": {
    "id": 8,
    "name": "Fabio Ramos Alves",
    "email": "fabio.ramos.alves@teste.com",
    "phoneNumber": "11953903260"
  }
}


--------------------------------------

GET

https://localhost:7014/Topic/Passenger