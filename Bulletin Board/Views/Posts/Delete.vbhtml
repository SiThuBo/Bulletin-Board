@ModelType BulletinBoard_OJT.DB_Entity.Posts
@Code
    ViewData("Title") = "Delete"
End Code
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Post Delete Confirmation</title>

</head>
<body>
    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>
    <div>
        <h4>Posts</h4>
        <hr />
        <dl class="dl-horizontal">

            <dt>
                @Html.DisplayNameFor(Function(model) model.Title)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Title)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.Description)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Description)
            </dd>


            <dt>
                @Html.DisplayName("Created User Name")
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.User1.Name)
            </dd>


        </dl>
        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="form-actions no-color">
                <input type="submit" value="Delete" class="btn btn-default" /> |
                @Html.ActionLink("Back to List", "Index")
            </div>
        End Using
    </div>
</body>
</html>
