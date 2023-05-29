using Blazored.Modal;
using Blazored.Modal.Services;
using ExMoney.Pages.Exchanges.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;

namespace ExMoney.Pages.Exchanges
{
    public partial class MakeExchange
    {
        [Inject] public IMemoryCache memCache { get; set; }
        [Inject] public NavigationManager navManager { get; set; }
        [Inject] public IModalService modalService { get; set; }

        private readonly int BaseCurrencyId;
        private readonly int ChangeCurrencyId;
        private readonly double Amount;

        public string UiTitle { get; set; } = "Effectuer un Echange";

        public Type DynamicViewType { get; set; } = typeof(CurrenciesSelection);

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

        public void GoToCurrenciesSelectionStep()
        {

            var instance = DynamicView.Instance as CheckoutView;
            ComponentParams[nameof(BaseCurrencyId)] = instance.BaseCurrencyId;
            ComponentParams[nameof(ChangeCurrencyId)] = instance.ChangeCurrencyId;
            ComponentParams[nameof(Amount)] = instance.Amount;
            
            DynamicViewType = typeof(CurrenciesSelection);
            stepOrder = 1;
            UiTitle = "Effectuer un Echange";
            StateHasChanged();
        }

        public void GoToRateCalculationStep()
        {
            CurrenciesSelection instance = DynamicView.Instance as CurrenciesSelection;
            ComponentParams[nameof(BaseCurrencyId)] = instance.BaseCurrencyId;
            ComponentParams[nameof(ChangeCurrencyId)] = instance.ChangeCurrencyId;
            ComponentParams[nameof(Amount)] = instance.Amount;

            if((double)ComponentParams[nameof(Amount)] <= 0 || (int)ComponentParams[nameof(BaseCurrencyId)] == 0 ){
                return;
            }

            DynamicViewType = typeof(CheckoutView);
            stepOrder = 2;
            UiTitle = "Check Out";
            StateHasChanged();
        }

        public void GoToNextStep()
        {
            if (stepOrder == 1)
                GoToRateCalculationStep();
        }

        public void GotoPreviousStep()
        {
            if(stepOrder == 2)
                GoToCurrenciesSelectionStep();
        }
    }
}