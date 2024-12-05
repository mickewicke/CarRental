För backend
Miljökrav 
- kunna köra .net 8 solution
- En sql-server att köra mot (behövs inte för tester)

Steg: 
För tester, endast nuger restore
För backend: 
Kör cold_start.sql som ligger i repots root-map
(dialekt t-sql)
uppdatera connectionsträngen i Bootstrapper.cs
projektet med din servers connection sträng

Frontend
Miljökrav:
Node (v?)
Steg: 
Kör npm install
npm start
