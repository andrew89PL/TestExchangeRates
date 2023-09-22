# TestExchangeRates
Testowa aplikacja pobierająca ostatnie kursy walut z API NBP

### Backend 
W podfolderze srv. 
Wykorzystuje Minimal API do zwracania wyników zapytania.  
Zapis wyników do bazy SQLite z wykorzystaniem Entity Fremewrok. 

### Frontend 
W podfolderze web.
Porosty bazowy komponent z wykorzystaniem React oraz axios do odpytaywania API backedndowego w celu pobrania danych.

Instalacja zależności 
```bash
  npm install
```
Uruchomienie z domyślnie włączonym HTTPS
```bash
  npm start
```
