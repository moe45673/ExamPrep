@ModelType Mvc5.ViewModels.VM_Error
@Code
    ViewData("Title") = "MyError"
End Code

<h2>MyError</h2>

<dl class="dl-horizontal">
    @For Each item In Model.ErrorMessages
      
      @<dt> @item.Key </dt>
      @<dd> @item.Value </dd>
        
    Next
</dl>