using IOC.Web.Models;

var builder = WebApplication.CreateBuilder(args);

/*-------------------------------------------------------------------------*/

//Benim kullandýðým controllerýn DateService ihtiyacý varsa controllera inject edildiðini burada göstermem gerekiyor.
//Herhangi bir classýn constructorýnda ISingletonDateService ile karþýlaþýrsan git DateServiceden bir nesne örneði oluþtur diyoruz bu þekilde.
//Not => Uygulamanýn her yerinde ilk oluþturmuþ olduðumuz nesne örneðini kullanacak.
builder.Services.AddSingleton<ISingletonDateService, DateService>();

/*-------------------------------------------------------------------------*/

//Yaþam döngüsünü bir request ile sýnýrlandýrmýþ olduk.
//Ýlk requestte bir nesne örneði oluþacak ondan sonraki requestlerde hep ayný nesne örneði kullanýlacak.
//Ýkinci requestte sýfýrdan oluþturulur.
builder.Services.AddScoped<IScopedDateService, DateService>();

/*-------------------------------------------------------------------------*/

//Herhangi bir constructor da sen ITransientDateService bunu görürsen her gördüðün yerde bir tane nesne örneði oluþtur.
//Bir requestte beþ kez geçiyorsa 5 kez yeni nesne örneði oluþturulacak
builder.Services.AddTransient<ITransientDateService, DateService>();

/*-------------------------------------------------------------------------*/

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
