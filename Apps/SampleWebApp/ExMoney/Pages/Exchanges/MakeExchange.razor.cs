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
        [Inject] public NavigationManager navManager { get; set; }


        private readonly int BaseCurrencyId = 0;
        private readonly int ChangeCurrencyId = 0;
        private readonly double Amount = 0.0;

        public string UiTitle { get; set; } = "Effectuer un Echange";

        public Type DynamicViewType  { get; set; } = typeof(CurrenciesSelection);

        public Dictionary<string, object> ComponentParams { get; set; }
        public DynamicComponent DynamicView { get; set; }

        private int stepOrder = 1;

        protected override void OnInitialized()
        {
            ComponentParams = new()
            {
                { nameof(BaseCurrencyId), BaseCurrencyId },
                { nameof(ChangeCurrencyId), ChangeCurrencyId },
                { nameof(Amount), Amount }
            };
        }

        public void GoToRateCalculationStep()
        {
            var instance = (DynamicView.Instance as CurrenciesSelection);
            ComponentParams[nameof(BaseCurrencyId)] = instance.BaseCurrencyId;
            ComponentParams[nameof(ChangeCurrencyId)] = instance.ChangeCurrencyId;
            ComponentParams[nameof(Amount)] = instance.Amount;

            DynamicViewType = typeof(CheckoutView);
            stepOrder = 2;
            UiTitle = "Check Out";
            StateHasChanged();
        }

        public void ContinueToConfig()
        {
            if(stepOrder == 1)
                GoToRateCalculationStep();
            // StateHasChanged();
        }
    }
}