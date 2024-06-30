using IOC.Web.Models;

var builder = WebApplication.CreateBuilder(args);

/*-------------------------------------------------------------------------*/

//Benim kulland���m controller�n DateService ihtiyac� varsa controllera inject edildi�ini burada g�stermem gerekiyor.
//Herhangi bir class�n constructor�nda ISingletonDateService ile kar��la��rsan git DateServiceden bir nesne �rne�i olu�tur diyoruz bu �ekilde.
//Not => Uygulaman�n her yerinde ilk olu�turmu� oldu�umuz nesne �rne�ini kullanacak.
builder.Services.AddSingleton<ISingletonDateService, DateService>();

/*-------------------------------------------------------------------------*/

//Ya�am d�ng�s�n� bir request ile s�n�rland�rm�� olduk.
//�lk requestte bir nesne �rne�i olu�acak ondan sonraki requestlerde hep ayn� nesne �rne�i kullan�lacak.
//�kinci requestte s�f�rdan olu�turulur.
builder.Services.AddScoped<IScopedDateService, DateService>();

/*-------------------------------------------------------------------------*/

//Herhangi bir constructor da sen ITransientDateService bunu g�r�rsen her g�rd���n yerde bir tane nesne �rne�i olu�tur.
//Bir requestte be� kez ge�iyorsa 5 kez yeni nesne �rne�i olu�turulacak
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
