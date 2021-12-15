@ModelType BulletinBoard_OJT.DB_Entity.Users
@Code
    ViewData("Title") = "Delete"
End Code
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>User Delete Confirmation</title>

</head>
<body>
    <h2>Delete</h2>


    <h3>Are you sure you want to delete this?</h3>
    <div>
        <h4>Users</h4>
        <hr />
        <dl class="dl-horizontal">
            <dt>
                @Html.DisplayNameFor(Function(model) model.Name)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Name)
            </dd>

            <dt>
                @Html.DisplayNameFor(Function(model) model.Email)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Email)
            </dd>s
            <dt>
                @Html.DisplayNameFor(Function(model) model.Phone)
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Phone)
            </dd>


            <dt>
                @Html.DisplayName("Role")
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.Role.Type)
            </dd>

            <dt>
                @Html.DisplayName("Created User Name")
            </dt>

            <dd>
                @Html.DisplayFor(Function(model) model.User.Name)
            </dd>



        </dl>
        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="form-actions no-color">
                <input type="submit" value="Delete" class="btn btn-danger" /> |
                @Html.ActionLink("Back to List", "Index")
            </div>
        End Using
    </div>
</body>
</html>
