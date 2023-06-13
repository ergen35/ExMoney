using Blazored.Modal.Services;
using ExMoney.Services;
using ExMoney.SharedLibs;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.WebUtilities;

namespace ExMoney.Pages.Exchanges
{
    public partial class MakeExchange
    {
        [Inject] public NavigationManager NavManager { get; set; }
        [Inject] public IModalService ModalService { get; set; }
        [Inject] public IExMoneyCurrenciesApi currenciesApi { get; set; }
        [Inject] public IMemoryCache memCache { get; set; }

        public string UiTitle { get; set; } = "Effectuer un Echange";
        private bool NtoFselected;
        private bool FtoNselected;
        public List<Currency> Currencies { get; set; }

        public int BaseCurrencyId { get; set; }
        public int ChangeCurrencyId { get; set; }
        public double Amount { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            List<Currency> cachedCurrencies = await memCache.GetOrCreateAsync("currencies", async (ce) =>
           {
               Refit.IApiResponse<List<Currency>> response = await currenciesApi.List();
               if (response.IsSuccessStatusCode)
               {
                   _ = ce.SetSlidingExpiration(TimeSpan.FromMinutes(3));
                   return response.Content!;
               }
               else
               {
                   return default;
               }
           });

            if (cachedCurrencies is not null)
            {
                Currencies = cachedCurrencies;
            }

            StateHasChanged();
        }

        public void SelectNtoF()
        {
            FtoNselected = false;
            NtoFselected = true;

            BaseCurrencyId = Currencies.FirstOrDefault(c => c.Symbol.ToLower() == "ngn").Id;
            ChangeCurrencyId = Currencies.FirstOrDefault(c => c.Symbol.ToLower() == "xof").Id;

            StateHasChanged();
        }

        public void SelectFtoN()
        {
            FtoNselected = true;
            NtoFselected = false;

            BaseCurrencyId = Currencies.FirstOrDefault(c => c.Symbol.ToLower() == "xof").Id;
            ChangeCurrencyId = Currencies.FirstOrDefault(c => c.Symbol.ToLower() == "ngn").Id;

            StateHasChanged();
        }

        public void GoToNextStep()
        {
            var nextUrl = NavManager.GetUriWithQueryParameters(NavManager.Uri, new Dictionary<string, object>() 
            {
                {"bcid", BaseCurrencyId.ToString()},
                {"ccid", ChangeCurrencyId.ToString()},
                {"amount", Amount.ToString()}
            });

            NavManager.NavigateTo(nextUrl);
        }
    }
}