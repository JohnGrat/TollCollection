# Toll Collection API

This API is designed for automatic toll collection in Göteborg, Sweden, adhering to the regulations set by Transportstyrelsen. [Here](https://www.transportstyrelsen.se/sv/vagtrafik/Trangselskatt/Trangselskatt-i-goteborg/Tider-och-belopp-i-Goteborg/).

## Endpoints

### /api/v1/tollpassage

This POST endpoint is used for adding new toll passages.

#### Request Body

The `/api/v1/tollpassage` endpoint takes:

- RegistrationNumber
- DateTime Timestamp
- VehicleTypeName

`VehicleTypeName` can be one of the following:
- "Motorbike"
- "Tractor"
- "Emergency"
- "Diplomat"
- "Foreign"
- "Military"
- "Taxable"

`Taxable` vehicles are the only ones that will pay the toll; the rest do not pay.

### /api/v1/tollresults

This GET endpoint retrieves toll results within a specified time range.

It takes:

- DateTime From
- DateTime To

This system uses an SQLite database that is part of the repo. It utilizes layered architecture and the repository pattern. It also utilizes the Fast-Endpoints library [here](https://fast-endpoints.com/).

![image](https://github.com/JohnGrat/TollCollection/assets/51702387/dfd15dcf-48f9-4daa-a212-e1fe5682ec42)
