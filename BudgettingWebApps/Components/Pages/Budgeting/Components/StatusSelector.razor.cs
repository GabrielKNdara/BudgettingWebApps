using BudgettingWebApps.Models;
using Microsoft.AspNetCore.Components;

namespace BudgettingWebApps.Components.Pages.Budgeting.Components
{
    public partial class StatusSelector
    {
        [Parameter]
        public EventCallback<bool> OnPremiumToggle { get; set; }
        private bool SaveButtonDisabled = true;
        [Parameter]
        public IncomeDto income{ get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
        List<IncomeStatusDto> incomeStatus = new List<IncomeStatusDto>
        {
            new IncomeStatusDto(){Id = 1, IncomeId=1,IsPaid=true,TransactionDate=new DateTime(2024,01,01),Comments="Received My Salary"},
            new IncomeStatusDto(){Id = 2, IncomeId=2,IsPaid=false,TransactionDate=new DateTime(),Comments=""},
            new IncomeStatusDto(){Id = 3, IncomeId=3,IsPaid=true,TransactionDate=new DateTime(2024,01,10),Comments="Received 100"},
            new IncomeStatusDto(){Id = 4, IncomeId=3,IsPaid=true,TransactionDate=new DateTime(2024,01,20),Comments="Received 50"},
            new IncomeStatusDto(){Id = 5, IncomeId=3,IsPaid=false,TransactionDate=new DateTime(),Comments="Not received yet"},
            new IncomeStatusDto(){Id = 6, IncomeId=2,IsPaid=false,TransactionDate=new DateTime(),Comments=""},
            new IncomeStatusDto(){Id = 7, IncomeId=2,IsPaid=true,TransactionDate=new DateTime(),Comments="Received 500"},
            new IncomeStatusDto(){Id = 8, IncomeId=2,IsPaid=false,TransactionDate=new DateTime(),Comments="Expecting anytime"}
        };
        public async Task CheckBoxChanged(IncomeStatusDto Status)
        {
            var newValue = Status.IsPaid;
           // incomeStatus.IsPaid = newValue;
            SaveButtonDisabled = false;
            if(newValue)
            {
                Status.TransactionDate = DateTime.Now;
            }
            await OnPremiumToggle.InvokeAsync(incomeStatus.Any(b =>b.IsPaid ));
        }
        public async Task SaveClick()
        {
            SaveButtonDisabled=true;
        }
       
      
    }
}
