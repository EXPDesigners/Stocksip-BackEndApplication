# StockSip | Back-End Application #

## Summary

SockSip Platform API Application, made with Microsoft C#, ASP.NET Core, Entity Framework Core and MySQL persistence. It also illustrates Open-API documentation configuration and integration with Swagger UI. Key features include:

- Efficient management of liquor stock across multiple warehouses.
- User-friendly interface for tracking stock levels.
- Comprehensive sales analytics and reporting.
- Integration with suppliers for seamless order management.
- Support for multiple warehouses.

## Features

- RESTful API
- OpenAPI Documentation
- Swagger UI
- ASP.NET Framework
- Entity Framework Core
- Audit Creation and Update Date
- Custom Route Naming Conventions
- Custom Object-Relational Mapping Naming Conventions
- MySQL Database
- Domain-Driven Design

## Documentation

StockSip Platform includes its own documentation and it's available in the `docs` folder. It includes the following documentation:

- User Stories (available in [docs/user-stories.md](docs/user-stories.md)).
- C4 Model Software Architecture Diagram (available in [docs/software-architecture](docs/software-architecture.dsl)).

## Bounded Contexts

This version of StockSip Platform is divided into seven bounded contexts: Authentication, Profile Management, Payments and Subscriptions, Inventory Management, Alerts and Notifications, Reporting and Analytics and Order Monitoring and Operation.

### Authentication Context

The Authentication Context is responsible for creating profiles and authentication of them in order to login in the application.
This context includes the following features:

- User registration and login. 
- Role-based access control. 
- Password management (reset, change). 
- Token-based authentication. 
- Session management.

This context also includes an anti-corruption layer to communicate this bounded context with the Inventory Management Context.
The anti-corruption layer is responsible for managing the communication between the Authentication Context and the Inventory Management Context.
It offers the following capabilities to other bounded contexts:

- Validating user credentials. 
- Generating and validating authentication tokens. 
- Ensuring secure password storage and management. 
- Implementing role-based access control to restrict access to certain features based on user roles.

### Profile Management Context

The Profile Management Context is responsible for the capability of showing the information of the registered user and the ability of modify the information (username, email, location). Also, it handles the management of user roles (liquor store owners and providers).
This context includes the following features:

- User profile creation and updates. 
- Profile picture management. 
- Plan details and benefits information.

### Payments and Subscriptions Context

This context handles payment processing and subscription management, allowing users to manage their billing and subscription plans. It includes the following features:

- Payment processing for subscriptions.
- Subscription plan management for both of the target segments.

### Inventory Management Context

This context focuses on managing liquor stock across multiple warehouses, allowing users to track stock levels, add new products, and manage inventory efficiently. It includes the following features:

- Warehouse management (add, update, delete warehouses). 
- Product management (add, update, delete products). 
- Stock tracking (view current stock levels, update stock). 
- Count the purchased products and the total amount of the purchase. 
- Product exits and entries management. 
- Product expiration date management. 
- Product care guides management.

This context also includes an anticorruption layer to ensure that the inventory management system is secure and does not interfere with the core business logic of the application. Its capabilities include:

- Managing multiple warehouses and their stock levels. 
- Tracking product entries and exits. 
- Managing product expiration dates and care guides.

### Alerts and Notification Context

This context manages real-time stock updates and notifications, ensuring users are always informed about their inventory status. It includes the following features:

- Stock level alerts. 
- Expiration date notifications. 
- The creation of alerts for low stock levels. 

This context also includes an anticorruption layer to ensure that the alert and notification system is secure and does not interfere with the core business logic of the application. Its capabilities include:

- Monitoring stock levels and expiration dates. 
- Generating alerts for low stock levels or approaching expiration dates.

### Reporting and Analytics Context

This context provides detailed sales reports and analytics, helping users make informed decisions based on their stock and sales data. It includes the following features:

- Restock planning. 
- Care guides creation for liquor products. 
- Payment history tracking. 
- Loses and damages reporting. 
- Dashboard analytics for stock and sales.

### Order Monitoring and Operation Context



# Reference Documentation #

The following guides illustrates how to use some featrues concretely:

