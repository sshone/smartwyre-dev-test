using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection().AddSingleton<IRebateService, RebateService>()
            .AddSingleton<IRebateDataStore, RebateDataStore>()
            .AddSingleton<IProductDataStore, ProductDataStore>()
            .AddSingleton<IRebateStrategyFactory, RebateStrategyFactory>()
            .AddSingleton<IRebateStrategy, FixedCashAmountRebateStrategy>()
            .AddSingleton<IRebateStrategy, FixedRateRebateStrategy>()
            .AddSingleton<IRebateStrategy, AmountPerUomRebateStrategy>();

        var rebateService = serviceProvider.BuildServiceProvider().GetService<IRebateService>();

        Console.WriteLine("Enter rebate identifier:");
        string rebateIdentifer = Console.ReadLine();
        Console.WriteLine("Enter product identifier:");
        string productIdentifier = Console.ReadLine();
        Console.WriteLine("Enter volume:");
        string volume = Console.ReadLine();

        var result = rebateService.Calculate(new CalculateRebateRequest
        {
            ProductIdentifier = productIdentifier,
            RebateIdentifier = rebateIdentifer,
            Volume = Convert.ToInt32(volume)
        });

        if (result.Success)
        {
            Console.WriteLine("Rebate calculated successfully");
        }
        else
        {
            Console.WriteLine("Rebate calculation failed");
        }
    }
}
