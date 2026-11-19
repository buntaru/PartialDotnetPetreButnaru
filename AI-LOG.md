# AI-LOG

Write down, in order, what you asked the AI. It is not a punishment: a missing or false log lowers the level D grade.

| Time (approx.) | What you asked (briefly) | What you kept | What you changed or rejected |
|---|---|---|---|
| _(fill in)_ | _(fill in)_ | _(fill in)_ | _(fill in)_ |

Vreau sa refactorizezi proiectul RoomHub folosind ca referinta structura proiectului InternHub, dar adaptata la cerintele din assignment. Nu include frontend sau integrarea AI.

Implementeaza Vertical Slice Architecture, cu fiecare operatie in propriul folder din Features. Pastreaza rutele si formatul raspunsurilor din contractul API.

Muta regulile de business din Services in Domain, folosind Rich Domain. Vreau ca entitatile sa aiba private setters si metode proprii pentru validari.

Inlocuieste BusinessException cu Result si Error, dupa modelul folosit in InternHub. Erorile trebuie mapate la HTTP 400, 404 si 409 folosind ProblemDetails.

Adauga regula R9: un participant poate avea maximum doua rezervari active in aceeasi zi UTC, indiferent de camera. Rezervarile anulate nu trebuie luate in calcul.

Verifica implementarea actuala si adapteaz-o pentru R10 si R11. Limita de durata trebuie sa depinda de capacitatea camerei, iar anularea sa foloseasca TimeProvider pentru verificarea celor 60 de minute.

Refactorizeaza codul pentru a folosi CQRS handlers, ICommandHandler si IQueryHandler. Vreau ca endpoint-urile sa fie thin, iar logica de orchestrare sa fie in handlers.

Muta CreateBooking din Features/Rooms in Features/Bookings si actualizeaza namespace-urile si dependintele necesare, fara sa modifici ruta HTTP.

Verifica unde sunt tratate erorile NotFound, Validation si Conflict dupa refactorizarea CQRS si daca mecanismul este compatibil cu Result pattern.