# Masraf Ödeme Sistemi

## Patika ve Akbank tarafından düzenlenen .NET Bootcamp'ine ait bitirme projesi olan uygulama


### Geliştirme Amacı
Sahada çalışan personele ait masrafların takip edilmesi ve yönetimi için geliştirdiğim uygulama, personele sahada çalıştıkları süre zarfında masraflarını girebilecekleri
ve aynı zamanda yöneticilerin de vakit kaybetmeden harcamayı onaylayıp, gecikme yaşamadan personele ödeme yapabilme imkanı sunuyor. 

Masraflar ile alakalı; her bir masraf için dökümanların ayrı ayrı ele alınması Çalışanın evrak, fiş, fatura toplamak zorunda kalması durumunu da epey kolaylaştırıyor. 


# API Dökümantasyonu
https://documenter.getpostman.com/view/28176839/2s9YsT7USt

# Uygulamanın çalıştırılması
Migrationlar FinalCase.Data projesinde yer alır,  FinalCase.Api projesinde yer alan ``appsettings.json`` üzerinden sql server ve diğer alanlar için gerekli alanları belirtmelisiniz.
EmailFunctions projesi de RabbitMq için queue dinlemeye ihtiyaç duyar içerisinde yer alan ``appsettings.json`` dosyasına değerler uygun şekilde eklenmelidir.

Migrationları database e uygulamak için 

solution dizininde
```cs
dotnet ef database update --project "./FinalCase.Data" --startup-project "./FinalCase.Api"
```
komutunu uygulayabilirsiniz. 

<br>
FinalCase.Api & EmailFunction & BankingSystem birlikte çalışıyor olmalıdır. 

```
dotnet run ./EmailFunctions
dotnet run ./BankingSystem
dotnet run ./FinalCase.Api
```
komutlarını uygulayabilirsiniz.


# Roller
Uygulama Admin ve Employee olarak 2 farklı rol içerir.

ApplicationUser ismiyle veri tabanı üzerinde tutulan bir tabloda kullanıcı kayıtları yer alır. 
Hassas bilgiler, örneğin şifre doğrudan değil şifrelenerek tutulmaktadır.

Roller uygulama seviyesinde birbirinden ayrılmıştır. Bu sayede yönetici ile alakalı istekler gönderildiğinde, employee için kullanılırken yönetici için kullanılmayan Iban alanı ile alakalı veri dönülmez.


# Mimari Tasarım

Projeyi oluştururken, bootcamp sürecinde kullanmış olduğumuz proje yapısını uyguladım ve geliştirmeler yaptım.

<img title="project-foldering" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/561e87da-5da1-496a-a99b-8c3792456cba" width="300" height="130" alt="foldering"  />

<br>

Robert C. Martin'in öne sürdüğü "Screaming Architecture" kavramı, bir binanın veya bir havaalanının mimari yapısının uzaktan bakıldığında kendi amacını açıkça ifade ettiği gibi,
yazılımın da benzer bir açıklık ve bütünlük ile kendi yapısını ortaya koymasının faydalı olduğu fikrini ifade eder. 

<img title="folder" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/71c16a62-273a-47b7-96b1-4c6689833346" width="200" height="350" alt="foldering"  />

Proje kapsamında gerekli işlevler birbirinden ayrı olarak birer feature olarak CQRS ve Mediator tasarım desenleriyle birlikte uyguladım.
Mediator implementasyonu için yaygın kullanılan bir kütüphane olan MediatR kullandım.


Okuma yapılan kısımlarda Entity Framework'ü tracking mekanizmasını da göz önüne alarak kullanmaya ve projection yapmaya çalıştım.
Yalnızca okuma gerçekleştirilen her bir feature'ı, okumaya özgü optimize etmeye çalıştım. Böylelikle CQRS in uygulamadaki varlığı da arttırılmış oldu.

<br> 
Method çağrıları yapılırken ReadOnly operasyonlarda ``AsNoTracking()`` ve ``AsNoTrackingWithIdentityResolution()`` gibi methodlar sorgular ile birlikte kullandım. 

Veri tabanından yüklü miktarda veri okumak yerine, ``Select()`` ve AutoMapper kütüphanesinin queryable extensionları arasında yer alan ``ProjectTo<>()`` extension method'u kullanarak projection yaptım.

<img title="class-diagram" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/04e3dacf-19d8-4edc-8086-1f73e373ca28" width="400" height="80" alt="foldering"  />
<img title="class-diagram" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/dd0393f9-7357-4101-a469-c6c2c3dec846" width="500" height="80" alt="foldering"  />

