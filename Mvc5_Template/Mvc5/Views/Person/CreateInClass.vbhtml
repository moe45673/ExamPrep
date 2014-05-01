@ModelType Mvc5.ViewModels.MessageForHttpGet
@Code
    ViewData("Title") = "CreateInClass"
End Code

<h2>CreateInClass</h2>

@Using (Html.BeginForm()) 
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <h4>MessageForHttpGet</h4>
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
