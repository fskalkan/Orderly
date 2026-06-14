# Orderly API

Orderly, Clean Architecture prensiplerine uygun olarak geliştirilmiş bir sipariş yönetim API projesidir.

Bu proje; CQRS, MediatR, FluentValidation, Validation Pipeline, Repository Pattern, Unit of Work ve Global Exception Handling yapılarını uygulamak amacıyla geliştirilmiştir.

## Projenin Amacı

Orderly; ürün, müşteri ve sipariş süreçlerini yönetebilen bir backend API projesidir.

Projede ürün ve müşteri CRUD işlemleri yapılabilir, sipariş oluşturulabilir, sipariş oluşturulurken stok kontrolü sağlanabilir ve sipariş durumları güncellenebilir.

## Kullanılan Teknolojiler

* ASP.NET Core Web API
* C#
* Entity Framework Core
* Microsoft SQL Server
* MediatR
* CQRS Pattern
* FluentValidation
* Validation Pipeline
* Repository Pattern
* Unit of Work
* Global Exception Middleware
* OpenAPI / Scalar
* Git & GitHub

## Proje Yapısı

```text
Orderly
├── Orderly.API
├── Orderly.Application
├── Orderly.Domain
└── Orderly.Infrastructure
```

## Katmanlar

### Orderly.API

Controller, middleware, API request modelleri, OpenAPI/Scalar ve uygulama başlangıç ayarlarını içerir.

### Orderly.Application

CQRS yapısındaki command, query, handler, validator, repository interface'leri ve business logic akışını içerir.

### Orderly.Domain

Entity, enum ve temel domain modellerini içerir.

### Orderly.Infrastructure

Entity Framework Core DbContext, repository implementasyonları ve veritabanı işlemlerini içerir.

## Uygulanan Yapılar

### CQRS

Okuma ve yazma işlemleri ayrı command/query yapıları ile yönetilmiştir.

Örnekler:

* `CreateProductCommand`
* `GetProductsQuery`
* `CreateCustomerCommand`
* `GetCustomersQuery`
* `CreateOrderCommand`
* `GetOrdersQuery`
* `UpdateOrderStatusCommand`

### MediatR

Controller'lar doğrudan business logic çalıştırmaz. İstekler MediatR üzerinden ilgili handler'a yönlendirilir.

### Validation Pipeline

FluentValidation kuralları MediatR pipeline üzerinden otomatik çalıştırılır. Böylece validation kontrolleri handler'a girmeden önce merkezi olarak yapılır.

### Global Exception Handling

Uygulama genelindeki hatalar `ExceptionMiddleware` ile yakalanır ve standart JSON response formatında client'a döndürülür.

### Unit of Work

Veritabanı değişiklikleri tek noktadan `SaveChangesAsync` ile yönetilir.

## Örnek Sipariş Oluşturma Request'i

```json
{
  "customerId": 1,
  "items": [
    {
      "productId": 2,
      "quantity": 2
    }
  ]
}
```

## Örnek Response

```json
{
  "id": 1,
  "customerId": 1,
  "totalAmount": 5599.98,
  "status": "Pending",
  "orderDate": "2026-06-14T12:00:00",
  "items": [
    {
      "productId": 2,
      "productName": "Updated Mouse",
      "quantity": 2,
      "unitPrice": 2799.99,
      "totalPrice": 5599.98
    }
  ]
}
```

## Geliştirici

Ferhat Samet Kalkan
