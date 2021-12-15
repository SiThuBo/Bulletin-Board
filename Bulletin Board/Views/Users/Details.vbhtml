@ModelType BulletinBoard_OJT.DB_Entity.Users
@Code
    ViewData("Title") = "Details"
    Dim userId = Model.Id.ToString() + "/"
End Code
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>User Detail</title>

</head>
<body>
    <h2>Details</h2>

    <div>
        <h4>Users</h4>
        <hr />
        <img src="@Url.Content("/Uploads/Images/" + userId + Model.Profile)" alt="profile" witdth="100px" height="100px" />
        <table class="text-monospace">
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Name)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Name)
                </td>
            </tr>
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Email)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Email)
                </td>
            </tr>
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Profile)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Profile)
                </td>
            </tr>
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Phone)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Phone)
                </td>
            </tr>
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Address)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Address)
                </td>
            </tr>
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Dob)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Dob)
                </td>
            </tr>
            <tr>

                <td>
                    @Html.DisplayNameFor(Function(model) model.Created_At)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Created_At)
                </td>

            </tr>
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Role.Type)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Role.Type)
                </td>
            </tr>
            <tr>
                <td>
                    @Html.DisplayName("Created User Name")
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.User.Name)
                </td>
            </tr>

        </table>
    </div>
    <p>
        @Html.ActionLink("Edit", "Edit", New With {.id = Model.Id}, New With {.class = "btn btn-outline-primary"}) |
        @Html.ActionLink("Back to List", "Index", New With {.area = ""}, New With {.class = "btn btn-outline-secondary"})
    </p>
</body>
</html>