Bu noktada ``Select()`` methodunun sonrasında ``AsNoTracking()`` methodunun çağrılamadığını fakat ``ProjectTo()`` methodunun buna müsade ettiğini,
bu yüzden; olası bir performans avantajı elde etme düşüncesiyle 2. görselde görülebileceği gibi bir çağrı yaptığımı belirtmek isterim.


<hr>

## Class Diagram

<img title="class-diagram" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/8ad776b8-7769-4524-996f-32c622824a31" width="650" height="350" alt="foldering"  />

#### Payment
Raporlama, join işlemlerini azaltma ve ödeme oluşturma gibi işlevler için oluşturduğum class.

#### PaymentMethod
Ödeme yöntemleri ile alakalı class yapısı

#### ExpenseCategory
Masraf kategorileri için oluşturuldu.

#### Documents
Dökümanlar için oluşturuldu.

#### Expense 
Harcamalar için oluşturduğum class.

#### ApplicationUser
Kullanıcılar için oluşturuldu, role için string türünde bir property içerir.


<hr>

### Entity Configurations

``BaseEntity`` ile alakalı konfigürasyonları tek bir noktada konumlandırmak için ``BaseEntityConfiguration`` abstract class olarak oluşturuldu  
Configure methodu kendisini kalıtım alan classlar tarafından override edilebilmesi için virtual olarak işaretlendi, türeyen classlar bu method içerisinde, base de yer alan implementasyonu çağırabilir ve bunun yanında 
kendi implementasyonlarını (bizim durumumuzda konfigürasyonlarını) gerçekleştirebilir.


<img title="class-diagram" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/e89fb061-b804-4612-a82c-b7892bd01a6c" width="650" height="350" alt="foldering"  />



# Kullanıcılara Özgü İşlemler


## Saha personelleri masraf girişini, yalnızca kendileri üzere olmak için gerçekleştirebiliyor. 

Json Web Token yapısından faydalanarak bu özelliği ekledim. ``Claims`` üzerinden kullanıcı rol ve identifier bilgilerine erişerek, yapıyı kurguladım.

```cs
[HttpPost]
[Authorize(Roles = Roles.Employee)]
public async Task<ApiResponse<ExpenseResponse>> CreateExpense([FromBody] ExpenseRequest request)
{
    var (employeeId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity); // to add InsertUserId
    var operation = new CreateExpenseCommand(employeeId, request);
    return await mediator.Send(operation);
}
```


``GetUserIdAndRoleFromClaims`` isimli tuple dönen bir static helper oluşturarak JWT üzerinden id ve role okuması gerçekleştirdim. Bu methodu gerektiği durumda çağırarak kullanıcıdan expense oluşturma sırasında EmployeeId ile alakalı değer girişi yapmasını beklemeden, kullandığı tokenı okuyarak id değerini expense oluşturma esnasında geçebiliyorum.

```cs
namespace FinalCase.Api.Helpers;
public static class ClaimsHelper
{
    /// <summary>
    /// Get the user id and role from the claims if they exist
    /// </summary>
    /// <param name="identity">The claim identity</param>
    /// <param name="idClaimType">ID claim </param>
    /// <param name="roleClaimType">Role claim </param>
    /// <returns>the user id and role, tuple</returns>
    public static (int UserId, string Role) GetUserIdAndRoleFromClaims(ClaimsIdentity identity,
        string idClaimType = JwtPayloadFields.Id, string roleClaimType = ClaimTypes.Role)
    {
        var idClaim = identity.FindFirst(idClaimType);
        var roleClaim = identity.FindFirst(roleClaimType);

        if (idClaim == null || roleClaim == null)
            throw new ArgumentException("Invalid Claims");

        return (int.Parse(idClaim.Value), roleClaim.Value);
    }
}
```

JWTPayload için ise doğrudan string değerler kullanmak yerine ayrı bir class oluşturdum.

```cs
namespace FinalCase.Business.Features.Authentication.Constants.Jwt;
public static class JwtPayloadFields
{
    // created to prevent magic strings
    // if values changed somehow, we can change here and it will be reflected everywhere
    // if the value deleted, then it will be a compile time error. So, we can't forget to change it everywhere
    public const string Id = "Id";
    public const string Email = "Email";
    public const string Username = "Username";
}
```

Roller de benzer şekilde constant string olarak ele alınıyor.

```cs
namespace FinalCase.Business.Features.Authentication.Constants.Roles;
public static class Roles
{
    public const string Admin = "admin";
    public const string Employee = "employee";
}
```

## Personel sadece kendi masraf tanımlarını görebiliyor
Yine bu özelliği de benzer şekilde ekledim. 

