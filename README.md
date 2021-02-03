# DrivingTripsCQRS

DrivingTripsCQRS is a console app built with .NET Core that reads AddDriver and AddTrip commands, persists domain entities to their stores, and queries a materialized view store for a report on all drivers.

Example command input:

```
Driver Dan
Driver Lauren
Driver Kumi
Trip Dan 07:15 07:45 17.3
Trip Dan 06:12 06:32 21.8
Trip Lauren 12:01 13:16 42.0
```

Expected query output:

```
Lauren: 42 miles @ 34 mph
Dan: 39 miles @ 47 mph
Kumi: 0 miles
```

This application uses the CQRS pattern to separate the logic between Commands (writes) and Queries (reads). CQRS pattern enforces the Single Responsibility Principle (the S in SOLID) by design and allows for designing a loosely coupled architecture.

**CQRS offers the following benefits:**

 - Scaling: it allows the read and write workloads to scale independently
 - Optimized data schema: read side can use schema that is optimized for queries, while the write side uses schema that is optimized for updates
 - Separation of Concerns: segregating the read and write sides can result in models that are more maintainable and flexible. More complex business logic goes into the write model, and read model can be relatively simple
 - Simple Queries: by storing a materialized view in the read store, the application can avoid complex joins when querying
- Messaging: CQRS is commonly used with messaging to process Commands and publish update Events

**Challenges of using CQRS pattern are:**

 - Complexity: CQRS can lead to more complex application design
 - Eventual Consistency: by separating the read and write stores, the read data may get stale. Due to this the read store must be updated to reflect the changes to the write model, which may cause delay between command being processed and the data store being updated

**DrivingTripsCQRS unit tests:**

DrivingTripsCQRS.Tests contains unit tests of several components of the DrivingTripsCQRS application. These unit tests perform isolated and focused testing of components that have external dependencies by mocking external dependency state or behavior.
