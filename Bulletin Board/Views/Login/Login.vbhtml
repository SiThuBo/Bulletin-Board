

@Code
    Layout = Nothing
End Code

@ModelType BulletinBoard_OJT.DB_Entity.Users




<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Bulletain Board_OJT </title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

    <style>
        li:hover {
            border: 1px;
            border-radius: 5px;
            background-color: deepskyblue;
            text-decoration: none;
        }
        #box{
            max-width:500px;
            margin-top:10%;
        }
        a {
            text-decoration-line: none;
        }
    </style>

</head>
<body>
    
    <div id="box" class="container justify-content-around border">
        <div class="text-center font-weight-bolder text-primary"><h4>LOGIN FORM</h4></div>
        <hr />
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
       
    <form action="Login" method="post" class="form-group form-horizontal">
        <div class="row justify-content-end">
            @Html.ValidationMessageFor(Function(model) model.User.Email, "", New With {.class = "text-danger"})
        </div>
        <div class="row form-group justify-content-center align-items-center">

            <div class="col-4">
                @Html.LabelFor(Function(model) model.Email, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>
            <div class="col-5">

                @Html.EditorFor(Function(model) model.User.Email, New With {.htmlAttributes = New With {.class = "form-control"}})
            </div>

        </div>

        <div class="row justify-content-end">

            @Html.ValidationMessageFor(Function(model) model.User.Password, "", New With {.class = "text-danger"})
        </div>
        <div class="row form-group justify-content-center align-items-center">

            <div class="col-4">
                @Html.LabelFor(Function(model) model.Password, htmlAttributes:=New With {.class = "control-label col-md-2"})
            </div>
            <div class="col-5">
                @Html.PasswordFor(Function(model) model.User.Password, New With {.class = "form-control"})
            </div>

        </div>

        <div class="row justify-content-center align-items-center">

            <div class="col-3">
                <input type="submit" value="Login" class="btn btn-primary text-center" />
            </div>

        </div>
    </form>

    </div>


    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryval")

</body>
</html>


