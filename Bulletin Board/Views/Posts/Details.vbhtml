@ModelType BulletinBoard_OJT.DB_Entity.Posts
@Code
    ViewData("Title") = "Details"
End Code
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Post Detail</title>

</head>
<body>
    <h2>Details</h2>

    <div>
        <h4>Posts</h4>
        <hr />
        <table class="text-monospace">

            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Title)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Title)
                </td>
            </tr>
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Description)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Description)
                </td>
            </tr>
            <tr>
                <td>
                    @Html.DisplayNameFor(Function(model) model.Status)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Status)
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
                    @Html.DisplayNameFor(Function(model) model.Updated_At)
                </td>

                <td>
                    @Html.DisplayFor(Function(model) model.Updated_At)
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