``ExpensesController.cs``

```cs
[HttpGet]
[Authorize(Roles = $"{Roles.Employee},{Roles.Admin}")]
[EmployeeIdFromQueryAuthorize]
public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetByParameter([FromQuery] GetExpensesQueryParameters parameters)
{
    var operation = new GetExpensesByParameterQuery(parameters);
    return await mediator.Send(operation);
}

[HttpGet("{id:min(1)}")]
[Authorize(Roles = $"{Roles.Employee},{Roles.Admin}")]
public async Task<ApiResponse<ExpenseResponse>> GetById(int id)
{
    var (userId, role) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);
    var operation = new GetExpenseByIdQuery(userId, role, id);
    return await mediator.Send(operation);
}

```
<br>

``GetExpenseByIdQueryQueryHandler.cs``


<img title="class-diagram" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/73b0ff69-d6a5-448a-942b-af6ffc38f0c8" width="650" height="350" alt="foldering"  />

Her bir Feature altında yer alacak şekilde; hata mesajları constant string olarak tutuluyor.


```cs
namespace FinalCase.Business.Features.Expenses.Constants;
public static class ExpenseMessages
{
    public const string ExpenseNotFound = "Expense not found.";

    public const string CompletedUpdateError = "Cannot update a completed expense";
    public const string RejectedUpdateError = "Cannot update a rejected expense";
    public const string OnlyPendingUpdateError = "Only the 'pending' status is allowed to update an expense";

    public const string PaymentMethodNotFound = "The specified Payment Method was not found.";
    public const string CategoryNotFound = "The specified Category was not found.";

    public const string ExpenseAlreadyApprovedError = "Some expenses have already been approved or rejected. Only pending expenses can be requested. Related Ids: {0}";
    public const string ExpenseToApprovedNotFoundError = "Some expenses that were requested for approval were not found. Related Ids: {0}";


    public const string UnauthorizedExpenseUpdate = "You do not have permission to update this expense.";
    public const string UnauthorizedExpenseDelete = "You do not have permission to delete this expense.";
    public const string UnauthorizedExpenseRead = "You do not have permission to access this expense.";
}

```


## Personel taleplerini ilgili kriterlere göre filtreleyebilir.

Filtreleme için daha önce derste bootcamp sürecindeki derslerimizde de kullandığımız LinqKit den faydalandım

``ExpensesController.cs``

```cs
[HttpGet]
[Authorize(Roles = $"{Roles.Employee},{Roles.Admin}")]
[EmployeeIdFromQueryAuthorize]
public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetByParameter([FromQuery] GetExpensesQueryParameters parameters)
{
    var operation = new GetExpensesByParameterQuery(parameters);
    return await mediator.Send(operation);
}
```

``GetExpenseByParameterQuery.cs``

<img title="class-diagram" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/4a90a6ac-47ae-40a1-a094-5671efe3f98b" width="500" height="280" alt="foldering"  />


<br>
<br>

``GetExpenseByParameterQueryHandler.cs``

<img title="" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/a01ebfd3-bdbe-48c1-bc08-aab3b2c2d306" width="650" height="350" alt="foldering"  />




## Personel ret olan talepleri için neden ret olduklarına dair açıklama görebilir. 
``api/Expenses/reject`` söz konusu endpoint expense id ve admin description'ı dizi olarak alır bu sayede reddedilen her bir harcama için red sebebi eklenmesi mümkündür.

<img title="" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/ff46b219-9c6f-4890-903c-4800ec49918b" width="650" height="350" alt="foldering"  />



## Onaylanan talepler için ödeme banka hesabına anında geçecek bir hayali ödeme sistemi tasarlanabilir. Admin rolüne sahip kullanıcılar tüm personelin taleplerini görebilir ve talepleri değerlendirip onaylayıp otomatik ödeme talimatı girişi sağlar ya da bir neden ile talebi reddederler.

Banka sistemini simüle etmek adına bir başka api projesi oluşturdum. Bu proje bir json dosyası üzerinden gelen ödemelere ait bilgileri tutuyor. 

Saha personeli tarafından oluşturulan ödeme, bir yönetici tarafından onaylandığında;

1. Expense in status bilgisi approved a çekilirken aynı zamanda payment da oluşturulur, tüm bunlar savechanges öncesi gerçekleştirildiği için tek bir transaction oluşturulur, atomicity de söz konusu (ya hep ya hiç !)

``ApproveExpensesCommandHandler.cs``

<img title="" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/5c6242e7-cc50-4e16-8ef3-60b1a8e244d0" width="1400" height="570" alt="foldering"  />

