@ModelType PagedList.IPagedList(Of BulletinBoard_OJT.DB_Entity.Users)
@Imports PagedList.Mvc
<link href="~/Content/PagedList.css" rel="stylesheet" type="text/css" />
@Code
    ViewData("Title") = "User List"
End Code

<h2>User List</h2>

    @Using Html.BeginForm("Index", "Users", FormMethod.Get)

        @<div class="row">
             <div class="col-lg-2 col-sm-2">
                 @Html.Editor("searchName", TryCast(ViewBag.CurrentFilter, String), New With {.class = "form_control"})
                
             </div>
             <div class="col-lg-2 col-sm-2">
                 @Html.Editor("searchEmail", TryCast(ViewBag.CurrentFilter, String), New With {.class = "form_control"})
             </div>
            <div class="col-lg-3 col-sm-3">
                @Html.Editor("searchStart", New With {.htmlAttributes = New With {.class = "form-control datefield", .type = "date"}})
            </div>
            <div class="col-lg-3 col-sm-3">
                @Html.Editor("searchEnd", New With {.htmlAttributes = New With {.class = "form-control datefield", .type = "date"}})
            </div>
            <div class="col-lg-1 col-sm-1">
                <input type="submit" value="Search" class="btn btn-outline-primary" />
            </div>
            <div class="col-lg-1 col-sm-1">
                @Html.ActionLink("Add", "Create", New With {.area = ""}, New With {.class = "btn btn-outline-primary"})
            </div>
        </div>
    End Using
    <br />

    <div class="table-responsive">
        <table class="table table-bordered table-striped">
            <thead class="table-primary">
            <th>
                @Html.ActionLink("NAME", "Index", New With {.sortOrder = ViewBag.NameSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>
            <th>
                EMAIL
            </th>

            <th>
                Created_User

            </th>


            <th>
                PHONE
            </th>

            <th>
                DATE OF BIRTH
            </th>

            <th>
                @Html.ActionLink("CREATED DATE", "Index", New With {.sortOrder = ViewBag.DateSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>

            <th></th>
            </thead>

            @For Each item In Model
                @<tr>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Name)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Email)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.User.Name)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Phone)
                    </td>

                    <td>

                        @Convert.ToString(String.Format("{0:yyyy'-'MM'-'dd}", item.Dob))
                    </td>

                    <td>

                        @Convert.ToString(String.Format("{0:yyyy'-'MM'-'dd}", item.Created_At))
                    </td>


                    <td>
                        @Html.ActionLink("Edit", "Edit", New With {.id = item.Id}) |
                        @Html.ActionLink("Details", "Details", New With {.id = item.Id}) |
                        @Html.ActionLink("Delete", "Delete", New With {.id = item.Id})
                    </td>
                </tr>
            Next

        </table>
    </div>
    <br />
    Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount
    @Html.PagedListPager(Model, Function(page) Url.Action("Index", _
          New With {page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter}))
