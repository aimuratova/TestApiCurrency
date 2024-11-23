using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace TestApiCurrency.BLL
{
    public class RateUsdKztBll
    {
        private string _currencyUrlPath = "";
        private readonly HttpClient _httpClient;

        public RateUsdKztBll(HttpClient client,IConfiguration configuration)
        {
            this._httpClient = client;
            this._currencyUrlPath = configuration.GetValue<string>("AppSettings:CurrencyUrlPath") ?? "";
        }

        public async Task<decimal?> getRateUsdKzt()
        {
            try
            {
                //check is url is assigned
                if (!String.IsNullOrEmpty(_currencyUrlPath))
                {
                    // Запрос на получение страницы
                    var response = await _httpClient.GetStringAsync(_currencyUrlPath);                    

                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(response);
                    
                    // Необходимо найти в таблице значение доллара используя XPath
                    var node = htmlDocument.DocumentNode.SelectSingleNode("//td[contains(text(), 'USD')]/following-sibling::td");

                    if (node != null)
                    {
                        string currency = node.InnerText.Trim();
                        if (decimal.TryParse(currency, out decimal rate))
                        {
                            return rate;
                        }
                    }
                    return null;
                }
                else
                {
                    Console.WriteLine("$Ошибка не найден путь к api");
                    return null;
                }
            }
            catch (Exception ex) { 
                Console.WriteLine("$Ошибка: {0}", ex.Message);
                return null;
            }

        }



    }
}
