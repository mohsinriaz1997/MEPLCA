using Microsoft.AspNetCore.Components;
using MudBlazor;
using CA.UI.General;


namespace CA.UI.Dialog
{
    public partial class DlgDocumentApproval
    {
        #region Variable 

        public string Remarks { get; set; }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        void Submit() => MudDialog.Close(DialogResult.Ok(Remarks));

        void Cancel() => MudDialog.Cancel();

        #endregion

        #region Initialize

        protected async override Task OnInitializedAsync()
        {
            try
            {
                await Task.Delay(4000);
            }
            catch (Exception ex)
            {
                Logs.GenerateLogs(ex);
            }
        }

        #endregion
    }
}
