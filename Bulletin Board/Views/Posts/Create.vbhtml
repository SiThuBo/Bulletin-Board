@ModelType BulletinBoard_OJT.DB_Entity.Posts
@Code
    ViewData("Title") = "Create"
End Code
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Post Create</title>

</head>
<body>
    <h2>Post Create</h2>

    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
            <h4>Posts</h4>
            <hr />
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
                </div>
            </div>


            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="Create" class="btn btn-outline-primary" />
                    @Html.ActionLink("Back to List", "Index", New With {.area = ""}, New With {.class = "btn btn-outline-secondary"})
                </div>
            </div>
        </div>
    End Using

    
</body>
</html>
