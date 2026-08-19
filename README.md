# ECommerce API

Backend RESTful API لمتجر إلكتروني، مبني بـ **ASP.NET Core** و **Clean Architecture**. المشروع بيغطي إدارة المنتجات، السلة، الطلبات، الدفع بـ Stripe، والـ Authentication بـ JWT.

## 🏗️ المعمارية (Architecture)

المشروع متقسم على طبقات (Clean Architecture) عشان الفصل بين الاهتمامات (Separation of Concerns) ويبقى قابل للاختبار والتوسع:

```
ECommerce/
│
├── Core/
│   ├── DomianLayer/          # الكيانات (Entities)، الـ Contracts، الـ Exceptions
│   ├── ServiceAbstraction/   # الـ Interfaces بتاعة الـ Services
│   └── Service/              # تنفيذ الـ Business Logic + CQRS Handlers (MediatR)
│
├── Infrastructure/
│   ├── Persistence/          # DbContext، Repositories، Migrations، Data Seeding
│   └── Presentation/         # الـ Controllers، الـ Attributes (Caching)
│
├── Shared/                   # DTOs، Error Models، Query Params (مشتركة بين الطبقات)
│
└── ECommerce/                 # المشروع الرئيسي (Web API) - Program.cs، Middlewares، DI Registration
```

**اتجاه الاعتمادية (Dependency Flow):**
`Presentation → ServiceAbstraction ← Service → DomainLayer ← Persistence`

كل طبقة بتعتمد على اللي جواها بس، والـ Domain Layer هو القلب اللي مفيهوش أي اعتمادية على حاجة تانية.

## ⚙️ التقنيات المستخدمة (Tech Stack)

| التقنية | الاستخدام |
|---|---|
| **.NET 8 / .NET 10** (Web host) | الـ Framework الأساسي |
| **ASP.NET Core Web API** | بناء الـ REST Endpoints |
| **Entity Framework Core** (SQL Server) | الوصول للداتا (ORM) |
| **ASP.NET Core Identity** | إدارة المستخدمين والـ Authentication |
| **JWT Bearer Authentication** | تأمين الـ Endpoints |
| **MediatR** | تطبيق CQRS على فيتشرز السلة |
| **AutoMapper** | التحويل بين الـ Entities والـ DTOs |
| **Redis (StackExchange.Redis)** | تخزين السلة (Basket) + Response Caching |
| **Stripe.NET** | معالجة الدفع الإلكتروني |
| **Swagger / Swashbuckle** | توثيق الـ API |

## 🧩 أهم الأفكار المعمارية في المشروع

- **Generic Repository + Unit of Work**: للتعامل مع الداتابيز بشكل عام بدل تكرار كود CRUD في كل Entity.
- **Specification Pattern**: لبناء الاستعلامات المعقدة (فلترة، ترتيب، تضمين علاقات) بشكل نظيف وقابل لإعادة الاستخدام (`ISpecification`, `BaseSpecification`).
- **CQRS (جزئي عبر MediatR)**: مطبّق على السلة (Basket) — كل عملية (Add/Update, Delete, Get) ليها Command/Query + Handler منفصل.
- **Two Separate DbContexts**: `StoreDbContext` للبيانات التجارية (منتجات، طلبات...) و`StoreIdentityDbContext` منفصل للـ Identity/Users.
- **Custom Caching Attribute**: `CacheAttribut` بيعمل caching للـ Response على مستوى الـ Action باستخدام Redis.
- **Global Exception Handling Middleware**: بيحوّل أي Exception (NotFound, BadRequest, Unauthorized...) لـ Response موحّد الشكل (`ErrorToReturn`).
- **Factory Pattern**: `ApiResponseFactory` لتوحيد شكل أخطاء الـ Validation.

## 📦 الموديولات الأساسية (Domain Modules)

- **Products**: Product, ProductBrand, ProductType
- **Basket**: Basket, BasketItem (متخزنة في Redis)
- **Orders**: Order, OrderItem, OrderAddress, DeliveryMethod, OrderStatus
- **Identity**: ApplicationUser, Address

## 🔌 أهم الـ Endpoints

