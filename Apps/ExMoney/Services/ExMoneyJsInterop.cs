using Microsoft.JSInterop;

namespace ExMoney.Services
{
    public class ExMoneyJsInterop
    {
        private readonly IJSRuntime jSRuntime;

        public ExMoneyJsInterop(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public async void ToggleSidebarNav()
        {
            await jSRuntime.InvokeVoidAsync("exmoneyInterop.sidebarTogglerClicked");
        }

        public async void CloseSidebarNav()
        {
            await jSRuntime.InvokeVoidAsync("exmoneyInterop.sideBarCloseBtnClicked");
        }

        public async void OnSidebarLinkClicked()
        {
            await jSRuntime.InvokeVoidAsync("exmoneyInterop.sideBarLinkClicked");
        }

        public async void InitDashboardCharts()
        {
            await jSRuntime.InvokeVoidAsync("exmoneyInterop.initDashboardCharts");
        }
    }
}
