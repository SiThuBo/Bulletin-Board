@ModelType BulletinBoard_OJT.DB_Entity.Posts
@Code
    ViewData("Title") = "ImportCSV"
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>CSV Upload</title>
    <style>
        .formbody {
            width: 500px;
        }

        p {
            color: red;
        }
    </style>
</head>
<body>
    <fieldset class="border m-5 p-5">
        <legend>Import CSV File</legend>
        <p>@ViewData("Message")</p>

        @Using (Html.BeginForm("ImportCSV", "Posts", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
            @Html.AntiForgeryToken()

            @<div class="formbody">
                <input type="file" name="postedfile" accept=".csv,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" /><br />
                <input type="submit" value="Upload" class="btn btn-primary mt-3" /> |
                @Html.ActionLink("Back to List", "Index", New With {.area = ""}, New With {.class = "btn btn-secondary mt-3"})
            </div>
        End Using

    </fieldset>
</body>
</html>