Oluşturmuş olduğum constant stringleri hata mesajı şeklinde dönen methodlar da mevcut, hata dönerlerse ``string.Join()`` methodu ile ``-`` işaretini aralara ekleyerek her birini birleştiriyor. Böylece daha sonra istenirse
Front end tarafında split edilerek kullanılabilir.

<img title="" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/54965467-4204-4c2f-9d44-56fa03585d66" width="1400" height="570" alt="foldering"  />

2. SendPayment methodu
Method Handler içerisinde oluşturulmuş Payment objelerini kullanarak banka simülasyonuna atılacak request i oluşturur. Her bir payment a karşılık;
request oluşturulacak, ödeme sonrası personele iletmek adına mail objesi oluşturulacak ve job için method injection vasıtası ile notificationService inject edilecektir.

<img title="" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/244ca2d9-f54e-44b5-922b-461c45a01eb8" width="1000" height="400" alt="foldering"  />


Sonrasında bir job oluşturulacak ve kullanıcıya eğer her şey yolunda ise 200 mesajı dönülecektir. Gerekli bilgileri almış olduğumu ve artık admin tarafından gelen onay isteğini daha fazla bekletmeye ihtiyaç 
olmadığını düşündüğüm için kalan kısmı job ile devam ettirdim. 


```cs
 /// <summary>
 /// Schedules a job to send a payment request to the banking system and continues with sending if the job is completed.
 /// </summary>
 /// <param name="request">The outgoing payment request.</param>
 /// <param name="email">The email to be sent.</param>
 /// <param name="notificationService">The service instance managing notifications.</param>
 /// <param name="cancellationToken">Cancellation Token.</param>
 public static void SendPaymentRequest(OutgoingPaymentRequest request, Email email, INotificationService notificationService, CancellationToken cancellationToken)
 {
     var sendPayment = BackgroundJob.Schedule(() => SendPaymentJobAsync(request, cancellationToken), TimeSpan.FromSeconds(3)); // Schedule a job to send the payment request to the banking system.
     var sendEmail = BackgroundJob.ContinueJobWith(sendPayment, () => notificationService.SendEmail(email)); // Schedule a job to send the email to the employee.        
     BackgroundJob.ContinueJobWith(sendEmail, () => CompletePayment(request.Description, cancellationToken));
 }
```

SendPaymentJob, Sık kullanılan bir kütüphane olan Restsharp vasıtası ile banka simülasyonuna istek gönderir (banka simülasyonunun kendi ödemelerimiz hakkında bilgi alabilmemiz ve ödeme yapabilmemiz için bize bir api sağladığını varsayıyoruz). 

<br>

İsteğin ödeme açıklamasında payment a ait id bulunur, banka simülasyonu id değerini kayıt ettiyse. Ödeme tamamlandı demektir. Idempotency bakımından düşünerek, resilience ile alakalı bir sunucu olursa (network güvenilir değildir) aynı ödeme 2. kez gönderilebilir. Bunun önüne geçmek için job önce bir post isteği gönderir ve payment a ait id yi sorar(simülasyon güvenlik gerekçesiyle get yerine post ile ödeme bilgilerini almayı tercih ediyor). Not Found yanıtı gelirse bir istek daha gönderir fakat bu kez ödeme ile alakalı endpoint e istek gönderecektir.

<img title="" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/e271abca-d8f9-4ec0-9b31-41a0678ccde5" width="1000" height="500" alt="foldering"  />

ilk iş başarı ile sonuçlanır ise 2. iş ile süreç devam eder

``queueNotificationService`` üzerinden RabbitMq vasıtasıyla alakalı Email için oluşturulmuş bir queue ya mesaj gönderilir. 3rd party bir abonelik sistemi ya da bir başka herhangi bir servis mail gönderimi için kullanılabileceğinden,
INotification servis isimli interface ve içerisindeki method imzası üzerinden gideriz.

RabbitMq yu dinleyen ``EmailFunction`` isimli bir konsol uygulaması mevcuttur. Mail gönderimi için Serverless çözümler de tercih edilebilirdi. Azure functions, AWS Lambda + AWS SNS gibi..

``EmailFunction`` email için oluşturulan queue yu dinler ve mail gönderir, ayn zamanda mail içeriğini konsol ekranına yazdırmaktadır.

FinalCase uygulaması ise queue ya mesajı ilettikten hemen sonra mail gönderimini beklemeksizin, Dapper aracılığıyla bir stored procedure çalıştırarak ``Payments``tablosunda ilgili payment a ait değeri completed olarak değiştirir.

