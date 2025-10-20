<h2 align="center"><strong>ProfisysApp – System zarządzania dokumentami</strong></h2>

<div align="center">
    <p>
      <img alt="Status" src="https://img.shields.io/badge/status-beta-blue">
      <img alt="Licencja" src="https://img.shields.io/badge/licencja-prywatna-lightgrey">
    </p>
    <p>
      <img alt="C#" src="https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white">
      <img alt=".NET 8" src="https://img.shields.io/badge/ASP.NET%20Core-512BD4?logo=.net&logoColor=white">
      <img alt="Entity Framework Core" src="https://img.shields.io/badge/Entity%20Framework%20Core-68217A?logo=nuget&logoColor=white">
      <img alt="SQLite" src="https://img.shields.io/badge/SQLite-003B57?logo=sqlite&logoColor=white">
      <img alt="Vue.js" src="https://img.shields.io/badge/Vue.js-4FC08D?logo=vue.js&logoColor=white">
      <img alt="Vite" src="https://img.shields.io/badge/Vite-646CFF?logo=vite&logoColor=white">
      <img alt="Pinia" src="https://img.shields.io/badge/Pinia-41B883?logo=vue.js&logoColor=white">
      <img alt="PrimeVue" src="https://img.shields.io/badge/PrimeVue-42A5F5?logo=primevue&logoColor=white">
      <img alt="Axios" src="https://img.shields.io/badge/Axios-5A29E4?logo=axios&logoColor=white"><br>
      <img alt="Toastification" src="https://img.shields.io/badge/Toastification-FF3E00?logo=javascript&logoColor=white">
      <img alt="Vue Router" src="https://img.shields.io/badge/Vue%20Router-35495E?logo=vue.js&logoColor=white">
      <img alt="REST API" src="https://img.shields.io/badge/REST%20API-009688?logo=swagger&logoColor=white">
    </p>
</div>

---

## 🎯 Cel projektu

Celem aplikacji **ProfisysApp** jest stworzenie systemu do zarządzania dokumentami, który umożliwia:

- Przechowywanie dokumentów w bazie danych i zarządzanie nimi.
- Pobieranie, importowanie i usuwanie dokumentów z poziomu frontendu.
- Bezpieczny dostęp do danych poprzez autoryzację JWT.
- Interaktywny interfejs użytkownika z Vue 3.
- Powiadomienia o stanie operacji poprzez Toastification.

---

## 👤 Rola użytkownika

| Rola  | Uprawnienia |
|-------|--------------|
| Administrator | Logowanie, pobieranie dokumentów, import CSV, usuwanie dokumentów, dostęp tylko po autoryzacji JWT. |

---

## 🧱 Stack technologiczny

### Backend:
- **ASP.NET Core 8** – API REST do zarządzania dokumentami.
- **C#** – logika biznesowa.
- **SQLite** – baza danych dla użytkowników i dokumentów.
- **JWT Authentication** – bezpieczne uwierzytelnianie i autoryzacja użytkowników.

### Frontend:
- **Vue 3** – interaktywny frontend.
- **Pinia** – zarządzanie stanem aplikacji.
- **Axios** – komunikacja z backendem.
- **Toastification** – powiadomienia dla użytkownika.

---

## 🧩 Moduły i funkcjonalności

| Moduł | Opis |
|-------|------|
| 🔐 **Logowanie** | Uwierzytelnianie użytkowników i przechowywanie tokenu JWT. |
| 📝 **Lista dokumentów** | Wyświetlanie wszystkich dokumentów w tabeli z wyszukiwaniem i możliwością sortowania. |
| 📂 **Import CSV** | Import danych z plików CSV do bazy danych. |
| 🗑 **Usuwanie dokumentów** | Usuwanie pojedynczych lub wszystkich dokumentów w bazie. |
| 📡 **Autoryzacja JWT** | Bezpieczny dostęp do endpointów API tylko dla zalogowanych użytkowników. |
| 💬 **Powiadomienia** | Toasty informujące o powodzeniu lub błędach operacji. |
| 🔄 **Routing** | Strony chronione i przekierowania na login w przypadku braku tokenu. |

---

## 📡 Endpointy API

| Metoda | Endpoint | Opis | Autoryzacja |
|---------|-----------|------|--------------|
| **POST** | `/api/Authentication/login` | Logowanie użytkownika i wygenerowanie tokenu JWT | ❌ |
| **POST** | `/api/Authentication/register` | Rejestracja nowego użytkownika. Niedostępne na frontend. | ❌ |
| **GET** | `/api/Documents/GetAllDocuments` | Pobranie wszystkich dokumentów z bazy danych | ✅ |
| **DELETE** | `/api/Documents/DeleteDocument/{id}` | Usunięcie pojedynczego dokumentu po ID | ✅ |
| **DELETE** | `/api/Documents/DeleteDocuments` | Usunięcie wszystkich dokumentów z bazy | ✅ |
| **POST** | `/api/CsvFiles/ImportCsv` | Import danych z plików CSV do bazy SQLite | ✅ |

✅ – wymaga tokenu JWT  
❌ – dostępny publicznie

---

## 📸 Zrzuty ekranu

| Widok | Podgląd |
|-------|----------|
| Strona logowania | <img src="./images/login.png" alt="Strona logowania" width="400"> |
| Lista dokumentów | <img src="./images/documents.png" alt="Lista dokumentów" width="400"> |

---

## 🔧 Instrukcja uruchomienia

### Backend:
1. Skonfiguruj plik `appsettings.json` z danymi do bazy danych. Jeśli korzystasz z `SQLite` nie zmieniaj nic:
```env
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=documents.db"
  }
}
```

2. W pliku konfiguracyjnym `./apps/server/Config/AppSettings.cs` w zmiennej `CLIENT_URL_ADDRESS` wpisz adres na, na którym pracuje twój Client:
```env
public string CLIENT_URL_ADDRESS { get; set; } = "http://localhost:5173";
```

3. Jeżeli pojawi sie problem z bazą danych wczytaj ostatnią migrację:
```env
dotnet ef database update
```

4. Uruchom projekt w Visual Studio lub za pomocą terminala. W ścieżce `./apps/server/` użyj komend:
```json
dotnet build
dotnet run
```

---

### Frontend
1. W ścieżce `./apps/client` zainstaluj zależności:
```env
npm install
```

2. W pliku konfiguracyjnym `.env.development` ustaw zmienną `VITE_API_BASE_URL` na adres twojego serwera:
```env
VITE_API_BASE_URL=http://localhost:5011/api
```

3. Uruchom aplikację:
```env
npm run dev
```
---

## 🔐 Autoryzacja
- Po zalogowaniu token JWT zapisywany jest w localStorage.
- Każde zapytanie do API wysyła nagłówek:
```env
Authorization: Bearer <token>
```
- Ochrona tras w Vue Router pilnuje, by użytkownik niezalogowany nie miał dostępu do widoków z dokumentami.

---

## 👨‍💻 Autor

![contributors badge](https://readme-contribs.as93.net/contributors/CzechU99/servicehUB)

---

> © 2025 ProfisysApp – Wszystkie prawa zastrzeżone

