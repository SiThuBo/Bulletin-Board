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

        a {
            text-decoration-line: none;
        }
    </style>

</head>
<body>
    <nav class="navbar sticky-top navbar-expand-sm navbar-dark " style="background-color: darkblue;">
        <div class="container">
            <div class="navbar-brand">
                <button type="button" class="navbar-toggler" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="navbar-toggler-icon"></span>
                </button>
                @Html.ActionLink("Bulletain Board", "Index", "Users", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div class="collapse navbar-collapse justify-content-end" id="navbarSupportedContent">
                <ul class="navbar-nav">
                    <li class="mr-2 nav-link">@Html.ActionLink("Users", "Index", "Users", New With {.area = ""}, New With {.style = "color:yellow"})</li>
                    <li class="mr-2 nav-link">@Html.ActionLink("User", "Details", "Users", New With {.area = ""}, New With {.style = "color:yellow"})</li>
                    <li class="mr-2 nav-link">@Html.ActionLink("Posts", "Index", "Posts", New With {.area = ""}, New With {.style = "color:yellow"})</li>
                    @If Not Session("CurrentUserName") Is Nothing Then
                        @<li Class="mr-2 nav-link text-light">@Session("CurrentUserName")</li>

                    End If


                    <li class="nav-link">@Html.ActionLink("Logout", "Logout", "Login", New With {.area = ""}, New With {.style = "color:yellow"})</li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <div class="navbar navbar-expand-md navbar-fixed-bottom">
        <footer class="text-center fixed-bottom" style="background-color: darkblue;">
            <p style="color:whitesmoke" class="mt-3">&copy; @DateTime.Now.Year - Seattle Consulting Myanmar Co.,Ltd</p>
        </footer>
        </div>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
</body>
</html>