```cs
/// <summary>
///  Updates the payment status in the database.
/// </summary>
/// <param name="description">payment description(The payment id)</param>
public static async Task CompletePayment(string description, CancellationToken cancellationToken)
{
    var parameters = new DynamicParameters();
    parameters.Add("@Id", int.Parse(description));

    await DapperExecutor.ExecuteStoredProcedureAsync(StoredProcedures.CompletePayment, parameters, configuration.GetConnectionString(DbKeys.SqlServer), cancellationToken);
}
```


# Raporlar

Rapor oluşturmak için stored procedure ve viewlardan yararlandım. Dapper üzerinden isteğe bağlı olarak ilgili raporlar sırasıyla elde edilebilir. Günlük haftalık ve aylık ödeme yoğunluğu bir job tarafından otomatik olarak belirli saat ve zamanlarda çalışarak, adminlere ait maillerin yer aldığı bir view e erişerek. Kayıtlı olan adminlere bu raporları mail yoluyla gönderir. 

API ile alakalı dökümantasyonda Raporlar ile ilgili bilgiler de yer almaktadır.

StoredProcedure ve View isimleri constant stringler olarak ayrı birer dosyada tutuluyor. Dapper üzerinde basit şekilde view ve stored procedure işletmek için bir static method da oluşturdum.

<img title="" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/4e156eff-c801-42ae-88c5-747086916b9b" width="1000" height="500" alt="foldering"  />

Document ile alakalı sorgu sonuçlarında tekrarlı veriler bulunduğunu, bunu aşmak için aşağıdaki söz konusu makaleden de faydalandığımı söylemeliyim.

https://medium.com/@nelsonciofi/the-best-way-to-store-and-retrieve-complex-objects-with-dapper-5eff32e6b29e

Bununla birlikte view çalıştırmak için oluşturdum method view değerini string olarak aldığı için, her ne kadar constant string geçerek kullanıyor olsam da. Sorguya doğrudan string ekleyen bir method, çok doğru görünmediği için 
reflection kullanan bir method da oluşturdum. Views class ı içerisinde yer alan her bir statik değer ile parametresinde aldığı değeri karşılaştırıyor, eğer eşleşme bulunamazsa method false dönüyor.

```cs
public static bool IsViewNameValid(string view)
{
    var fields = typeof(Views).GetFields(BindingFlags.Public | BindingFlags.Static);
    // Gets all the values of the fields in the Views class

    List<string> values = fields.Select(field => (string)field.GetValue(null)).ToList();

    return values.Any(value => value.Equals(view));
}
```

 Dapper için yazdığım View için sorgu oluşturan methodun da bir parçası

```cs
public static IEnumerable<T> QueryView<T>(string view, string connectionString)
{
    if (!IsViewNameValid(view)) // To prevent a possible SQL injection, since the parameter is a string
        throw new ArgumentException("Invalid view name");

    using var connection = new SqlConnection(connectionString);
    connection.Open();

    return connection.Query<T>($"SELECT * FROM {view}");
}
```


# Masraf İşlemleri

Masraf için kategorisi bilgisi ve döküman bilgisi ``Expense.cs`` isimli class içerisinde yer almaktadır.

# Yönetici İşlemleri

## Seed Datalar
ModelBuilder için extension yazarak seed datalar oluşturdum. Test amaçlı kullanılabilirler.

<img title="" src="https://github.com/CanberkTimurlenk/Akbank-Net-Bootcamp-FinalCase/assets/18058846/c4ccc163-392e-4d43-b56d-1728a3a49ba0" width="600" height="450" alt=""  />

## Personel Ekleme
Personel ekleme işlevi admin rolü ile sınırlandırılmıştır.

```
[HttpPost]
[Authorize(Roles = Roles.Admin)]
public async Task<ApiResponse<EmployeeResponse>> Create(EmployeeRequest request)
{
    var (userId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity); // to add InsertUserId

    return await mediator.Send(new CreateEmployeeCommand(userId, request));
}
```

## Masraf Kategorisi, Ödeme Yöntemi gibi Alanların Eklenmesi
Yalnızca yönetici tarafından gerçekleştirilebilmesi için attribute kullanıldı.



# Caching
PaymentMethod ve ExpenseCategory gibi çok değişmeyeceğini düşündüğüm değerleri cache üzerinde MediatR sayesinde kullanılabilen IPipeline Behavior özelliğini kullanarak Cacheledim. GetById ya da GetAll gibi işlevler için okuma Cache üzerinden yapılıyor.

# Validations
Tüm requestler için validationlar oluşturdum. Regex ifadeleri de ekledim.

