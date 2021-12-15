@ModelType BulletinBoard_OJT.DB_Entity.Users
@Code
    ViewData("Title") = "Edit"
    Dim userId = Model.Id.ToString() + "/"

End Code
<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>User Edit</title>
    <style>
        .profileimage {
            width: 100px;
            height: 100px;
        }
    </style>
</head>
<body>
    <h2>Edit</h2>

    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-horizontal">
            <h4>Users</h4>
            <hr />
            @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            @Html.HiddenFor(Function(model) model.Id)
            @Html.HiddenFor(Function(model) model.Password)
            @Html.HiddenFor(Function(model) model.ConfirmPassowrd)
            @Html.HiddenFor(Function(model) model.Created_At)
            @Html.HiddenFor(Function(model) model.Is_Deleted)
            @Html.HiddenFor(Function(model) model.Created_User_Id)
            @Html.HiddenFor(Function(model) model.Profile)
            <div Class="form-group">
                @Html.LabelFor(Function(model) model.Profile, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <image-holder><img src="@Url.Content("/Uploads/Images/" + userId + Model.Profile)" alt="profile" class="profileimage" /></image-holder>
            </div>
            @Using (Html.BeginForm("ImageEdit", "Users", FormMethod.Post, New With {.enctype = "multipart/form-data", .id = "innerForm"}))
                @<div class="formbody">
                    <div Class="form-group">
                        <div class="col-md-2"></div>
                        <div class="col-md-10" id="wrapper">

                            <input type="file" name="ImageFile" accept="image/*" id="imageSelect" />

                            <p class="error">@ViewData("Message")</p>
                        </div>
                    </div>
                </div>

            End Using

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
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Password, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.PasswordFor(Function(model) model.Password, New With {.Value = Model.Password, .class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.ConfirmPassowrd, htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.PasswordFor(Function(model) model.ConfirmPassowrd, New With {.Value = Model.ConfirmPassowrd, .class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.ConfirmPassowrd, "", New With {.class = "text-danger"})
                </div>
            </div>

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
                @Html.LabelFor(Function(model) model.Roles_Id, "Roles", htmlAttributes:=New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.DropDownList("Roles_Id", Nothing, htmlAttributes:=New With {.class = "form-control"})
                    @Html.ValidationMessageFor(Function(model) model.Roles_Id, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <input type="submit" value="Save" class="btn btn-outline-primary" />
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
            var userId = $("#Id").val();

           var fileUpload = $("#imageSelect").get(0);
var files = fileUpload.files;

// Create FormData object
var fileData = new FormData();
fileData.append("userID",userId)
// Looping over all files and add it to FormData object
for (var i = 0; i < files.length; i++) {
fileData.append(files[i].name, files[i]);
}

            $.ajax({
                type: "POST",
                url: '/Users/ImageEdit',

                contentType: false,
                processData: false,
                 data: fileData,
                success: function (data) {


},
error: function (data) { }
            });
            var fileName = $(this)[0].files.name;
             var countFiles = $(this)[0].files.length;
          var imgPath = $(this)[0].value;
          var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();
          var image_holder = $("image-holder");
          image_holder.empty();
          if (extn == "gif" || extn == "png" || extn == "jpg" || extn == "jpeg") {
            if (typeof(FileReader) != "undefined") {
              //loop for each file selected for uploaded.
              for (var i = 0; i < countFiles; i++)
              {
                var reader = new FileReader();
                reader.onload = function(e) {
                  $("<img />", {
                    "src": e.target.result,
                    "class": "thumb-image profileimage"
                  }).appendTo(image_holder);
                }
                image_holder.show();
                reader.readAsDataURL($(this)[0].files[i]);
                }


            } else {
              alert("This browser does not support FileReader.");
            }
          } else {
            alert("Pls select only images");
          }

        });
    });
</script>