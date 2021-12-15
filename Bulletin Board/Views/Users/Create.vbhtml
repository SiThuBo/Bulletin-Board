@ModelType BulletinBoard_OJT.DB_Entity.Users
@Code
    ViewData("Title") = "Create"
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />

    <title>User Create</title>
    <style>
        .error {
            color: #A94442;
        }
    </style>
</head>
<body>
    @Using (Html.BeginForm("Create", "Users", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
        @Html.AntiForgeryToken()
        @ViewBag.message
        @<div class="form-horizontal">
            <h4>Users</h4>
            <hr />
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            <div class="form-group">
                @Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Email, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Email, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Email, "", New With {.class = "text-danger"})
                    <p class="error">@ViewData("Email")</p>
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Password, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Password, New With {.htmlAttributes = New With {.class = "form-control", .type = "password"}})
                    @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.ConfirmPassowrd, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.ConfirmPassowrd, New With {.htmlAttributes = New With {.class = "form-control", .type = "password"}})
                    @Html.ValidationMessageFor(Function(model) model.ConfirmPassowrd, "", New With {.class = "text-danger"})
                </div>
            </div>

            @Using (Html.BeginForm("ImageUpload", "Users", FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "innerForm"}))
                @<div class="formbody">
                    <div Class="form-group">
                        @Html.LabelFor(Function(model) model.Profile, htmlAttributes:=New With {.class = "control-label col-md-2"})
                        <div Class="col-md-10">

                            <input type="file" name="ImageFile" accept="image/*" id="imageSelect" />

                            <p class="error">@ViewData("Message")</p>
                        </div>
                    </div>
                </div>

            End Using

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Phone, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Phone, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Phone, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Address, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Address, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.Address, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Dob, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Dob, New With {.htmlAttributes = New With {.class = "form-control datefield", .type = "date"}})
                    @Html.ValidationMessageFor(Function(model) model.Dob, "", New With {.class = "text-danger"})
                </div>
            </div>


            <div class="form-group">
                @Html.LabelFor(Function(model) model.Roles_Id, "Type", htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.DropDownList("Roles_Id", Nothing, htmlAttributes:=New With {.class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.Roles_Id, "", New With {.class = "text-danger"})
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
@Scripts.Render("~/bundles/jquery")
@Scripts.Render("~/bundles/jqueryval")
</html>
<script type="text/javascript">

    $(document).ready(function () {

        $('#imageSelect').change(function (e) {
           var fileUpload = $("#imageSelect").get(0);
var files = fileUpload.files;

// Create FormData object
var fileData = new FormData();

// Looping over all files and add it to FormData object
for (var i = 0; i < files.length; i++) {
fileData.append(files[i].name, files[i]);
}

            $.ajax({
                type: "POST",
                url: '/Users/ImageUpload',

                contentType: false,
                processData: false,
                 data: fileData,
                success: function (data) {


},
error: function (data) { }
});

        });
    });
</script>
