# EmploRecrutingProject

Rekrutacyjny projekt do zarządzania pracownikami i ich wnioskami urlopowymi, zbudowany w stylu Clean Architecture, zrealizowany nieco na overkillu – z pełnym rozdziałem warstw, wzorcami projektowymi i testami.

---

## Architektura
Projekt podzielony jest na cztery główne warstwy:

🔹 Application
- Komendy, zapytania, handler’y (CQRS z MediatR)

- Interfejsy dla serwisów i repozytoriów

🔹 Domain
- Encje i logika domenowa 

🔹 Infrastructure
- EF Core + MySQL

- Repozytoria, serwisy (np. VacationPolicyService)

- Wzorzec Unit of Work

- Seedy danych

- Konfiguracja encji z użyciem Fluent API (IEntityTypeConfiguration<>)

🔹 Presentation
- ASP.NET Core MVC jako frontend (Views, Controllers)

---

## 🧠Założenia i decyzje projektowe
CQRS z MediatR wybrane dla separacji zapytań i poleceń

Nie użyto ORM typu Dapper - skupienie na EF Core + testowalność

Brak AutoMappera - konwersje wykonane ręcznie dla lepszej kontroli

Logika typu „czy pracownik może wziąć urlop” zamknięta w serwisie – zasada SRP
