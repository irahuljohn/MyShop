
var productId = 0;

$(document).ready(function () {
   // BindProductlist();
    BindCategoryDDL();
});

function BindProductlist() {

    $("#Tblproduct").DataTable({
        serverSide: true,
        ajax: {
            url: "/Product/GetAllProducts",
            type: "GET",
            "async": true,
            dataType: "json",
            dataSrc: function (json) {
                categoryList = json;
                return json;
            },
            error: function (response) {
                alert(response.d);
            }
        },
        dom: "lBfrtip",
        "draw": 8,
        "recordsTotal": 5,
        "recordsFiltered": 5,
        //bLengthChange: true,
        lengthMenu: [[5, 50, -1], [5, 50, "All"]],
        "filter": true,
        bSort: true,
        bPaginate: true,
        bStateSave: true,
        processing: true,
        "destroy": true,
        columns: [
            { data: 'name' },
            { data: 'Description' },
            { data: 'category' },
            { data: 'price' },
            { data: 'createdDate' },
            {
                data: null, "sorting": false,
                className: "center",
                "render": function (data, type, row) {
                    var inner = "<a href='javascript:void(0);' onclick='EditCategory(" + row.id + ")' data-toggle='tooltip' data-placement='top' title='edit project details' class='popup icon-edit' />Edit</a>\
                                &nbsp;|&nbsp;<a href='javascript:void(0);' data-toggle='tooltip' data-placement='top' title='delete project details' class='icon-remove' onclick='DeleteCategory(" + row.id + ")'/>Delete</a>&nbsp;"
                    console.log("working");
                    return inner;
                }
            }],
        colReorder: true
    });
}


//Function for category save....
$("#btnsubmit").click(function () {

    var model = {
        Id: productId,
        Name: $('#txtProductName').val(),
        Description: $('#txtDescrip').val(),
        CategoryId: parseInt($('#ddlCategoryName').val()),
        Price: $('#txtProductPrice').val(),
        Quantity: $('#txtQuantity').val(),
    };
    debugger;
    $.ajax({
        type: "POST",
        url: '/Product/AddProduct',
        data: model,
        dataType: 'json',
        success: function (data) {
            $('#exampleModal').modal('hide');
            Clearfields();
        },
        error: function (data) {
            console.log("error: " + data);
            $('#exampleModal').modal('hide');
            Clearfields();
        }
    });
});

//Function used for category bind dropdown..
function BindCategoryDDL() {
    $.ajax({
        url: "/Product/GetCategorys",
        type: "GET",
        dataType: 'json',
        "async": true, 
        success: function (res) {
            console.log(res.data);
            debugger;
            $.each(res, function (data, value) {
                $("#ddlCategoryName").append($("<option></option>").val(value.id).html(value.category));
            })  
        },
        error: function (data) {
            debugger;
            console.log("error: " + data);
        }
    });
}

//Function use for clear Modal fields.
function Clearfields() {
    $("#txtProductName").val('');
    $("#txtDescrip").val('');
    $("#ddlCategoryName").val('');
    $("#txtProductPrice").val('');
}