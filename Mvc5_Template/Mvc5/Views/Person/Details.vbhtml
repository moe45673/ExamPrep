@ModelType Mvc5.ViewModels.MessageFull
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>MessageFull</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Text)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Text)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Status)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Status)
        </dd>
     </dl>
</div>

        @* Added manually by MF *@
        
       <div class="display-label"><i>Translations</i></div>
        @If Not IsNothing(Model.Translations) Then
          @<div class="dl-horizontal"> 
          @For Each item In Model.Translations

            Dim translation = item
            @*@<div class="display-field"> *@ 
       
           @<dt> @Html.DisplayNameFor(Function(unused) translation.Language) </dt>
           @<dd> @Html.DisplayFor(Function(unused) translation.Language) </dd>     

           @<dt> @Html.DisplayNameFor(Function(unused) translation.Text) </dt>
           @<dd> @Html.DisplayFor(Function(unused) translation.Text) </dd>     
           @<br /> 
            Next
         </div>
        Else
  
            @<p> @Html.Label("No details found") </p>

        End if
            
       </div>


<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>
