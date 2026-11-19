# DECISIONS

Answer **yourself**, in your own words, in 3–5 sentences per question.
Refer to concrete files and classes from your own repo (for example `Features/Bookings/CreateBooking/...`).
An unanswered question gives 0 points.

## 1. A rule you moved
Pick one business rule (R1–R8). In which file did it live in the starter and in which file does it live now? Why there?

R7 spre exemplu era initial in metoda create in bookingservice.cs, acum este in metoda create din domain/bookings/bookings.cs pt ca este o regula de business

## 2. Result instead of an exception
Why is `Result` a better choice than an exception for "the room is occupied"? What happens in your code when an `Error` is returned?

Result pattern ul este o alegere mai buna decat sa aruncam exceptii in cod pt ca ma ajuta sa interpretez erori de business si sa le diferentiez de erori ale aplicatiei

## 3. Who knows about EF Core?
Which classes in your project know about EF Core and which do not? What do you gain and what do you pay with Repository + Unit of Work?

_(your answer here)_

## 4. The flow of a request
Draw in text (ASCII arrows) the path of a `POST /api/rooms/{roomId}/bookings` request through your project, with the **real** names of files and classes, from the endpoint to the database.

ar fi trebuit sa fie : POST api/rooms/roomID/bookings -> CreateBooking/Endpoint.cs -> CreateBooking/Handler.cs -> Domain/Bookings/Booking.cs

## 5. What you rejected from what the AI gave you
Give a concrete example of code or an idea proposed by the AI that you corrected or rejected, and say why.

Mi s a sugerat sa pastrez CreateBooking in Features/Rooms si eu am decis sa il mut la bookings pt ca am considerat ca este mai potrivit sa stea langa celelalte comenzi de boolking

## 6. If you had 30 more minutes
What would you improve and what risk did you leave in the project?

Daca as mai fi avut 30 de minute as fi terminat cu siguranta de implementat ef core
