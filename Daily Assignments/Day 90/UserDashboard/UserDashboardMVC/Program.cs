namespace UserDashboardMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // ── HttpClient → ProductAPI ───────────────────────────
            builder.Services.AddHttpClient("ProductAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ProductAPI:BaseUrl"]
                                             ?? throw new Exception("ProductAPI:BaseUrl not configured"));
            });

            // ── HttpClient → AddressAPI ───────────────────────────
            builder.Services.AddHttpClient("AddressAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["AddressAPI:BaseUrl"]
                                             ?? throw new Exception("AddressAPI:BaseUrl not configured"));
            });

            // ── HttpClient → Postal Pincode API (external) ────────
            builder.Services.AddHttpClient("PostalAPI", client =>
            {
                client.BaseAddress = new Uri("https://api.postalpincode.in/");
                client.Timeout = TimeSpan.FromSeconds(8);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

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
        }
    }
}
