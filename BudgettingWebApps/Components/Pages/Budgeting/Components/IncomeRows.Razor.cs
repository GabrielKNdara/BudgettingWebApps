using BudgettingWebApps.Models;
using Microsoft.AspNetCore.Components;

namespace BudgettingWebApps.Components.Pages.Budgeting.Components
{
    public partial class IncomeRows
    {
        public bool ShowStatus { get; set; }
        [Parameter]
        public IncomeDto Income { get; set; }

        public void PremiumToggle (bool isPaidInFull)
        {
            //Income.
        }  
    }
}
