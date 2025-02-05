using GetExchange_Rate;
using GetExchange_Rate.Bussines.Abstractions;

/*
 Получить котировки валют с ЦБ за текущий день. 
https://www.cbr.ru/development/SXML/ 
Должна быть переменная для выбора валюты 
Котировка выводится в консоль 
Опубликовать в открытом репозитории на github.com или любом аналогичном хостинге 
кода 
 */

IConsole console;
var serviceProvider = new ServiceInstaller(args);
console = serviceProvider.GetService<IConsole>();
await console.RunAsync();
