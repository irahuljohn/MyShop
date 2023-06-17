
var categoryId = 0;
var categoryList = [];
//function for category dataTable bind..
$(document).ready(function () {  
    BindCategorylist();

});

function BindCategorylist() {

    $("#categoryTbl").DataTable({
        serverSide: true,      
        dom: "lBfrtip",
        "draw": 8,
        //"recordsTotal": 5,
        "page": 1,
        //"recordsFiltered": 1,
        //bLengthChange: true,
        lengthMenu: [[5, 10, 50, -1], [5, 10, 50, "All"]],
        "filter": true, 
        bSort: true,
        bPaginate: true,
        bStateSave: true,
        processing: true,
        "destroy": true,
        ajax: {
            url: "/Product/GetCategory",
            type: "Post",
            "async": true,
            dataType: "json",
            dataSrc: function (json) {
                categoryList = json.data;
                return json.data;
            },
            error: function (response) {
                alert(response.d);
            }
        },
        columns: [
            { data: 'category' },
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
    $.ajax({
        type: "POST",
        url: '/Product/AddCategory',
        data: {
            ID: categoryId,
            Name: $('#txtCategoryName').val()
        },
        success: function (data) {
            console.log('success', data)
            $('#exampleModal').modal('hide');           
            $("#txtCategoryName").val('');
            BindCategorylist();
        },
        error: function (data) {
            console.log('error',data)
            $('#exampleModal').modal('hide');
            $("#txtCategoryName").val('');
        }
    });
});


//Function for Edit the category...
function EditCategory(id) {
    var data = categoryList.find(x => x.id == id);
    if (data != null) {
        $("#txtCategoryName").val(data.category);
        categoryId = id;
        $('#exampleModal').modal('show');
    }
    else {
        alert("Record not found");
    }
}

//for delete the category..
function DeleteCategory(id) {
    $.ajax({
        type: "POST",
        url: '/Product/DeleteCategory',
        data: { id },
        success: function (data) {
            console.log('success', data)
            if (data != null) {
                alert(data);
                BindCategorylist();
            }
        },
        error: function () {
            alert("error");
        }
    });
}


//Clear modal fields using Close button....
$('#btnClose').click(function () {
    $("#txtCategoryName").val('');
});
