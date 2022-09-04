using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReservetionDesktopApp.DbContexts;
using ReservetionDesktopApp.Exceptions;
using ReservetionDesktopApp.HostBuilders;
using ReservetionDesktopApp.Models;
using ReservetionDesktopApp.Services;
using ReservetionDesktopApp.Services.ReservationConflictValidators;
using ReservetionDesktopApp.Services.ReservationCreators;
using ReservetionDesktopApp.Services.ReservationProviders;
using ReservetionDesktopApp.Stores;
using ReservetionDesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReservetionDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");

                    services.AddSingleton(new ReservetionDesktopAppDbContextFactory(connectionString));
                    services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                    services.AddTransient<ReservationBook>();

                    string hotelName = hostContext.Configuration.GetValue<string>("HotelName");
                    services.AddSingleton((s) => new Hotel(hotelName, s.GetRequiredService<ReservationBook>()));

                    services.AddSingleton<HotelStore>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            ReservetionDesktopAppDbContextFactory ReservetionDesktopAppDbContextFactory = _host.Services.GetRequiredService<ReservetionDesktopAppDbContextFactory>();
            using (ReservetionDesktopAppDbContext dbContext = ReservetionDesktopAppDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationService<ReservationListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
