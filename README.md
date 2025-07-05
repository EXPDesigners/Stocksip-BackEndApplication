# StockSip | Back-End Application #

## Summary

StockSip Platform API Application, made with Microsoft C#, ASP.NET Core, Entity Framework Core and MySQL persistence. It also illustrates Open-API documentation configuration and integration with Swagger UI. Key features include:

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

The Authentication Context is responsible for registering of new users and authentication of them in order to login in the application. Also, it handles the management of user roles (liquor store owners and providers).
This context includes the following features:

- User registration and login (sign-in and sign-up).
- Role-based access control.
- Password management (reset, change).
- Token-based authentication with JWT.

### Profile Management Context

The Profile Management Context is responsible for the capability of showing the information of the registered user and the ability of modify the information (name, email, location).
This context includes the following features:

- User profile creation and updates.
- Profile picture management.
- Plan details and benefits information.

### Payments and Subscriptions Context

This context handles payment processing and subscription management, allowing users to manage their billing and subscription plans. It includes the following features:

- Creation of a new account with a subscription plan.
- Payment processing for subscriptions.
- Subscription plan management for both of the target segments.

### Inventory Management Context

This context focuses on managing liquor stock across multiple warehouses, allowing users to track stock levels, add new products, and manage inventory efficiently. It includes the following features:

- Warehouse management (add, update, delete warehouses).
- Product management (add, update, delete products).
- Stock tracking (view current stock levels, update stock).
- Product exits and entries management.
- Product expiration date management.
- Product care guides management.

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

- Dashboard analytics for stock and sales.

### Order Monitoring and Operation Context

This context focuses on managing orders and operations, allowing users to track orders, manage suppliers, and ensure smooth operations. It includes the following features:

For liquor store owners:

- Order management (requests, tracking, and history).

For suppliers:

- Supplier management (view orders, update order status).
- Order tracking and history.