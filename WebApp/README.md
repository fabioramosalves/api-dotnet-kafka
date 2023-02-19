cluster in kafka

https://confluent.cloud/environments/env-099735/clusters/lkc-r56rd9/topics/PassangerTopic/message-viewer



--------------------------

POST

https://localhost:7014/Topic/FlightTracking

{
  "flightTracking": {
    "airlineCode":"DL",
		"number":"2015",
		"scheduledDepartureDate":"2023-01-25T17:14:00",
		"departureAirportCode":"CUN",
		"arrivalAirportCode":"DTW",
		"departureTerminal":"3",
		"arrivalTerminal":"M",
		"status":"1",
		"departureLocalTime":"2023-01-25T17:14:00-05:00",
		"arrivalLocalTime":"2023-01-25T21:09:00-05:00",
		"logicalKeyString":"DL|2015|20230125|CUN"
  }
}

--------------------------------------

GET

https://localhost:7014/Topic/FlightTracking





-----------------Docker commands-------------------------
docker build .
docker run -d -p 7500:80 232e75db368f