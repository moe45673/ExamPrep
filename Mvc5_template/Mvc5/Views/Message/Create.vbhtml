@ModelType Mvc5.ViewModels.MessageForHttpGet
@Code
    ViewData("Title") = "Create"
End Code

<h2>Create</h2>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <h4>MessageCreateForHttpGet</h4>
        <hr />
        @Html.ValidationSummary(true)
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Text, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Text)
                @Html.ValidationMessageFor(Function(model) model.Text)
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.TranslationText, New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.TranslationText)
                @Html.ValidationMessageFor(Function(model) model.TranslationText)
            </div>
        </div>
                 @* IMPORTANT: ADDED BY MF*@
         <div class="form-group">
             <label class="control-label col-md-2">Translation Language</label>
             <div class="col-md-10">
                 @* IMPORTANT: Ensure first param matches MessageHttpPost field*@
                 @Html.DropDownList("LanguageId", Model.LanguageSelectList)
                 @Html.ValidationMessageFor(Function(Model) Model.LanguageSelectList)
             </div>
         </div>

                 @* IMPORTANT: ADDED BY MF*@
         <div class="form-group">
             <label class="control-label col-md-2">Status</label>
             <div class="col-md-10">
                 @* IMPORTANT: Ensure first param matches MessageHttpPost field*@
                 @Html.DropDownList("StatusId", Model.StatusSelectList)
                 @Html.ValidationMessageFor(Function(Model) Model.StatusSelectList)
             </div>
         </div>

    <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    </div>
    
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
