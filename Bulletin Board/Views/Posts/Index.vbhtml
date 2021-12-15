@ModelType PagedList.IPagedList(Of BulletinBoard_OJT.DB_Entity.Posts)
@Imports PagedList.Mvc

<link href="~/Content/PagedList.css" rel="stylesheet" type="text/css" />
@Code
    ViewBag.Title = "Posts"
End Code
<h2>Posts List</h2>

@Using Html.BeginForm("Index", "Posts", FormMethod.Get)
    @<div class="row">

    @Html.Editor("SearchString", TryCast(ViewBag.CurrentFilter, String), New With {.class = "form_control"})|


    <input type="submit" value="Search" Class="btn btn-outline-primary" /> |


    @Html.ActionLink("Add", "Create", New With {.area = ""}, New With {.class = "btn btn-outline-primary"}) |

    @Html.ActionLink("Download", "ExportToCSV", New With {.area = ""}, New With {.class = "btn btn-outline-primary"}) |

    @Html.ActionLink("Upload", "ImportCSV", New With {.area = ""}, New With {.class = "btn btn-outline-primary"})


</div>
End Using
<br />

<div class="table-responsive">
    <Table Class="table table-bordered table-striped">
        <tr class="table-primary">

            <th>
                @Html.ActionLink("Title", "Index", New With {.sortOrder = ViewBag.NameSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>

            <th>
                Description
            </th>

            <th>
                @Html.ActionLink("Posted Date", "Index", New With {.sortOrder = ViewBag.DateSortParm, .currentFilter = ViewBag.CurrentFilter})
            </th>

            <th>
                Posted User
            </th>
            <th></th>
        </tr>

        @For Each item In Model
            @<tr>

                <td>
                    @Html.DisplayFor(Function(modelItem) item.Title)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Description)
                </td>

                <td>

                    @Convert.ToString(String.Format("{0:yyyy'-'MM'-'dd}", item.Created_At))
                </td>

                <td>
                    @Html.DisplayFor(Function(modelItem) item.User.Name)
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", New With {.id = item.Id}) |
                    @Html.ActionLink("Details", "Details", New With {.id = item.Id}) |
                    @Html.ActionLink("Delete", "Delete", New With {.id = item.Id})
                </td>
            </tr>
        Next

    </Table>
</div>

<br />
        Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount
        @Html.PagedListPager(Model, Function(page) Url.Action("Index",
                       New With {page, .sortOrder = ViewBag.CurrentSort, .currentFilter = ViewBag.CurrentFilter}))