| Controller | يعمل إيه |
|---|---|
| `AuthenticationController` | Login, Register, CheckEmail, CurrentUser, إدارة عنوان المستخدم |
| `ProductsController` | عرض المنتجات (Pagination + فلترة)، البراندات، الأنواع |
| `BasketController` | إضافة/تعديل/عرض/حذف السلة |
| `OrdersController` | إنشاء طلب، عرض الطلبات، طرق الشحن |
| `PaymentsController` | إنشاء/تحديث Payment Intent + Stripe Webhook |

## 🚀 طريقة التشغيل (Getting Started)

### المتطلبات
- [.NET SDK](https://dotnet.microsoft.com/download) (النسخة المطلوبة حسب `ECommerce.Web.csproj`)
- SQL Server (أو LocalDB)
- Redis Server (شغال على `localhost` أو حسب الـ connection string)
- حساب [Stripe](https://stripe.com/) (لو هتشتغل على الدفع) — API Keys

### خطوات التشغيل

1. **Clone المشروع**
   ```bash
   git clone https://github.com/Mahmoud-Amr11/ECommerce.git
   cd ECommerce
   ```

2. **إعداد الـ Connection Strings**

   عدّل `ECommerce/appsettings.json` (أو أفضل: استخدم `appsettings.Development.json` أو [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets)) وحط بياناتك الحقيقية بدل ما هو موجود في الريبو:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True",
       "IdentityConnection": "Server=.;Database=ECommerceIdentityDb;Trusted_Connection=True;TrustServerCertificate=True",
       "RedisConnection": "localhost"
     },
     "JWTOptions": {
       "SecretKey": "<Your-Secret-Key>",
       "Issuer": "http://localhost:5000",
       "Audience": "http://localhost:5000/api"
     },
     "stripe": {
       "SecretKey": "<Your-Stripe-Secret-Key>",
       "EndpointSecret": "<Your-Stripe-Webhook-Secret>"
     }
   }
   ```

   > ⚠️ ملاحظة مهمة: الـ `SecretKey` بتاع الـ JWT موجود حاليًا مكتوب صريح جوه `appsettings.json` في الريبو. لازم يتشال من الكود ويتحط في User Secrets أو Environment Variables أو Key Vault قبل أي Deployment حقيقي، ونفس الكلام لأي مفاتيح Stripe.

3. **تطبيق الـ Migrations**
   ```bash
   dotnet ef database update --project Infrastructure/Persistence/Persistence.csproj --startup-project ECommerce/ECommerce.Web.csproj --context StoreDbContext

   dotnet ef database update --project Infrastructure/Persistence/Persistence.csproj --startup-project ECommerce/ECommerce.Web.csproj --context StoreIdentityDbContext
   ```

4. **تشغيل المشروع**
   ```bash
   dotnet run --project ECommerce/ECommerce.Web.csproj
   ```

5. **افتح Swagger** على:
   ```
   http://localhost:5000/swagger
   ```
   هيظهرلك كل الـ Endpoints وتقدر تجربها مباشرة.

## 📁 ملاحظات إضافية

- الـ Data Seeding بيحصل تلقائي عند تشغيل التطبيق (`SeedingData` في `Program.cs`) — بيعمل seed للمنتجات والـ Identity الأساسية.
- فيه ملف `ECommerce.http` جاهز لتجربة الـ Requests مباشرة من الـ IDE (VS Code / Visual Studio).

## 🛠️ TODO / حاجات تستاهل تتظبط

- [ ] شيل الـ Secrets (JWT Key, Stripe Keys) من `appsettings.json` وحطهم في مكان آمن، وأضف `appsettings.json` لملف `.gitignore` أو استخدم secrets مختلفة لكل بيئة.
- [ ] وحّد استخدام CQRS/MediatR على باقي الـ Services (حاليًا مطبّق على الـ Basket بس).
- [ ] أضف طبقة Unit/Integration Tests.
- [ ] وضّح النسخة المطلوبة من .NET SDK في الـ README (المشروع الرئيسي على .NET 10 preview بينما باقي المشاريع على .NET 8).
