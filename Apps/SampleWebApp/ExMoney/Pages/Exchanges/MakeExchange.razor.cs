using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorAnimate;
using Blazored.Modal;
using Blazored.Modal.Services;
using ExMoney;
using ExMoney.Data;
using ExMoney.Pages.Auth;
using ExMoney.Pages.Components;
using ExMoney.Pages.Exchanges.Components;
using ExMoney.Services;
using ExMoney.Shared;
using ExMoney.SharedLibs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;

namespace ExMoney.Pages.Exchanges
{
    public partial class MakeExchange
    {
        [Inject] public IMemoryCache memCache { get; set; }
        [Inject] public IExMoneyCurrenciesApi currenciesApi { get; set; }
        [Inject] public NavigationManager navManager { get; set; }


        public List<Currency> Currencies { get; set; }
        public int BaseCurrencyId { get; set; }
        public int ChangeCurrencyId { get; set; }
        public double Amount { get; set; } = 0.0;

        public string UiTitle { get; set; } = "Effectuer un Echange";

        public Type DynamicViewType  { get; set; } = typeof(CurrenciesSelection);

        public Dictionary<string, object> ComponentParams { get; set; } = new();
        public DynamicComponent DynamicView { get; set; }

        private int stepOrder = 1;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //load currencies
                var cachedCurrencies = await memCache.GetOrCreateAsync<List<Currency>>("currencies", async (ce) =>
                {
                    var response = await currenciesApi.List();
                    if (response.IsSuccessStatusCode)
                    {
                        ce.SetSlidingExpiration(TimeSpan.FromMinutes(2));
                        return response.Content!;
                    }
                    else
                        return default;
                });

                if (cachedCurrencies is not null)
                {
                    Currencies = cachedCurrencies;

                    ComponentParams.Add("BaseCurrencyId", BaseCurrencyId);
                    ComponentParams.Add("ChangeCurrencyId", ChangeCurrencyId);
                    ComponentParams.Add("Amount", Amount);

                    StateHasChanged();
                }
            }
        }

        public void GoToRateCalculationStep()
        {
            DynamicViewType = typeof(CheckoutView);
            stepOrder = 2;
            UiTitle = "Check Out";
            StateHasChanged();
        }

        public void ContinueToConfig()
        {
            if(stepOrder == 1)
                GoToRateCalculationStep();
            StateHasChanged();
        }
    }
}